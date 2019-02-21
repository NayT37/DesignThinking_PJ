using SQLite4Unity3d;
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Networking;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class PublicServices:MonoBehaviour  {

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
	/// <param name="ageRange">
	/// Attribute that contains an string with the age's range of the public that will be created.
	/// </param>
	/// <param name="gender">
	/// Attribute that contains an string with the gender of the public that will be created.
	/// </param>
	/// <returns>
	/// An object of type _public with all the data of the _public that was created.
	/// </returns>

	public Public CreatePublic(string _agerange, string _gender){

		//valueToResponse = 1

		//The identifier of the project is obtained to be able to pass 
		//it as an attribute in the new public that will be created
		int projectid = DataBaseParametersCtrl.Ctrl._projectLoaded.id;

		//Get the current date to create the new public
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		//Creation of the new public
		var new_p = new Public{
				id = DataBaseParametersCtrl.Ctrl.GenerateCodeToId(),
				ageRange = _agerange,
				gender = _gender,
				percentage = 100,
				creationDate = date,
				lastUpdate = date,
				projectId = projectid
		};

		//Start-Validation that the query is right
		
		int result = _connection.Insert (new_p);

		if (result != 0)
		{
			DataBaseParametersCtrl.Ctrl._publicLoaded = new_p;
			return new_p;
		}else {
			return _nullPublic;
		}
		//End-Validation that the query
		
		
		
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
	public Public GetPublicNamed(int projectId){

		//valueToResponse = 2
		
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
	public int UpdatePublic(Public publicToUpdate){
		return _connection.Update(publicToUpdate, publicToUpdate.GetType());
	}
}

