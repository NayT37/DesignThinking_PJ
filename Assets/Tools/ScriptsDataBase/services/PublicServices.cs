using SQLite4Unity3d;
using UnityEngine;
using System;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class PublicServices  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private Public _nullPublic = new Public{
				id = 0,
				ageRange = "null",
				gender = "null",
				percentage = 0,
				creationDate = "null",
				lastUpdate = "null",
				projectId = 0
		};
	


	/// <summary>
	/// Description to method to create a _public
	/// </summary>
	/// <param name="_public">
	/// Attribute that contains an object of type _public with all the data of the _public that will be created.
	/// </param>
	/// <returns>
	/// An object of type _public with all the data of the _public that was created.
	/// </returns>

	public Public CreatePublic(Public _public){

		// var publicValidation = GetProblemNamed(_public.name, _public.projectId);

		// if ((publicValidation.name).Equals("null"))
		// {
			_connection.Insert (_public);
			return _public;
		// } else {
		// 	return _nullPublic;
		// }
		
		
	}

	/// <summary>
	/// Description to method Get Public with the specified projectId
	/// </summary>}
	/// <param name="projectId">
	/// project identifier to find the correct _public that will be searched
	/// </param>
	/// <returns>
	/// An object of type _public with all the data of the _public that was searched and if doesnt exist so return an empty _public.
	/// </returns>
	public Public GetPublicNamed( int projectId){
		
		var p = _connection.Table<Public>().Where(x => x.projectId == projectId).FirstOrDefault();

		if (p == null)
			return _nullPublic;	
		else 
			return p;
	}

	/// <summary>
	/// Description to method Get Public that contain in the DataBaseParametersCtrl.!-- _publicLoaded
	/// </summary>
	/// <returns>
	/// An object of type _public with all the data of the _public that was searched and if doesnt exist so return an empty _public.
	/// </returns>
	public Public GetPublicNamed(){

		int projectId = DataBaseParametersCtrl.Ctrl._publicLoaded.projectId;
		
		var p = _connection.Table<Public>().Where(x => x.projectId == projectId).FirstOrDefault();

		if (p == null)
			return _nullPublic;	
		else 
			return p;
	}

	/// <summary>
	/// Description of the method to delete a public
	/// </summary>
	/// <param name="publicToDelete">
	/// An object of type public that contain the public that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeletePublic(Public publicToDelete){

		//Se debe tener el cuenta que al eliminar un proyecto de debe eliminar 
		//todo lo que continua hacia abajo en la jerarquia de la base de datos (problema, publico, etc)
		return _connection.Delete(publicToDelete);
	}

	/// <summary>
	/// Description of the method to update a _public
	/// </summary>
	/// <param name="publicToUpdate">
	/// An object of type _public that contain the _public that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateProblem(Public publicToUpdate){
		return _connection.Update(publicToUpdate, publicToUpdate.GetType());
	}
}

