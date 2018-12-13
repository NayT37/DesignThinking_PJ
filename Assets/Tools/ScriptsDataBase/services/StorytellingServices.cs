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

	private StoryTelling _nullStorytelling = new StoryTelling{
				id = 0,
				percentage = 0,
				creationDate = "null",
				lastUpdate = "null",
				projectId = 0
		};
	


	/// <summary>
	/// Description to method to create a storyTelling
	/// </summary>
	/// <param name="storyTelling">
	/// Attribute that contains an object of type storyTelling with all the data of the storyTelling that will be created.
	/// </param>
	/// <returns>
	/// An object of type storyTelling with all the data of the storyTelling that was created.
	/// </returns>

	public StoryTelling CreateStoryTelling(StoryTelling storyTelling){

		// var publicValidation = GetProblemNamed(storyTelling.name, storyTelling.projectId);

		// if ((publicValidation.name).Equals("null"))
		// {
			_connection.Insert (storyTelling);
			return storyTelling;
		// } else {
		// 	return _nullPublic;
		// }
		
		
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

