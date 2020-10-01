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
	private Int64 checkId;
	public Note CreateNote(string notedescription){

		//valueToResponse = 1

		//The identifier of the storytellind is obtained to be able to pass 
		//it as an attribute in the new note that will be created
		Int64 storytellingid = DataBaseParametersCtrl.Ctrl._storyTellingLoaded.id;

		//Get the current date to create the new empathymap
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		int valueToReturn = 0;

		//Creation of the new storyTelling
		var new_n = new Note{
				id = DataBaseParametersCtrl.Ctrl.GenerateCodeToId(),
				position = 0,
				creationDate = date,
				description = notedescription,
				storytellingId = storytellingid,
				lastUpdate = date		
		};

		//Start-Validation that the query is right
		checkId = new_n.id;
        while (GetNoteId(checkId).id == new_n.id)
        {
            new_n.id = DataBaseParametersCtrl.Ctrl.GenerateCodeToId();
        }
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

	public Note GetNoteId(Int64 noteid)
    {

        //valueToResponse = 2

        var n = _connection.Table<Note>().Where(x => x.id == noteid).FirstOrDefault();

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
	public IEnumerable<Note> GetNotes(){

		//valueToResponse = 2 

		Int64 storytellingId = DataBaseParametersCtrl.Ctrl._storyTellingLoaded.id;
		return _connection.Query<Note> ("select * from Note where storytellingId = " + storytellingId +" ORDER BY creationDate ASC");
	}

	public IEnumerable<Note> GetAllNotes(){

		//valueToResponse = 2 

		return _connection.Query<Note> ("select * from Note where id LIKE '%" + DataBaseParametersCtrl.Ctrl._teacherLoggedIn.identityCard +"%' ORDER BY creationDate ASC");
	}

	public int GetAllNotesCount(){

		//valueToResponse = 2 

		return _connection.Query<Note> ("select * from Note where id LIKE '%" + DataBaseParametersCtrl.Ctrl._teacherLoggedIn.identityCard +"%' ORDER BY creationDate ASC").Count;
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

		Int64 storytellingId = DataBaseParametersCtrl.Ctrl._storyTellingLoaded.id;
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
	public int GetNotesToPosition(Int64 storyTellingid){
		
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

}

