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

public class FieldServices:MonoBehaviour  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private Field _nullField = 
		new Field{
				id = 0,
				name = "null",
				description = "null",
				percentage = 0,
				creationDate = "null",
				lastUpdate = "null",
				problemId = 0
		};
	

	private bool isQueryOk = false;

	private Field _fieldGetToDB = new Field();

	private int resultToDB = 0;

	private IEnumerable<Field> _fieldsLoaded = new Field[]{
		new Field{
				id = 0,
				name = "null",
				description = "null",
				percentage = 0,
				creationDate = "null",
				lastUpdate = "null",
				problemId = 0
		},
		new Field{
				id = 0,
				name = "null",
				description = "null",
				percentage = 0,
				creationDate = "null",
				lastUpdate = "null",
				problemId = 0
		},
		new Field{
				id = 0,
				name = "null",
				description = "null",
				percentage = 0,
				creationDate = "null",
				lastUpdate = "null",
				problemId = 0
		}
	};

	/// <summary>
	/// Description to method to create a field
	/// </summary>
	/// <param name="stringfield">
	/// Attribute that contains an string with the name of the field that will be created.
	/// </param>
	/// <param name="fielddescription">
	/// Attribute that contains an string with the description of the field that will be created.
	/// </param>
	/// <returns>
	/// An object of type field with all the data of the field that was created.
	/// </returns>

	public Field CreateField(string fieldname, string fielddescription){

		//valueToResponse = 1

		//The identifier of the project is obtained to be able to pass 
		//it as an attribute in the new problem that will be created
		int problemid = DataBaseParametersCtrl.Ctrl._problemLoaded.id;

		//Get the current date to create the new field
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		//Creation of the new problem
		var new_f = new Field{
				name = fieldname,
				description = fielddescription,
				percentage = 100,
				creationDate = date,
				lastUpdate = date,
				problemId = problemid
		};

		//Start-Validation that the query is right
		
		int result = _connection.Insert (new_f);

		if (result != 0)
		{
			Debug.Log(new_f);
			return new_f;
		}else {
			return _nullField;
		}
		
		
	}

	/// <summary>
	/// Description of the method to obtain all the fields of a specific project
	/// </summary>
	/// <param name="problemId">
	/// integer to define the identifier of the problem from which all the related fields will be brought.
	/// <returns>
	/// A IEnumerable list of all the Problems found from the identifier of the project that was passed as a parameter
	/// </returns>
	public IEnumerable<Field> GetFields(int problemId){
		
		//valueToResponse = 2

		return _connection.Table<Field>().Where(x => x.problemId == problemId);
	}

	/// <summary>
	/// Description of the method to delete a field
	/// </summary>
	/// <param name="fieldToDelete">
	/// An object of type field that contain the field that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteField(Field fieldToDelete){

		
		//valueToResponse = 3

		int result = _connection.Delete(fieldToDelete);

		if (result!=0)
			Debug.Log("Se borró el campo correctamente");
		else
			Debug.Log("No se borró el campo");
		
		return result;
	}

	/// <summary>
	/// Description of the method to update a field
	/// </summary>
	/// <param name="arraystringsfield">
	/// Attribute that contains an string array with new desciption of the each field that will be created.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateFields(string [] arraystringsfield){

		
		//valueToResponse = 4

		var _problemServices = new ProblemServices();
		
		int problemid = DataBaseParametersCtrl.Ctrl._problemLoaded.id;
		
		var fields = GetFields(problemid);

		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		int counter = 0;

		int result = 0;

		foreach (var f in fields)
		{
			f.description = arraystringsfield[counter];
			f.lastUpdate = date;
			counter++;
			result+=_connection.Update(f, f.GetType());

		}

		if (result == 3) {
			int r = _problemServices.UpdateProblem();
			if (r!=0)
			{
				result = 1;
			}
			
		}else 
			result = 0;
		return result;
	}

	#region METHODS to get data to DB

	public void setDBToWeb(string methodToCall, int valueToResponse, Field field){

		//UserData tempUser = new UserData (player.id, player.cycle, game);
		string json = JsonUtility.ToJson (field, true);
		UnityWebRequest postRequest = SetJsonForm (json, methodToCall);
		if (postRequest != null){
			switch(valueToResponse){
				case 1:

				StartCoroutine (waitDB_ToCreateField (postRequest));

				break;

				case 5:

				StartCoroutine (waitDB_ToDeleteField (postRequest));
				
				break;

				case 6:

				StartCoroutine (waitDB_ToUpdateField (postRequest));
				
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

	IEnumerator waitDB_ToCreateField (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseCreateField resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseCreateField> (www.downloadHandler.text);
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
					_fieldGetToDB = resp.fieldCreated;
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

	IEnumerator waitDB_ToDeleteField (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseDeleteField resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseDeleteField> (www.downloadHandler.text);
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

	IEnumerator waitDB_ToUpdateField (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseUpdateField resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseUpdateField> (www.downloadHandler.text);
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
           
			yield return (waitDB_ToGetFields (postRequest));
		
        }


	IEnumerator waitDB_ToGetFields (WWW www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseGetFields resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseGetFields> (www.text);
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
					_fieldsLoaded = resp.fields;
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

