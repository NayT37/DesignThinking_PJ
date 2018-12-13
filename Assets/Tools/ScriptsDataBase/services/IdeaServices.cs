using SQLite4Unity3d;
using UnityEngine;
using System;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class IdeaServices  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private Idea _nullIdea = new Idea{
				id = 0,
				creationDate = "null",
				description = "null",
				sectorId = 0,
				lastUpdate = "null"
		};
	


	/// <summary>
	/// Description to method to create a idea
	/// </summary>
	/// <param name="idea">
	/// Attribute that contains an object of type idea with all the data of the idea that will be created.
	/// </param>
	/// <returns>
	/// An object of type idea with all the data of the idea that was created.
	/// </returns>

	public Idea CreateIdea(Idea idea){

		// var publicValidation = GetProblemNamed(idea.name, idea.sectorId);

		// if ((publicValidation.name).Equals("null"))
		// {
			_connection.Insert (idea);
			return idea;
		// } else {
		// 	return _nullPublic;
		// }
		
		
	}

	/// <summary>
	/// Description of the method to obtain all the sectors of a specific empathyMap
	/// </summary>
	/// <param name="sectorId">
	/// integer to define the identifier of the sector from which all the related ideas will be brought.
	/// <returns>
	/// A IEnumerable list of all the ideas found from the identifier of the empathyMap that was passed as a parameter
	/// </returns>
	public IEnumerable<Idea> GetIdeas(int sectorId){
		return _connection.Table<Idea>().Where(x => x.sectorId == sectorId);
	}

	/// <summary>
	/// (This is a test method) Description of the method to obtain all the Ideas
	/// </summary>
	/// <returns>
	/// A IEnumerable list of all the ideas found
	/// </returns>
	public IEnumerable<Idea> GetIdeas(){
		return _connection.Table<Idea>();
	}

	/// <summary>
	/// Description of the method to delete a empathymap
	/// </summary>
	/// <param name="ideaToDelete">
	/// An object of type empathymap that contain the empathymap that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteSector(Idea ideaToDelete){

		return _connection.Delete(ideaToDelete);
	}

	/// <summary>
	/// Description of the method to update a idea
	/// </summary>
	/// <param name="ideaToUpdate">
	/// An object of type idea that contain the idea that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateEmpathymap(Idea ideaToUpdate){
		return _connection.Update(ideaToUpdate, ideaToUpdate.GetType());
	}
}

