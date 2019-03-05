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

public class TrainingServices:MonoBehaviour  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private Training _nullTraining = new Training{
				id = 0,
				name = "null",
				percentage = 0,
				creationDate = "null",
				groupId = 0,
				lastUpdate = "null"
		};
	
	private CaseServices _caseServices = new CaseServices();
	private string[] _arraycasesname = new string[]{"case_1","case_2","case_3"};


	/// <summary>
	/// Description to method to create a training
	/// </summary>
	/// <param name="groupid">
	/// Attribute that contains the identifier group to pass as parameter of the new training that will be created.
	/// </param>
	/// <returns>
	/// An integer response of the query (0 = the training was not created correctly. 15 = the training was created correctly and its respective cases and moments too.!-- [>=100] = the training was created correctlly but some case was not created and so its moments neither)
	/// </returns>

	public int CreateTraining(Int64 groupid){

		//valueToResponse = 1
		
		//Get the current date to create the new group
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		int valueToReturn = 0;

		int counter = 0;
		var new_t = new Training{
				id = DataBaseParametersCtrl.Ctrl.GenerateCodeToId(),
				name = "Training",
				percentage = 0,
				creationDate = date,
				groupId = groupid,
				lastUpdate = date
		};
		
		int result = _connection.Insert (new_t);

		//If the creation of the training is correct, then the cases corresponding to that training are created.
		if (result!=0){

			DataBaseParametersCtrl.Ctrl._trainingloaded = new_t;
			
			for (int i = 0; i < 3; i++)
			{
				//Creation of the cases
				counter+=_caseServices.Createcase(_arraycasesname[i], new_t.id);
			}
			valueToReturn = counter;

		}else{
			valueToReturn = 0;
		}
		

		return valueToReturn;
	
	}

	/// <summary>
	/// Description to method Get group that contain in the DataBaseParametersCtrl.!-- _trainingLoaded
	/// </summary>
	/// <param name="trainingid">
	/// integer to define the identifier of the training that will be searched.
	/// <returns>
	/// <returns>
	/// An object of type group with all the data of the group that was searched and if doesnt exist so return an empty group.
	/// </returns>
	public Training GetTrainingId(Int64 trainingid){
		
		//valueToResponse = 2

		var t = _connection.Table<Training>().Where(x => x.id == trainingid).FirstOrDefault();

		if (t == null)
			return _nullTraining;	
		else 
			return t;
	}

	/// <summary>
	/// Description of the method to obtain all the trainings of a specific group
	/// </summary>
	/// <param name="groupId">
	/// integer to define the identifier of the group from which all the related courses will be brought.
	/// <returns>
	/// A IEnumerable list of all the Trainings found from the identifier of the group that was passed as a parameter
	/// </returns>
	public Training GetTraining(Int64 groupId){

		//valueToResponse = 3

		var result = _connection.Table<Training>().Where(x => x.groupId == groupId).FirstOrDefault();

		if (result.id == 0)
			result = _nullTraining;
		
		return result;
	}

	/// <summary>
	/// Description of the method to obtain all the trainings of a specific group
	/// </summary>
	/// <param name="groupId">
	/// integer to define the identifier of the group from which all the related courses will be brought.
	/// <returns>
	/// A IEnumerable list of all the Trainings found from the identifier of the group that was passed as a parameter
	/// </returns>
	public IEnumerable<Training> GetAllTrainings(){

		//valueToResponse = 3

		return _connection.Query<Training> ("select * from Training where id LIKE '%" + DataBaseParametersCtrl.Ctrl._teacherLoggedIn.identityCard +"%' ORDER BY creationDate ASC");
	
	}



	/// <summary>
	/// (This is a test method) Description of the method to obtain all the Training
	/// </summary>
	/// <returns>
	/// A IEnumerable list of all the trainings found
	/// </returns>

	public int DeleteTraining(Training trainingToDelete){

		//All the cases belonging to the training that will be deleted are obtained.
		var cases = _caseServices.GetCases(trainingToDelete.groupId);

		int result = _connection.Delete(trainingToDelete);
		int valueToReturn = 0;

		//If the elimination of the training is correct, then the cases corresponding to that training are eliminated.
		if (result!=0)
		{
			foreach (var _case in cases)
			{
				valueToReturn += _caseServices.DeleteCase(_case);
			}

			Debug.Log("TRAINING BORRADO --- " + result);
		} else {
			valueToReturn = 0;
		}

		return valueToReturn;
	}

	/// <summary>
	/// Description of the method to update a training
	/// </summary>
	/// <param name="trainingid">
	/// Identifier of the training that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateTraining(Int64 trainingid){

		var _groupServices = new GroupServices();

		var trainingToUpdate = GetTrainingId(trainingid);
		var cases = _caseServices.GetCases(trainingid);
		int valueToReturn = 0;
		int average = 0;
		
		if (trainingToUpdate.id!=0)
		{
			foreach (var _case in cases)
			{
				average += _case.percentage;
			}

			average = average/3;

			trainingToUpdate.percentage = average;
			trainingToUpdate.lastUpdate = DataBaseParametersCtrl.Ctrl.GetDateTime();

			valueToReturn = _connection.Update(trainingToUpdate, trainingToUpdate.GetType());

			if (valueToReturn!=0)
			{
				DataBaseParametersCtrl.Ctrl._trainingloaded = trainingToUpdate;
				_groupServices.UpdateGroup();
			}
		}
		return _connection.Update(trainingToUpdate, trainingToUpdate.GetType());
	}
}

