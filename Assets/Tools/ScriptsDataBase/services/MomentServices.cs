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

public class MomentServices:MonoBehaviour  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private Moment _nullMoment = 
		new Moment{
				id = 0,
				name = "null",
				percentage = 0,
				creationDate = "null",
				caseId = 0,
				lastUpdate = "null"
		};

	private IEnumerable<Moment> _momentsLoaded = new Moment[]{
		new Moment{
				id = 0,
				name = "null",
				percentage = 0,
				creationDate = "null",
				caseId = 0,
				lastUpdate = "null"
		},
		new Moment{
				id = 0,
				name = "null",
				percentage = 0,
				creationDate = "null",
				caseId = 0,
				lastUpdate = "null"
		},
		new Moment{
				id = 0,
				name = "null",
				percentage = 0,
				creationDate = "null",
				caseId = 0,
				lastUpdate = "null"
		},
		new Moment{
				id = 0,
				name = "null",
				percentage = 0,
				creationDate = "null",
				caseId = 0,
				lastUpdate = "null"
		}
	};


	/// <summary>
	/// Description to method to create a moment.
	/// </summary>
	/// <param name="momentname">
	/// Attribute that contains an string with the name of the moment that will be created.
	/// </param>
	/// /// <param name="caseid">
	/// Attribute that contains case identifier to the moment that will be created.
	/// </param>
	/// <returns>
	/// An integer response of the query (0 = the object was not created correctly. !0 = the object was created correctly)
	/// </returns>

	public int CreateMoment(string momentname, Int64 caseid){

		//valueToResponse = 1

		int valueToReturn = 0;

		//Get the current date to create the new group
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		 var new_m = new Moment{
				id = DataBaseParametersCtrl.Ctrl.GenerateCodeToId(),
				name = momentname,
				percentage = 0,
				creationDate = date,
				caseId = caseid,
				lastUpdate = date
		};

		int result = _connection.Insert (new_m);
		
		return valueToReturn;
		
	}

	/// <summary>
	/// Description of the method to obtain all the moments of a specific case
	/// </summary>
	/// <param name="caseId">
	/// integer to define the identifier of the case from which all the related moments will be brought.
	/// <returns>
	/// A IEnumerable list of all the moments found from the identifier of the case that was passed as a parameter
	/// </returns>
	public IEnumerable<Moment> GetMoments(Int64 caseId){
		//valueToResponse = 2
		return _connection.Query<Moment> ("select * from Moment where caseId = " + caseId +" ORDER BY creationDate ASC");
	}

	public IEnumerable<Moment> GetAllMoments(){

		//valueToResponse = 2 

		return _connection.Table<Moment>().Where(x => x.id.ToString().StartsWith(DataBaseParametersCtrl.Ctrl._teacherLoggedIn.identityCard));
	}

	/// <summary>
	/// Description of the method to delete a moment
	/// </summary>
	/// <param name="momentToDelete">
	/// An object of type moment that contain the moment that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteMoment(Moment momentToDelete){

		//valueToResponse = 3

		int result = _connection.Delete(momentToDelete);

		return result;
	}

	/// <summary>
	/// Description of the method to update a moment
	/// </summary>
	/// <param name="newpercentage">
	/// An integer that contain the new percetage that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateMoment(int newpercentage){

		//valueToResponse = 4
		
		var momentToUpdate = DataBaseParametersCtrl.Ctrl._momentLoaded;
		
		var _caseServices = new CaseServices();

		momentToUpdate.percentage = newpercentage;
		momentToUpdate.lastUpdate = DataBaseParametersCtrl.Ctrl.GetDateTime();

		int result = _connection.Update(momentToUpdate, momentToUpdate.GetType());

		if (result!=0)
		{
			_caseServices.UpdateCase(momentToUpdate.caseId);
		}

		return result;
	}
}

