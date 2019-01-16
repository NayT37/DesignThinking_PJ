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

public class SectionServices:MonoBehaviour  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private NodeServices _nodeServices = new NodeServices();

	private Section _nullSection = 
		new Section{
				id = 0,
				name = "null",
				creationDate = "null",
				mindmapId = 0,
				isOptional = false,
				lastUpdate = "null"			
		};
	

	private bool isQueryOk = false;

	private Section _sectionGetToDB = new Section();

	private int resultToDB = 0;

	private IEnumerable<Section> _sectionsLoaded = new Section[]{
		new Section{
				id = 0,
				name = "null",
				creationDate = "null",
				mindmapId = 0,
				isOptional = false,
				lastUpdate = "null"			
		},
		new Section{
				id = 0,
				name = "null",
				creationDate = "null",
				mindmapId = 0,
				isOptional = false,
				lastUpdate = "null"			
		},
		new Section{
				id = 0,
				name = "null",
				creationDate = "null",
				mindmapId = 0,
				isOptional = false,
				lastUpdate = "null"			
		},
		new Section{
				id = 0,
				name = "null",
				creationDate = "null",
				mindmapId = 0,
				isOptional = false,
				lastUpdate = "null"			
		}
	};

	/// <summary>
	/// Description to method to create a section
	/// </summary>
	/// <param name="sectionname">
	/// Attribute that contains an string with the name of the section that will be created.
	/// </param>
	/// <returns>
	/// An object of type section with all the data of the section that was created.
	/// </returns>

	public Section CreateSection(string sectionname){

		//valueToResponse = 1

		//The identifier of the mindmap is obtained to be able to pass 
		//it as an attribute in the new section that will be created
		int mindmapid = DataBaseParametersCtrl.Ctrl._mindMapLoaded.id;

		//Get the current date to create the new section
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		//Creation of the new section
		var new_s = new Section{
				name = sectionname,
				creationDate = date,
				mindmapId = mindmapid,
				isOptional = false,
				lastUpdate = date			
		};

		//Start-Validation that the query is right
		
		int result = _connection.Insert (new_s);

		int count = 0;

		if (result != 0)
		{
			DataBaseParametersCtrl.Ctrl._sectionLoaded = new_s;
		
				for (int i = 0; i < 3; i++)
				{
					var e = _nodeServices.CreateNode();

					if (e.id != 0)
						count++;
				}

				if (count == 3){
					Debug.Log(new_s);
					return new_s;
				}else
					return _nullSection;
		}else
			return _nullSection;
	
		//End-Validation that the query		
		
		
	}

	/// <summary>
	/// Description to method Get Section with the specified mindmapId
	/// </summary>
	/// <param name="sectionid">
	/// section identifier to find the correct section that will be searched
	/// </param>
	/// <returns>
	/// An object of type section with all the data of the section that was searched and if doesnt exist so return an empty section.
	/// </returns>
	public Section GetSectionId( int sectionid){
		
		var s = _connection.Table<Section>().Where(x => x.id == sectionid).FirstOrDefault();

		if (s == null)
			return _nullSection;	
		else 
			return s;
	}

	/// <summary>
	/// Description to method Get Section with the specified mindmapId
	/// </summary>
	/// <param name="mindmapid">
	/// mindmap identifier to find the correct section that will be searched
	/// </param>
	/// <returns>
	/// An object of type section with all the data of the section that was searched and if doesnt exist so return an empty section.
	/// </returns>
	public int GetSectionByAverage(int mindmapid){
		
		var sections = _connection.Table<Section>().Where(x => x.mindmapId == mindmapid).Where(x => x.name.StartsWith("-"));
		int counter = 0;

		foreach (var s in sections)
		{
			var nodes = _nodeServices.GetNodes(s.id);

			foreach (var node in nodes)
			{
				if (!node.description.Equals("")){
					counter++;
				}
			}
		}
		
		return counter;
	}


	/// <summary>
	/// Description of the method to obtain all the sections of a specific mindMap
	/// </summary>
	/// <param name="mindmapId">
	/// integer to define the identifier of the mindMap from which all the related Sections will be brought.
	/// <returns>
	/// A IEnumerable list of all the Sections found from the identifier of the mindMap that was passed as a parameter
	/// </returns>
	public IEnumerable<Section> GetSections(int mindmapId){
		return _connection.Table<Section>().Where(x => x.mindmapId == mindmapId);
	}

	/// <summary>
	/// (This is a test method) Description of the method to obtain all the Sections
	/// </summary>
	/// <returns>
	/// A IEnumerable list of all the sections found
	/// </returns>
	public IEnumerable<Section> GetSections(){

		//valueToResponse = 2

		return _connection.Table<Section>();
	}

	/// <summary>
	/// Description of the method to delete a section
	/// </summary>
	/// <param name="sectionToDelete">
	/// An object of type section that contain the section that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteSection(Section sectionToDelete){

		//valueToResponse = 3

		int sectionid = sectionToDelete.id;

		int result = _connection.Delete(sectionToDelete);

		int valueToReturn = 0;

		//If the elimination of the section is correct, then the nodes corresponding to that empathymap are eliminated.
		if (result!=0)
		{
			// All the nodes belonging to the section that will be deleted are obtained.
			var nodes = _nodeServices.GetNodes(sectionid);

			foreach (var node in nodes)
			{
				valueToReturn += _nodeServices.DeleteNode(node);
			}
			Debug.Log("Se borró la sección correctamente");
		} else {
			valueToReturn = 0;
			Debug.Log("No se borró la sección");
		}

		return valueToReturn;
	}

	/// <summary>
	/// Description of the method to update a moment
	/// </summary>
	/// <param name="sectionid">
	/// An integer that contain the identifier section that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateSection(int sectionid){

		var sectionToUpdate = GetSectionId(sectionid);

		var _mindmapServices = new MindmapServices();

		sectionToUpdate.lastUpdate = DataBaseParametersCtrl.Ctrl.GetDateTime();

		int result = _connection.Update(sectionToUpdate, sectionToUpdate.GetType());

		if (result!=0)
		{
			_mindmapServices.UpdateMindmap(sectionToUpdate.mindmapId);
		}

		return result;
	}

	#region METHODS to get data to DB

	public void setDBToWeb(string methodToCall, int valueToResponse, Section section){

		//UserData tempUser = new UserData (player.id, player.cycle, game);
		string json = JsonUtility.ToJson (section, true);
		UnityWebRequest postRequest = SetJsonForm (json, methodToCall);
		if (postRequest != null){
			switch(valueToResponse){
				case 1:

				StartCoroutine (waitDB_ToCreateSection (postRequest));

				break;

				case 3:

				StartCoroutine (waitDB_ToDeleteSection (postRequest));
				
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

	IEnumerator waitDB_ToCreateSection (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseCreateSection resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseCreateSection> (www.downloadHandler.text);
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
					_sectionGetToDB = resp.sectionCreated;
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

	IEnumerator waitDB_ToDeleteSection (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseDeleteSection resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseDeleteSection> (www.downloadHandler.text);
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
           
			yield return (waitDB_ToGetSections (postRequest));
		
        }


	IEnumerator waitDB_ToGetSections (WWW www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseGetSections resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseGetSections> (www.text);
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
					_sectionsLoaded = resp.sections;
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

