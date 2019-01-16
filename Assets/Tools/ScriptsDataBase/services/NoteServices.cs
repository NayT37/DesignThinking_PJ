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

public class NoteServices:MonoBehaviour  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private Note _nullNote = 
		new Note{
				id = 0,
				position = 0,
				creationDate = "null",
				description = "null",
				storytellingId = 0,
				lastUpdate = "null"			
		};
	

	private bool isQueryOk = false;

	private Note _noteGetToDB = new Note();

	private int resultToDB = 0;

	private IEnumerable<Note> _notesLoaded = new Note[]{
		new Note{
				id = 0,
				position = 0,
				creationDate = "null",
				description = "null",
				storytellingId = 0,
				lastUpdate = "null"			
		},
		new Note{
				id = 0,
				position = 0,
				creationDate = "null",
				description = "null",
				storytellingId = 0,
				lastUpdate = "null"			
		},
		new Note{
				id = 0,
				position = 0,
				creationDate = "null",
				description = "null",
				storytellingId = 0,
				lastUpdate = "null"			
		}
	};

	/// <summary>
	/// Description to method to create a note
	/// </summary>
	/// <param name="notedescription">
	/// Attribute that contains an string with the note description of the note that will be created.
	/// </param>
	/// <returns>
	/// An object of type note with all the data of the note that was created.
	/// </returns>

	public Note CreateNote(string notedescription){

		//valueToResponse = 1

		//The identifier of the storytellind is obtained to be able to pass 
		//it as an attribute in the new note that will be created
		int storytellingid = DataBaseParametersCtrl.Ctrl._storyTellingLoaded.id;

		//Get the current date to create the new empathymap
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		int valueToReturn = 0;

		//Creation of the new storyTelling
		var new_n = new Note{
				position = 1,
				creationDate = date,
				description = notedescription,
				storytellingId = storytellingid,
				lastUpdate = date		
		};

		//Start-Validation that the query is right
		
		int result = _connection.Insert (new_n);

		if (result != 0)
		{
			valueToReturn = result;
			DataBaseParametersCtrl.Ctrl._noteLoaded = new_n;
			Debug.Log(new_n);
			return new_n;
		}else {
			return _nullNote;
		}
		
	}

	/// <summary>
	/// Description of the method to obtain all the notes of a specific project
	/// </summary>
	/// <param name="storytellingId">
	/// integer to define the identifier of the project from which all the related Notes will be brought.
	/// <returns>
	/// A IEnumerable list of all the Notes found from the identifier of the project that was passed as a parameter
	/// </returns>
	public IEnumerable<Note> GetNotes(){

		//valueToResponse = 2 

		int storytellingId = DataBaseParametersCtrl.Ctrl._storyTellingLoaded.id;
		return _connection.Table<Note>().Where(x => x.storytellingId == storytellingId);
	}

	/// <summary>
	/// Description of the method to obtain all the notes of a specific project
	/// </summary>
	/// <param name="storytellingId">
	/// integer to define the identifier of the project from which all the related Notes will be brought.
	/// <returns>
	/// A IEnumerable list of all the Notes found from the identifier of the project that was passed as a parameter
	/// </returns>
	public int GetNotesCounter(){

		//valueToResponse = 3

		int storytellingId = DataBaseParametersCtrl.Ctrl._storyTellingLoaded.id;
		return _connection.Table<Note>().Where(x => x.storytellingId == storytellingId).Count();
	}

	/// <summary>
	/// Description to method Get Section with the specified storyTellingid
	/// </summary>
	/// <param name="storyTellingid">
	/// storytelling identifier to find the correct note that will be searched
	/// </param>
	/// <returns>
	/// An object of type section with all the data of the section that was searched and if doesnt exist so return an empty section.
	/// </returns>
	public int GetNotesToPosition(int storyTellingid){
		
		int counter = _connection.Table<Note>().Where(x => x.storytellingId == storyTellingid).Where(x => x.position != 0).Count();
		return counter;
	}

	/// <summary>
	/// Description of the method to delete a note
	/// </summary>
	/// <param name="noteToDelete">
	/// An object of type note that contain the note that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteNote(Note noteToDelete){

		//valueToResponse = 4
		
		int result = _connection.Delete(noteToDelete);

		if (result!=0)
			Debug.Log("Se borró el campo correctamente");
		else
			Debug.Log("No se borró el campo");
		
		return result;
	}

	/// <summary>
	/// Description of the method to update a note
	/// </summary>
	/// <param name="noteToUpdate">
	/// Attribute that contains an object type of note with the object note that will be created.
	/// <returns>
	/// <param name="newdescription">
	/// Attribute that contains an string with the new description of the note that will be created.
	/// <returns>
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateNote(int position, string newdescription){

		//valueToResponse = 5

		var noteToUpdate = DataBaseParametersCtrl.Ctrl._noteLoaded;
		
		var storytellingServices = new StorytellingServices();

		noteToUpdate.lastUpdate = DataBaseParametersCtrl.Ctrl.GetDateTime();

		if (!newdescription.Equals(""))
		{
			noteToUpdate.description = newdescription;
		} 

		noteToUpdate.position = position;

		int result = _connection.Update(noteToUpdate, noteToUpdate.GetType());

		if (result!=0)
		{
			storytellingServices.UpdateStoryTelling();
		}

		return result;
	}

	#region METHODS to get data to DB

	public void setDBToWeb(string methodToCall, int valueToResponse, Note node){

		//UserData tempUser = new UserData (player.id, player.cycle, game);
		string json = JsonUtility.ToJson (node, true);
		UnityWebRequest postRequest = SetJsonForm (json, methodToCall);
		if (postRequest != null){
			switch(valueToResponse){
				case 1:

				StartCoroutine (waitDB_ToCreateNote (postRequest));

				break;

				case 4:

				StartCoroutine (waitDB_ToDeleteNote (postRequest));
				
				break;

				case 5:

				StartCoroutine (waitDB_ToUpdateNote (postRequest));
				
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

	IEnumerator waitDB_ToCreateNote (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseCreateNote resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseCreateNote> (www.downloadHandler.text);
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
					_noteGetToDB = resp.noteCreated;
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

	IEnumerator waitDB_ToDeleteNote (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseDeleteNote resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseDeleteNote> (www.downloadHandler.text);
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

	IEnumerator waitDB_ToUpdateNote (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseUpdateNote resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseUpdateNote> (www.downloadHandler.text);
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

				yield return  (waitDB_ToGetNotes (postRequest));

				break;

				case 3:

				yield return  (waitDB_ToGetNotesCounter (postRequest));
				
				break;

			}
		
        }


	IEnumerator waitDB_ToGetNotes (WWW www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseGetNotes resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseGetNotes> (www.text);
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
					_notesLoaded = resp.notes;
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

		IEnumerator waitDB_ToGetNotesCounter (WWW www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseGetNotesCounter resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseGetNotesCounter> (www.text);
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

