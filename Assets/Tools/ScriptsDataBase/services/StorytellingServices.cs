using SQLite4Unity3d;
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Networking;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class StorytellingServices:MonoBehaviour  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private MindmapServices _mindmapServices = new MindmapServices();

	private NoteServices _noteServices = new NoteServices();

	private StoryTelling _nullStorytelling = 
		new StoryTelling{
				id = 0,
				percentage = 0,
				creationDate = "null",
				projectId = 0,
				lastUpdate = "null",
				version = 0
		};
	
	private bool isQueryOk = false;

	private StoryTelling _storytellingGetToDB = new StoryTelling();

	private int resultToDB = 0;
	private int[] arrayversions = new int[]{1,2,3};

	private IEnumerable<StoryTelling> _storytellingsLoaded = new StoryTelling[]{
		new StoryTelling{
				id = 0,
				percentage = 0,
				creationDate = "null",
				projectId = 0,
				lastUpdate = "null",
				version = 0
		},
		new StoryTelling{
				id = 0,
				percentage = 0,
				creationDate = "null",
				projectId = 0,
				lastUpdate = "null",
				version = 0
		},
		new StoryTelling{
				id = 0,
				percentage = 0,
				creationDate = "null",
				projectId = 0,
				lastUpdate = "null",
				version = 0
		}
	};

	/// <summary>
	/// Description to method to create a storyTelling
	/// </summary>
	/// <param name="versionstorytelling">
	/// Attribute that contains an integer with version's value of the storyTelling that will be created.
	/// </param>
	/// <returns>
	/// An object of type storyTelling with all the data of the storyTelling that was created.
	/// </returns>

	public StoryTelling CreateStoryTelling(int versionstorytelling){

		//valueToResponse = 1

		//The identifier of the project is obtained to be able to pass 
		//it as an attribute in the new empathymap that will be created
		int projectid = DataBaseParametersCtrl.Ctrl._projectLoaded.id;

		//Get the current date to create the new storytelling
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		//Creation of the new storyTelling
		var new_s = new StoryTelling{
				percentage = 0,
				creationDate = date,	
				projectId = projectid,
				lastUpdate = date,
				version = versionstorytelling
		};

		//Start-Validation that the query is right
		
		int result = _connection.Insert (new_s);

		if (result != 0)
		{
			DataBaseParametersCtrl.Ctrl._storyTellingLoaded = new_s;

			var m = _mindmapServices.CreateMindMap(1);
		
			if (m.id != 0){
				Debug.Log(new_s);
				return new_s;
			}else{
				Debug.Log("+++");
				return _nullStorytelling;
			}
			
			
		}else {
			Debug.Log("...");
			return _nullStorytelling;
		}
		
		
	}

	/// <summary>
	/// Description of the method to obtain the average percentage of all the storytellings with specified project identifier
	/// </summary>
	/// <param name="storyTellingid">
	/// storytelling identifier to find the correct storytelling that will be searched
	/// </param>
	/// <returns>
	/// An integer with the average of all storytellings with specified project identifier
	/// </returns>
	public int GetStorytellingAverage(int projectid){
		
		var storytellings = _connection.Table<StoryTelling>().Where(x => x.projectId == projectid);
		int counter = 0;
		int sum = 0;
		int result = 0;

		foreach (var s in storytellings)
		{
			sum += s.percentage;
			counter++;
		}

		result = (sum/counter);
		return result;
	}

	/// <summary>
	/// Description of the method to obtain all the storyTellings of a specific project
	/// </summary>
	/// <param name="projectId">
	/// integer to define the identifier of the project from which all the related StoryTellings will be brought.
	/// <returns>
	/// A IEnumerable list of all the StoryTellings found from the identifier of the project that was passed as a parameter
	/// </returns>
	public IEnumerable<StoryTelling> GetStoryTellings(){

		//valueToResponse = 2

		int projectId = DataBaseParametersCtrl.Ctrl._projectLoaded.id;
		return _connection.Table<StoryTelling>().Where(x => x.projectId == projectId);
	}

	/// <summary>
	/// Description of the method to obtain all the storyTellings of a specific project
	/// </summary>
	/// <param name="projectId">
	/// integer to define the identifier of the project from which all the related StoryTellings will be brought.
	/// <returns>
	/// A IEnumerable list of all the StoryTellings found from the identifier of the project that was passed as a parameter
	/// </returns>
	public int GetStoryTellingsCounters(){

		//valueToResponse = 3

		int projectId = DataBaseParametersCtrl.Ctrl._projectLoaded.id;
		return _connection.Table<StoryTelling>().Where(x => x.projectId == projectId).Count();
	}

	/// <summary>
	/// Description of the method to delete a empathymap
	/// </summary>
	/// <param name="storytellingToDelete">
	/// An object of type empathymap that contain the empathymap that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteStoryTelling(StoryTelling storytellingToDelete){

		//valueToResponse = 4

		int storytellingid = storytellingToDelete.id;

		// All the notes belonging to the storytelling that will be deleted are obtained.
		var notes = _noteServices.GetNotes();

		// All the mindmaps belonging to the storytelling that will be deleted are obtained.
		var mindmaps = _mindmapServices.GetMindmaps(storytellingid);

		int result = _connection.Delete(storytellingToDelete);

		int valueToReturn = 0;

		//If the elimination of the empathymap is correct, then the notes corresponding to that empathymap are eliminated.
		if (result!=0)
		{
			foreach (var note in notes)
			{
				valueToReturn += _noteServices.DeleteNote(note);
			}

			foreach (var mindmap in mindmaps)
			{
				valueToReturn += _mindmapServices.DeleteMindmap(mindmap);
			}
			Debug.Log("Se borró el storytelling campo correctamente");
		} else {
			valueToReturn = 0;
			Debug.Log("No se borró el storytelling");
		}

		return valueToReturn;
	}

	/// <summary>
	/// Description of the method to delete a empathymap
	/// </summary>
	/// <param name="storytellingToDelete">
	/// An object of type empathymap that contain the empathymap that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteStoryTelling(){

		//valueToResponse = 4

		var storytellingToDelete = DataBaseParametersCtrl.Ctrl._storyTellingLoaded;

		int storytellingid = storytellingToDelete.id;

		// All the notes belonging to the storytelling that will be deleted are obtained.
		var notes = _noteServices.GetNotes();

		// All the mindmaps belonging to the storytelling that will be deleted are obtained.
		var mindmaps = _mindmapServices.GetMindmaps(storytellingid);

		int result = _connection.Delete(storytellingToDelete);

		int valueToReturn = 0;

		//If the elimination of the empathymap is correct, then the notes corresponding to that empathymap are eliminated.
		if (result!=0)
		{
			foreach (var note in notes)
			{
				valueToReturn += _noteServices.DeleteNote(note);
			}

			foreach (var mindmap in mindmaps)
			{
				valueToReturn += _mindmapServices.DeleteMindmap(mindmap);
			}
			Debug.Log("Se borró el storytelling campo correctamente");
		} else {
			valueToReturn = 0;
			Debug.Log("No se borró el storytelling");
		}

		return valueToReturn;
	}

	/// <summary>
	/// Description of the method to update a moment
	/// </summary>
	/// <param name="mindmapid">
	/// An integer that contain the identifier section that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateStoryTelling(StoryTelling storyTellingUpdate, int newVersion){

		//valueToResponse = 5

		int storytellingid = storyTellingUpdate.id;
		
		int counterNotes = _noteServices.GetNotesToPosition(storytellingid);

		storyTellingUpdate.version = newVersion;

		storyTellingUpdate.lastUpdate = DataBaseParametersCtrl.Ctrl.GetDateTime();

		int result = _connection.Update(storyTellingUpdate, storyTellingUpdate.GetType());

		if (result!=0)
		{
			Debug.Log(storyTellingUpdate);
		}

		return result;
	}

	/// <summary>
	/// Description of the method to update a moment
	/// </summary>
	/// <param name="mindmapid">
	/// An integer that contain the identifier section that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateStoryTelling(){

		var _publicServices = new PublicServices();

		var _projectServices = new ProjectServices();

		var storyTellingUpdate = DataBaseParametersCtrl.Ctrl._storyTellingLoaded;

		int storytellingid = storyTellingUpdate.id;
		
		int counterNotes = _noteServices.GetNotesToPosition(storytellingid);

		int averageToNotes = ((counterNotes*100)/6);

		int averageToMindmaps = _mindmapServices.GetMindmapsAverage(storytellingid);

		storyTellingUpdate.percentage = ((averageToNotes+averageToMindmaps)/2);

		storyTellingUpdate.lastUpdate = DataBaseParametersCtrl.Ctrl.GetDateTime();

		int result = _connection.Update(storyTellingUpdate, storyTellingUpdate.GetType());

		if (result!=0)
		{
			Debug.Log(storyTellingUpdate);
			_projectServices.UpdateProject(true);
		}

		return result;
	}

	#region METHODS to get data to DB

	public void setDBToWeb(string methodToCall, int valueToResponse, StoryTelling storytelling){

		//UserData tempUser = new UserData (player.id, player.cycle, game);
		string json = JsonUtility.ToJson (storytelling, true);
		UnityWebRequest postRequest = SetJsonForm (json, methodToCall);
		if (postRequest != null){
			switch(valueToResponse){
				case 1:

				StartCoroutine (waitDB_ToCreateStoryTelling (postRequest));

				break;

				case 4:

				StartCoroutine (waitDB_ToDeleteStoryTelling (postRequest));
				
				break;

				case 5:

				StartCoroutine (waitDB_ToUpdateStoryTelling (postRequest));
				
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

	IEnumerator waitDB_ToCreateStoryTelling (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseCreateStoryTelling resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseCreateStoryTelling> (www.downloadHandler.text);
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
					_storytellingGetToDB = resp.storytellingCreated;
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

	IEnumerator waitDB_ToDeleteStoryTelling (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseDeleteStorytelling resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseDeleteStorytelling> (www.downloadHandler.text);
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

	IEnumerator waitDB_ToUpdateStoryTelling (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseUpdateStorytelling resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseUpdateStorytelling> (www.downloadHandler.text);
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

				yield return (waitDB_ToGetStoryTellings (postRequest));
				
				break;

				case 3:

				yield return (waitDB_ToGetStoryTellingsCounter (postRequest));
				
				break;
			}
        }

	IEnumerator waitDB_ToGetStoryTellings (WWW www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseGetStoryTellings resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseGetStoryTellings> (www.text);
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
					_storytellingsLoaded = resp.storytellings;
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

	IEnumerator waitDB_ToGetStoryTellingsCounter (WWW www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseGetStoryTellingsCounter resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseGetStoryTellingsCounter> (www.text);
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

