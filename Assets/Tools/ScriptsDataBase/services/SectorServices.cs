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

public class SectorServices:MonoBehaviour  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private Sector _nullSector = 
		new Sector{
				id = 0,
				name = "null",
				description = "",
				creationDate = "null",
				empathymapId = 0,
				lastUpdate = "null"
		};
	
	private bool isQueryOk = false;

	private Sector _sectorGetToDB = new Sector();

	private int resultToDB = 0;

	private IEnumerable<Sector> _sectorsLoaded = new Sector[]{
		new Sector{
				id = 0,
				name = "null",
				description = "",
				creationDate = "null",
				empathymapId = 0,
				lastUpdate = "null"
		},
		new Sector{
				id = 0,
				name = "null",
				description = "",
				creationDate = "null",
				empathymapId = 0,
				lastUpdate = "null"
		},
		new Sector{
				id = 0,
				name = "null",
				description = "",
				creationDate = "null",
				empathymapId = 0,
				lastUpdate = "null"
		}
	};

	/// <summary>
	/// Description to method to create a sector
	/// </summary>
	/// <param name="sectorname">
	/// Attribute that contains an string with the sector's name that will be created.
	/// </param>
	/// <returns>
	/// An object of type sector with all the data of the sector that was created.
	/// </returns>

	public Sector CreateSector(string sectorname){

		//valueToResponse = 1

		//The identifier of the empathymap is obtained to be able to pass 
		//it as an attribute in the new sector that will be created
		int empathymapid = DataBaseParametersCtrl.Ctrl._empathyMapLoaded.id;

		//Get the current date to create the new empathymap
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		//Creation of the new empathymap
		var new_s = new Sector{
				name = sectorname,
				description = "",
				creationDate = date,
				empathymapId = empathymapid,
				lastUpdate = date
		};

		//Start-Validation that the query is right
		
		int result = _connection.Insert (new_s);

		if (result != 0)
		{
			Debug.Log(new_s);
			return new_s;	
			
		}else {
			return _nullSector;
		}
		//End-Validation that the query		

			
		// } else {
		// 	return _nullPublic;
		// }
		
		
	}
	/// <summary>
	/// Description to method Get Section with the specified empathymapid
	/// </summary>
	/// <returns>
	/// An object of type section with all the data of the section that was searched and if doesnt exist so return an empty section.
	/// </returns>
	public int GetSectorWithDescription(){

		int empathymapid = DataBaseParametersCtrl.Ctrl._empathyMapLoaded.id;	
		int counter = _connection.Table<Sector>().Where(x => x.empathymapId == empathymapid).Where(x => x.description != "").Count();
		return counter;
	}

	/// <summary>
	/// Description of the method to obtain all the sectors of a specific empathyMap
	/// </summary>
	/// <param name="empathymapId">
	/// integer to define the identifier of the empathyMap from which all the related problems will be brought.
	/// <returns>
	/// A IEnumerable list of all the sectorsa found from the identifier of the empathyMap that was passed as a parameter
	/// </returns>
	public IEnumerable<Sector> GetSectors(int empathymapId){

		//valueToResponse = 2
		return _connection.Table<Sector>().Where(x => x.empathymapId == empathymapId);
	}

	/// <summary>
	/// Description of the method to delete a empathymap
	/// </summary>
	/// <param name="sectorToDelete">
	/// An object of type empathymap that contain the empathymap that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteSector(Sector sectorToDelete){
		
		//valueToResponse = 3
		
		return _connection.Delete(sectorToDelete);
	}

	/// <summary>
	/// Description of the method to update a note
	/// </summary>
	/// <param name="newdescription">
	/// Attribute that contains an string with the new description of the sector that will be created.
	/// <returns>
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public Sector UpdateSector(string newdescription){

		//valueToResponse = 4

		var sectorToUpdate = DataBaseParametersCtrl.Ctrl._sectorLoaded;

		var empathymapServices = new EmpathymapServices();

		sectorToUpdate.lastUpdate = DataBaseParametersCtrl.Ctrl.GetDateTime();

		sectorToUpdate.description = newdescription;

		int result = _connection.Update(sectorToUpdate, sectorToUpdate.GetType());

		if (result!=0)
		{
			DataBaseParametersCtrl.Ctrl._sectorLoaded = sectorToUpdate;
			empathymapServices.UpdateEmpathymap();
		}

		return sectorToUpdate;
	}

	#region METHODS to get data to DB

	public void setDBToWeb(string methodToCall, int valueToResponse, Sector sector){

		//UserData tempUser = new UserData (player.id, player.cycle, game);
		string json = JsonUtility.ToJson (sector, true);
		UnityWebRequest postRequest = SetJsonForm (json, methodToCall);
		if (postRequest != null){
			switch(valueToResponse){
				case 1:

				StartCoroutine (waitDB_ToCreateSector (postRequest));

				break;

				case 3:

				StartCoroutine (waitDB_ToDeleteSector (postRequest));
				
				break;

				case 4:

				StartCoroutine (waitDB_ToUpdateSector (postRequest));
				
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

	IEnumerator waitDB_ToCreateSector (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseCreateSector resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseCreateSector> (www.downloadHandler.text);
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
					_sectorGetToDB = resp.sectorCreated;
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

	IEnumerator waitDB_ToDeleteSector (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseDeleteSector resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseDeleteSector> (www.downloadHandler.text);
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

	IEnumerator waitDB_ToUpdateSector (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseUpdateSector resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseUpdateSector> (www.downloadHandler.text);
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
           
			yield return (waitDB_ToGetSector (postRequest));
		
        }


	IEnumerator waitDB_ToGetSector (WWW www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseGetSectors resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseGetSectors> (www.text);
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
					_sectorsLoaded = resp.sectors;
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

