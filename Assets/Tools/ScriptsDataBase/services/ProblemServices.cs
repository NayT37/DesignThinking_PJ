using SQLite4Unity3d;
using UnityEngine;
using System;
using UnityEngine.Networking;
using System.Collections;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class ProblemServices:MonoBehaviour  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;


	private Problem _nullProblem = new Problem{
				id = 0,
				percentage = 0,
				creationDate = "null",
				lastUpdate = "null",
				projectId = 0
		};
	
	private FieldServices _fieldServices = new FieldServices();

	private bool isQueryOk = false;

	private Problem _problemGetToDB = new Problem();

	private int resultToDB = 0;


	/// <summary>
	/// Description to method to create a problem
	/// </summary>
	/// <param name="arrayfieldsname">
	/// Attribute that contains an string array with names of the each field that will be created.
	/// </param>
	/// <returns>
	/// An object of type problem with all the data of the problem that was created.
	/// </returns>

	public Problem CreateProblem(string[] arrayfieldsname){

		//valueToResponse = 1

		//The identifier of the project is obtained to be able to pass 
		//it as an attribute in the new problem that will be created
		int projectid = DataBaseParametersCtrl.Ctrl._projectLoaded.id;

		//Get the current date to create the new problem
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		//Creation of the new problem
		var new_p = new Problem{
				percentage = 100,
				creationDate = date,
				lastUpdate = date,
				projectId = projectid
		};

		//Start-Validation that the query is right
		
		int result = _connection.Insert (new_p);

		if (result != 0)
		{
			DataBaseParametersCtrl.Ctrl._problemLoaded = new_p;
			for (int i = 0; i < arrayfieldsname.Length; i++)
			{
				//Creation of the fields
				_fieldServices.CreateField("Field_"+(i+1), arrayfieldsname[i]);
			}
			Debug.Log(new_p);
			return new_p;
		}else {
			return _nullProblem;
		}
		//End-Validation that the query
		
	}

	/// <summary>
	/// Description of the method to obtain all the problems of a specific project
	/// </summary>
	/// <param name="projectId">
	/// integer to define the identifier of the project from which all the related problems will be brought.
	/// <returns>
	/// A IEnumerable list of all the Problems found from the identifier of the project that was passed as a parameter
	/// </returns>
	public Problem GetProblem(int projectId){

		//valueToResponse = 2

		return _connection.Table<Problem>().Where(x => x.projectId == projectId).FirstOrDefault();
	}

	/// <summary>
	/// (This is a test method) Description of the method to obtain all the Problem
	/// </summary>
	/// <returns>
	/// A IEnumerable list of all the projects found
	/// </returns>
	public int GetProblemsCounter(){

		//valueToResponse = 3

		int projectid = DataBaseParametersCtrl.Ctrl._projectLoaded.id;
		return _connection.Table<Problem>().Where(x => x.projectId == projectid).Count();
	}

	/// <summary>
	/// Description of the method to delete a problem
	/// </summary>
	/// <param name="problemToDelete">
	/// An object of type problem that contain the problem that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteProblem(Problem problemToDelete){

		//valueToResponse = 4

		int problemid = problemToDelete.id;
		// All the fields belonging to the problem that will be deleted are obtained.
		var fields = _fieldServices.GetFields(problemid);

		int result = _connection.Delete(problemToDelete);

		int valueToReturn = 0;

		//If the elimination of the problem is correct, then the fields corresponding to that case are eliminated.
		if (result!=0)
		{
			foreach (var field in fields)
			{
				valueToReturn += _fieldServices.DeleteField(field);
			}
			Debug.Log("Se borró el problema campo correctamente");
		} else {
			valueToReturn = 0;
			Debug.Log("No se borró el problema");
		}

		return valueToReturn;
	}

	/// <summary>
	/// Description of the method to update a problem
	/// </summary>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateProblem(){

		var _projectServices = new ProjectServices();
		
		var problemToUpdate = DataBaseParametersCtrl.Ctrl._problemLoaded;
		problemToUpdate.lastUpdate = DataBaseParametersCtrl.Ctrl.GetDateTime();

		int result = _connection.Update(problemToUpdate, problemToUpdate.GetType());

		if (result!=0)
		{
			DataBaseParametersCtrl.Ctrl._problemLoaded = problemToUpdate;
			_projectServices.UpdateProject(true);
		}
		return result;
	}

	
		#region METHODS to get data to DB

	public void setDBToWeb(string methodToCall, int valueToResponse, Problem problem){

		//UserData tempUser = new UserData (player.id, player.cycle, game);
		string json = JsonUtility.ToJson (problem, true);
		UnityWebRequest postRequest = SetJsonForm (json, methodToCall);
		if (postRequest != null){
			switch(valueToResponse){
				case 1:

				StartCoroutine (waitDB_ToCreateProblem (postRequest));

				break;

				case 4:

				StartCoroutine (waitDB_ToDeleteProblem (postRequest));
				
				break;
			}
		}
			
	
	}

	private UnityWebRequest SetJsonForm (string json, string method) {
		try {
			UnityWebRequest web = UnityWebRequest.Put (DataBaseParametersCtrl.Ctrl._ipServer + method + "/put", json);
			web.SetRequestHeader ("Content-Type", "application/json");
			return web;
		} catch {
			return null;
		}
	}

	IEnumerator waitDB_ToCreateProblem (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseCreateProblem resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseCreateProblem> (www.downloadHandler.text);
            } catch { }

            //Validacion de la informacion obtenida
            if (!string.IsNullOrEmpty (www.error) && resp == null) { //Error al descargar data
                Debug.Log (www.error);
                try {

                } catch (System.Exception e) { Debug.Log (e); }
                yield return null;
            } else

            if (resp != null) { // Informacion obtenida exitosamente
                if (!resp.error) { // sin error en el servidor
					_problemGetToDB = resp.problemCreated;
					isQueryOk = true;
                    } else { // no existen usuarios
                    }

                } else { //Error en el servidor de base de datos
                    // Debug.Log ("user error: " + resp.error);
                    try {

                    } catch { }
                    // HUDController.HUDCtrl.MessagePanel (resp.msg);
                }
            }
        
        yield return null;
    }

	IEnumerator waitDB_ToDeleteProblem (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseDeleteProblem resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseDeleteProblem> (www.downloadHandler.text);
            } catch { }

            //Validacion de la informacion obtenida
            if (!string.IsNullOrEmpty (www.error) && resp == null) { //Error al descargar data
                Debug.Log (www.error);
                try {

                } catch (System.Exception e) { Debug.Log (e); }
                yield return null;
            } else

            if (resp != null) { // Informacion obtenida exitosamente
                if (!resp.error) { // sin error en el servidor
					resultToDB = resp.result;
					isQueryOk = true;
                    } else { // no existen usuarios
                    }

                } else { //Error en el servidor de base de datos
                    // Debug.Log ("user error: " + resp.error);
                    try {

                    } catch { }
                    // HUDController.HUDCtrl.MessagePanel (resp.msg);
                }
            }
        
        yield return null;
    }

	#endregion

	#region METHODS to get data to DB
	public IEnumerator GetToDB (string methodToCall, string parameterToGet, int valueToResponse) {

            WWW postRequest = new WWW (DataBaseParametersCtrl.Ctrl._ipServer + methodToCall + parameterToGet); // buscar en el servidor al usuario
            switch(valueToResponse){
				case 2:

				yield return (waitDB_ToGetProblem (postRequest));

				break;

				case 3:

				yield return (waitDB_ToGetProblemsCounter (postRequest));
				
				break;

			}
        }

	IEnumerator waitDB_ToGetProblem (WWW www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseGetProblem resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseGetProblem> (www.text);
            } catch { }

            //Validacion de la informacion obtenida
            if (!string.IsNullOrEmpty (www.error) && resp == null) { //Error al descargar data
                Debug.Log (www.error);
                try {

                } catch (System.Exception e) { Debug.Log (e); }
                yield return null;
            } else

            if (resp != null) { // Informacion obtenida exitosamente
                if (!resp.error) { // sin error en el servidor
					_problemGetToDB = resp.problem;
					isQueryOk = true;
                    } else { // no existen usuarios
                    }

                } else { //Error en el servidor de base de datos
                    // Debug.Log ("user error: " + resp.error);
                    try {

                    } catch { }
                    // HUDController.HUDCtrl.MessagePanel (resp.msg);
                }
            }
        
        yield return null;
    }

	IEnumerator waitDB_ToGetProblemsCounter (WWW www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseGetProblemCounter resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseGetProblemCounter> (www.text);
            } catch { }

            //Validacion de la informacion obtenida
            if (!string.IsNullOrEmpty (www.error) && resp == null) { //Error al descargar data
                Debug.Log (www.error);
                try {

                } catch (System.Exception e) { Debug.Log (e); }
                yield return null;
            } else

            if (resp != null) { // Informacion obtenida exitosamente
                if (!resp.error) { // sin error en el servidor
					resultToDB = resp.counter;
					isQueryOk = true;
                    } else { // no existen usuarios
                    }

                } else { //Error en el servidor de base de datos
                    // Debug.Log ("user error: " + resp.error);
                    try {

                    } catch { }
                    // HUDController.HUDCtrl.MessagePanel (resp.msg);
                }
            }
        
        yield return null;
    }

	#endregion
}

