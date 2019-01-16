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

public class MomentServices:MonoBehaviour  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private Moment _nullMoment = 
		new Moment{
				id = 0,
				name = "null",
				percentage = 0,
				creationDate = "null",
				caseId = 0,
				lastUpdate = "null"
		};
	
	
	private bool isQueryOk = false;

	private Moment _momentGetToDB = new Moment();

	private int resultToDB = 0;

	private IEnumerable<Moment> _momentsLoaded = new Moment[]{
		new Moment{
				id = 0,
				name = "null",
				percentage = 0,
				creationDate = "null",
				caseId = 0,
				lastUpdate = "null"
		},
		new Moment{
				id = 0,
				name = "null",
				percentage = 0,
				creationDate = "null",
				caseId = 0,
				lastUpdate = "null"
		},
		new Moment{
				id = 0,
				name = "null",
				percentage = 0,
				creationDate = "null",
				caseId = 0,
				lastUpdate = "null"
		},
		new Moment{
				id = 0,
				name = "null",
				percentage = 0,
				creationDate = "null",
				caseId = 0,
				lastUpdate = "null"
		}
	};


	/// <summary>
	/// Description to method to create a moment.
	/// </summary>
	/// <param name="momentname">
	/// Attribute that contains an string with the name of the moment that will be created.
	/// </param>
	/// /// <param name="caseid">
	/// Attribute that contains case identifier to the moment that will be created.
	/// </param>
	/// <returns>
	/// An integer response of the query (0 = the object was not created correctly. !0 = the object was created correctly)
	/// </returns>

	public int CreateMoment(string momentname, int caseid){

		//valueToResponse = 1

		int valueToReturn = 0;

		//Get the current date to create the new group
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		 var new_m = new Moment{
				name = momentname,
				percentage = 0,
				creationDate = date,
				caseId = caseid,
				lastUpdate = date
		};

		int result = _connection.Insert (new_m);

		Debug.Log(new_m);
		
		return valueToReturn;
		
	}

	/// <summary>
	/// Description of the method to obtain all the moments of a specific case
	/// </summary>
	/// <param name="caseId">
	/// integer to define the identifier of the case from which all the related moments will be brought.
	/// <returns>
	/// A IEnumerable list of all the moments found from the identifier of the case that was passed as a parameter
	/// </returns>
	public IEnumerable<Moment> GetMoments(int caseId){
		//valueToResponse = 2
		return _connection.Table<Moment>().Where(x => x.caseId == caseId);
	}

	/// <summary>
	/// Description of the method to delete a moment
	/// </summary>
	/// <param name="momentToDelete">
	/// An object of type moment that contain the moment that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteMoment(Moment momentToDelete){

		//valueToResponse = 3

		int result = _connection.Delete(momentToDelete);

		if (result!=0)
			Debug.Log("Se borró el momento correctamente");
		else
			Debug.Log("No se borró el momento");
		
		return result;
	}

	/// <summary>
	/// Description of the method to update a moment
	/// </summary>
	/// <param name="newpercentage">
	/// An integer that contain the new percetage that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateMoment(int newpercentage){

		//valueToResponse = 4
		
		var momentToUpdate = DataBaseParametersCtrl.Ctrl._momentLoaded;
		
		var _caseServices = new CaseServices();

		momentToUpdate.percentage = newpercentage;
		momentToUpdate.lastUpdate = DataBaseParametersCtrl.Ctrl.GetDateTime();

		int result = _connection.Update(momentToUpdate, momentToUpdate.GetType());

		if (result!=0)
		{
			_caseServices.UpdateCase(momentToUpdate.caseId);
		}

		return result;
	}

	#region METHODS to get data to DB

	public void setDBToWeb(string methodToCall, int valueToResponse, Moment moment){

		//UserData tempUser = new UserData (player.id, player.cycle, game);
		string json = JsonUtility.ToJson (moment, true);
		UnityWebRequest postRequest = SetJsonForm (json, methodToCall);
		if (postRequest != null){
			switch(valueToResponse){
				case 1:

				StartCoroutine (waitDB_ToCreateMoment (postRequest));

				break;

				case 3:

				StartCoroutine (waitDB_ToDeleteMoment (postRequest));
				
				break;

				case 4:

				StartCoroutine (waitDB_ToUpdateMoment (postRequest));
				
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

	IEnumerator waitDB_ToCreateMoment (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseCreateMoment resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseCreateMoment> (www.downloadHandler.text);
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
					_momentGetToDB = resp.momentCreated;
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

	IEnumerator waitDB_ToDeleteMoment (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseDeleteMoment resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseDeleteMoment> (www.downloadHandler.text);
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

	IEnumerator waitDB_ToUpdateMoment (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseUpdateMoment resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseUpdateMoment> (www.downloadHandler.text);
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
           
			yield return (waitDB_ToGetMoments (postRequest));

        }

	

	IEnumerator waitDB_ToGetMoments (WWW www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseGetMoments resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseGetMoments> (www.text);
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
					_momentsLoaded = resp.moments;
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

