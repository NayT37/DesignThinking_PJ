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
	/// <param name="notedescription">
	/// Attribute that contains an string with the note description of the note that will be created.
	/// </param>
	/// <returns>
	/// An object of type note with all the data of the note that was created.
	/// </returns>

	public Note CreateNote(string notedescription){

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
				description = notedescription,
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
	/// Description to method Get Section with the specified storyTellingid
	/// </summary>
	/// <param name="storyTellingid">
	/// storytelling identifier to find the correct note that will be searched
	/// </param>
	/// <returns>
	/// An object of type section with all the data of the section that was searched and if doesnt exist so return an empty section.
	/// </returns>
	public int GetNotesToPosition(int storyTellingid){
		
		int counter = _connection.Table<Note>().Where(x => x.storyTellingId == storyTellingid).Where(x => x.position != 0).Count();
		return counter;
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
	public int DeleteNote(Note noteToDelete){

		return _connection.Delete(noteToDelete);
	}

	/// <summary>
	/// Description of the method to update a note
	/// </summary>
	/// <param name="positionToUpdate">
	/// Attribute that contains an integer with the position of the note that will be created.
	/// <returns>
	/// <param name="newdescription">
	/// Attribute that contains an string with the new description of the note that will be created.
	/// <returns>
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateNote(int positionToUpdate, string newdescription){

		var noteToUpdate = DataBaseParametersCtrl.Ctrl._noteLoaded;

		var storytellingServices = new StorytellingServices();

		noteToUpdate.lastUpdate = DataBaseParametersCtrl.Ctrl.GetDateTime();

		if (positionToUpdate == 0)
			noteToUpdate.description = newdescription;
		else 
			noteToUpdate.position = positionToUpdate;

		int result = _connection.Update(noteToUpdate, noteToUpdate.GetType());

		if (result!=0)
		{
			storytellingServices.UpdateStoryTelling();
		}

		return result;
	}
}

