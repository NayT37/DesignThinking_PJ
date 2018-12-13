using SQLite4Unity3d;
using UnityEngine;
using System;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class CaseServices  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private Case _nullCase = new Case{
				id = 0,
				name = "null",
				percentage = 0,
				creationDate = "null",
				trainingId = 0,
				lastUpdate = "null"
		};
	


	/// <summary>
	/// Description to method to create a case
	/// </summary>
	/// <param name="case">
	/// Attribute that contains an object of type case with all the data of the case that will be created.
	/// </param>
	/// <returns>
	/// An object of type case with all the data of the case that was created.
	/// </returns>

	public Case Createcase(Case _case){

		//var trainingValidation = GetTrainingNamed(case.name, case.trainingId);

		// if ((trainingValidation.name).Equals("null"))
		// {
			_connection.Insert (_case);
			return _case;
		// } else {
		// 	return _nullTraining;
		// }
		
		
	}

	/// <summary>
	/// Description to method Get Case that contain in the DataBaseParametersCtrl.!-- _trainingLoaded
	/// </summary>
	/// <returns>
	/// An object of type case with all the data of the case that was searched and if doesnt exist so return an empty case.
	/// </returns>
	public Case GetCaseNamed(){

		string caseName = DataBaseParametersCtrl.Ctrl._caseLoaded.name;
		int trainingId = DataBaseParametersCtrl.Ctrl._caseLoaded.trainingId;
		
		var c = _connection.Table<Case>().Where(x => x.name == caseName).Where(x => x.trainingId == trainingId).FirstOrDefault();

		if (c == null)
			return _nullCase;	
		else 
			return c;
	}

	/// <summary>
	/// Description of the method to obtain all the cases of a specific training
	/// </summary>
	/// <param name="trainingId">
	/// integer to define the identifier of the training from which all the related cases will be brought.
	/// <returns>
	/// A IEnumerable list of all the Cases found from the identifier of the training that was passed as a parameter
	/// </returns>
	public IEnumerable<Case> GetCases(int trainingId){
		return _connection.Table<Case>().Where(x => x.trainingId == trainingId);
	}

	/// <summary>
	/// (This is a test method) Description of the method to obtain all the Cases
	/// </summary>
	/// <returns>
	/// A IEnumerable list of all the cases found
	/// </returns>
	public IEnumerable<Case> GetCases(){
		return _connection.Table<Case>();
	}

	/// <summary>
	/// Description of the method to delete a case
	/// </summary>
	/// <param name="caseToDelete">
	/// An object of type case that contain the case that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteCase(Case caseToDelete){
		return _connection.Delete(caseToDelete);
	}

	/// <summary>
	/// Description of the method to update a case
	/// </summary>
	/// <param name="caseToUpdate">
	/// An object of type case that contain the case that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateCase(Case caseToUpdate){
		return _connection.Update(caseToUpdate, caseToUpdate.GetType());
	}
}

