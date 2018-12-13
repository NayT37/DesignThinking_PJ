using SQLite4Unity3d;
using UnityEngine;
using System;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class MomentServices  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private Moment _nullMoment = new Moment{
				id = 0,
				name = "null",
				percentage = 0,
				creationDate = "null",
				caseId = 0,
				lastUpdate = "null"
		};
	


	/// <summary>
	/// Description to method to create a moment
	/// </summary>
	/// <param name="moment">
	/// Attribute that contains an object of type moment with all the data of the moment that will be created.
	/// </param>
	/// <returns>
	/// An object of type moment with all the data of the moment that was created.
	/// </returns>

	public Moment Createcase(Moment moment){

		//var trainingValidation = GetTrainingNamed(moment.name, moment.caseId);

		// if ((trainingValidation.name).Equals("null"))
		// {
			_connection.Insert (moment);
			return moment;
		// } else {
		// 	return _nullTraining;
		// }
		
		
	}

	/// <summary>
	/// Description to method Get Moment that contain in the DataBaseParametersCtrl.!-- _trainingLoaded
	/// </summary>
	/// <returns>
	/// An object of type moment with all the data of the moment that was searched and if doesnt exist so return an empty moment.
	/// </returns>
	public Moment GetMomentNamed(){

		string momentName = DataBaseParametersCtrl.Ctrl._momentLoaded.name;
		int caseId = DataBaseParametersCtrl.Ctrl._momentLoaded.caseId;
		
		var m = _connection.Table<Moment>().Where(x => x.name == momentName).Where(x => x.caseId == caseId).FirstOrDefault();

		if (m == null)
			return _nullMoment;	
		else 
			return m;
	}

	/// <summary>
	/// Description of the method to obtain all the moments of a specific case
	/// </summary>
	/// <param name="caseId">
	/// integer to define the identifier of the case from which all the related moments will be brought.
	/// <returns>
	/// A IEnumerable list of all the moments found from the identifier of the case that was passed as a parameter
	/// </returns>
	public IEnumerable<Moment> GetMoments(int caseId){
		return _connection.Table<Moment>().Where(x => x.caseId == caseId);
	}

	/// <summary>
	/// (This is a test method) Description of the method to obtain all the Cases
	/// </summary>
	/// <returns>
	/// A IEnumerable list of all the cases found
	/// </returns>
	public IEnumerable<Moment> GetMoments(){
		return _connection.Table<Moment>();
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
		return _connection.Delete(momentToDelete);
	}

	/// <summary>
	/// Description of the method to update a moment
	/// </summary>
	/// <param name="momentToUpdate">
	/// An object of type moment that contain the moment that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateMoment(Moment momentToUpdate){
		return _connection.Update(momentToUpdate, momentToUpdate.GetType());
	}
}

