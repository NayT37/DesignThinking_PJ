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
}

