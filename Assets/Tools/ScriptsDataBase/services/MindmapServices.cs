using SQLite4Unity3d;
using UnityEngine;
using System;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class MindmapServices  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private Mindmap _nullMindmap = new Mindmap{
				id = 0,
				percentage = 0,
				creationDate = "null",
				storyTellingId = 0,
				image = "null",
				lastUpdate = "null"			
		};
	


	/// <summary>
	/// Description to method to create a mindmap
	/// </summary>
	/// <param name="mindmap">
	/// Attribute that contains an object of type mindmap with all the data of the mindmap that will be created.
	/// </param>
	/// <returns>
	/// An object of type mindmap with all the data of the mindmap that was created.
	/// </returns>

	public Mindmap CreateMindMap(Mindmap mindmap){

		// var publicValidation = GetProblemNamed(mindmap.name, mindmap.storytellingId);

		// if ((publicValidation.name).Equals("null"))
		// {
			_connection.Insert (mindmap);
			return mindmap;
		// } else {
		// 	return _nullPublic;
		// }
		
		
	}

	/// <summary>
	/// Description to method Get Mindmap with the specified storytellingId
	/// </summary>
	/// <param name="storytellingId">
	/// storyTelling identifier to find the correct mindmap that will be searched
	/// </param>
	/// <returns>
	/// An object of type mindmap with all the data of the mindmap that was searched and if doesnt exist so return an empty mindmap.
	/// </returns>
	public Mindmap GetMindmapNamed( int storytellingId){
		
		var m = _connection.Table<Mindmap>().Where(x => x.storyTellingId == storytellingId).FirstOrDefault();

		if (m == null)
			return _nullMindmap;	
		else 
			return m;
	}

	/// <summary>
	/// Description to method Get Mindmap that contain in the DataBaseParametersCtrl.!-- _empathyMapLoaded
	/// </summary>
	/// <returns>
	/// An object of type mindmap with all the data of the mindmap that was searched and if doesnt exist so return an empty mindmap.
	/// </returns>
	public Mindmap GetMindmapNamed(){

		int storytellingId = DataBaseParametersCtrl.Ctrl._mindMapLoaded.storyTellingId;
		
		var m = _connection.Table<Mindmap>().Where(x => x.storyTellingId == storytellingId).FirstOrDefault();

		if (m == null)
			return _nullMindmap;	
		else 
			return m;
	}

	/// <summary>
	/// Description of the method to obtain all the notes of a specific project
	/// </summary>
	/// <param name="storytellingId">
	/// integer to define the identifier of the project from which all the related Notes will be brought.
	/// <returns>
	/// A IEnumerable list of all the Notes found from the identifier of the project that was passed as a parameter
	/// </returns>
	public IEnumerable<Mindmap> GetMindmaps(int storytellingId){
		return _connection.Table<Mindmap>().Where(x => x.storyTellingId == storytellingId);
	}

	/// <summary>
	/// (This is a test method) Description of the method to obtain all the Notes
	/// </summary>
	/// <returns>
	/// A IEnumerable list of all the notes found
	/// </returns>
	public IEnumerable<Mindmap> GetMindmaps(){
		return _connection.Table<Mindmap>();
	}

	/// <summary>
	/// Description of the method to delete a mindmap
	/// </summary>
	/// <param name="mindmapToDelete">
	/// An object of type mindmap that contain the mindmap that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteEmpathymap(Mindmap mindmapToDelete){

		return _connection.Delete(mindmapToDelete);
	}

	/// <summary>
	/// Description of the method to update a mindmap
	/// </summary>
	/// <param name="mindmapToUpdate">
	/// An object of type mindmap that contain the mindmap that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateEmpathymap(Mindmap mindmapToUpdate){
		return _connection.Update(mindmapToUpdate, mindmapToUpdate.GetType());
	}
}

