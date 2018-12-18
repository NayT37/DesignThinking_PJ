using SQLite4Unity3d;
using UnityEngine;
using System;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class StorytellingServices  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private MindmapServices _mindmapServices = new MindmapServices();

	private NoteServices _noteServices = new NoteServices();

	private StoryTelling _nullStorytelling = new StoryTelling{
				id = 0,
				percentage = 0,
				creationDate = "null",
				projectId = 0,
				lastUpdate = "null",
				version = 0
		};
	
	private int[] arrayversions = new int[]{1,2,3};

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

		//The identifier of the project is obtained to be able to pass 
		//it as an attribute in the new empathymap that will be created
		int projectid = DataBaseParametersCtrl.Ctrl._projectLoaded.id;

		//Get the current date to create the new storytelling
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		//Creation of the new storyTelling
		var new_s = new StoryTelling{
				percentage = 0,
				creationDate = "null",
				lastUpdate = "null",
				projectId = projectid,
				version = versionstorytelling
		};

		//Start-Validation that the query is right
		
		int result = _connection.Insert (new_s);

		int count_mindmap = 0;

		int count_notes = 0;

		int notescounter = 1;

		if (result != 0)
		{
			DataBaseParametersCtrl.Ctrl._storyTellingLoaded = new_s;

			for (int i = 0; i < notescounter; i++)
			{
				//Creation of the notes
				var n = _noteServices.CreateNote();

				if (n.id!=0)
					count_notes++;
			}

			for (int i = 0; i < 3; i++)
			{
				var m = _mindmapServices.CreateMindMap(arrayversions[i]);

				if (m.id != 0)
					count_mindmap++;
			}

			if (count_mindmap == 3 && count_notes==1)
				return new_s;
			else
				return _nullStorytelling;
			
			
		}else {
			return _nullStorytelling;
		}
		
		
	}

	/// <summary>
	/// Description to method Get StoryTelling with the specified projectId
	/// </summary>
	/// <param name="projectId">
	/// project identifier to find the correct storyTelling that will be searched
	/// </param>
	/// <returns>
	/// An object of type storyTelling with all the data of the storyTelling that was searched and if doesnt exist so return an empty storyTelling.
	/// </returns>
	public StoryTelling GetStoryTellingNamed( int projectId){
		
		var s = _connection.Table<StoryTelling>().Where(x => x.projectId == projectId).FirstOrDefault();

		if (s == null)
			return _nullStorytelling;	
		else 
			return s;
	}

	/// <summary>
	/// Description to method Get StoryTelling that contain in the DataBaseParametersCtrl.!-- _storyTellingLoaded
	/// </summary>
	/// <returns>
	/// An object of type storyTelling with all the data of the storyTelling that was searched and if doesnt exist so return an empty storyTelling.
	/// </returns>
	public StoryTelling GetStoryTellingNamed(){

		int projectId = DataBaseParametersCtrl.Ctrl._storyTellingLoaded.projectId;
		
		var s = _connection.Table<StoryTelling>().Where(x => x.projectId == projectId).FirstOrDefault();

		if (s == null)
			return _nullStorytelling;	
		else 
			return s;
	}

	/// <summary>
	/// Description of the method to obtain all the storyTellings of a specific project
	/// </summary>
	/// <param name="projectId">
	/// integer to define the identifier of the project from which all the related StoryTellings will be brought.
	/// <returns>
	/// A IEnumerable list of all the StoryTellings found from the identifier of the project that was passed as a parameter
	/// </returns>
	public IEnumerable<StoryTelling> GetStoryTellings(int projectId){
		return _connection.Table<StoryTelling>().Where(x => x.projectId == projectId);
	}

	/// <summary>
	/// (This is a test method) Description of the method to obtain all the StoryTellings
	/// </summary>
	/// <returns>
	/// A IEnumerable list of all the storyTellings found
	/// </returns>
	public IEnumerable<StoryTelling> GetStoryTellings(){
		return _connection.Table<StoryTelling>();
	}

	/// <summary>
	/// Description of the method to delete a empathymap
	/// </summary>
	/// <param name="storytellingToDelete">
	/// An object of type empathymap that contain the empathymap that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteEmpathymap(StoryTelling storytellingToDelete){

		return _connection.Delete(storytellingToDelete);
	}

	/// <summary>
	/// Description of the method to update a storyTelling
	/// </summary>
	/// <param name="storytellingToUpdate">
	/// An object of type storyTelling that contain the storyTelling that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateEmpathymap(StoryTelling storytellingToUpdate){
		return _connection.Update(storytellingToUpdate, storytellingToUpdate.GetType());
	}
}

