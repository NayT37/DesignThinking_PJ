using SQLite4Unity3d;
using UnityEngine;
using System;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class NoteServices  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private Note _nullNote = new Note{
				id = 0,
				position = 0,
				creationDate = "null",
				description = "null",
				storyTellingId = 0,
				lastUpdate = "null"			
		};
	


	/// <summary>
	/// Description to method to create a note
	/// </summary>
	/// <returns>
	/// An object of type note with all the data of the note that was created.
	/// </returns>

	public Note CreateNote(){

		//The identifier of the storytellind is obtained to be able to pass 
		//it as an attribute in the new note that will be created
		int storytellingid = DataBaseParametersCtrl.Ctrl._storyTellingLoaded.id;

		//Get the current date to create the new empathymap
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		int valueToReturn = 0;

		//Creation of the new storyTelling
		var new_n = new Note{
				position = 0,
				creationDate = date,
				description = "",
				storyTellingId = storytellingid,
				lastUpdate = date		
		};

		//Start-Validation that the query is right
		
		int result = _connection.Insert (new_n);

		if (result != 0)
		{
			valueToReturn = result;
			DataBaseParametersCtrl.Ctrl._noteLoaded = new_n;
		
			return new_n;
		}else {
			return _nullNote;
		}
		
	}

	/// <summary>
	/// Description to method Get Note with the specified storytellingId
	/// </summary>
	/// <param name="storytellingId">
	/// storyTelling identifier to find the correct note that will be searched
	/// </param>
	/// <returns>
	/// An object of type note with all the data of the note that was searched and if doesnt exist so return an empty note.
	/// </returns>
	public Note GetNoteNamed( int storytellingId){
		
		var n = _connection.Table<Note>().Where(x => x.storyTellingId == storytellingId).FirstOrDefault();

		if (n == null)
			return _nullNote;	
		else 
			return n;
	}

	/// <summary>
	/// Description to method Get Note that contain in the DataBaseParametersCtrl.!-- _empathyMapLoaded
	/// </summary>
	/// <returns>
	/// An object of type note with all the data of the note that was searched and if doesnt exist so return an empty note.
	/// </returns>
	public Note GetNoteNamed(){

		int storytellingId = DataBaseParametersCtrl.Ctrl._noteLoaded.storyTellingId;
		
		var n = _connection.Table<Note>().Where(x => x.storyTellingId == storytellingId).FirstOrDefault();

		if (n == null)
			return _nullNote;	
		else 
			return n;
	}

	/// <summary>
	/// Description of the method to obtain all the notes of a specific project
	/// </summary>
	/// <param name="storytellingId">
	/// integer to define the identifier of the project from which all the related Notes will be brought.
	/// <returns>
	/// A IEnumerable list of all the Notes found from the identifier of the project that was passed as a parameter
	/// </returns>
	public IEnumerable<Note> GetNotes(int storytellingId){
		return _connection.Table<Note>().Where(x => x.storyTellingId == storytellingId);
	}

	/// <summary>
	/// (This is a test method) Description of the method to obtain all the Notes
	/// </summary>
	/// <returns>
	/// A IEnumerable list of all the notes found
	/// </returns>
	public IEnumerable<Note> GetNotes(){
		return _connection.Table<Note>();
	}

	/// <summary>
	/// Description of the method to delete a note
	/// </summary>
	/// <param name="noteToDelete">
	/// An object of type note that contain the note that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteEmpathymap(Note noteToDelete){

		return _connection.Delete(noteToDelete);
	}

	/// <summary>
	/// Description of the method to update a note
	/// </summary>
	/// <param name="noteToUpdate">
	/// An object of type note that contain the note that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateEmpathymap(Note noteToUpdate){
		return _connection.Update(noteToUpdate, noteToUpdate.GetType());
	}
}

