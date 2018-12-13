using SQLite4Unity3d;
using UnityEngine;
using System;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class TrainingServices  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private Training _nullTraining = new Training{
				id = 0,
				name = "null",
				percentage = 0,
				creationDate = "null",
				groupId = 0,
				lastUpdate = "null"
		};
	


	/// <summary>
	/// Description to method to create a training
	/// </summary>
	/// <returns>
	/// An object of type training with all the data of the training that was created.
	/// </returns>

	public Training CreateTraining(){

		int groupid = DataBaseParametersCtrl.Ctrl._groupLoaded.id;
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		var new_t = new Training{
				name = "Training",
				percentage = 0,
				creationDate = date,
				groupId = groupid,
				lastUpdate = date
		};
			_connection.Insert (new_t);
			return new_t;
	
	}

	/// <summary>
	/// Description to method Get Training that contain in the DataBaseParametersCtrl.!-- _trainingLoaded
	/// </summary>
	/// <returns>
	/// An object of type training with all the data of the training that was searched and if doesnt exist so return an empty training.
	/// </returns>
	public Training GetTrainingNamed(){

		string trainingName = DataBaseParametersCtrl.Ctrl._trainingloaded.name;
		int groupId = DataBaseParametersCtrl.Ctrl._trainingloaded.groupId;
		
		var g = _connection.Table<Training>().Where(x => x.name == trainingName).Where(x => x.groupId == groupId).FirstOrDefault();

		if (g == null)
			return _nullTraining;	
		else 
			return g;
	}

	/// <summary>
	/// Description of the method to obtain all the trainings of a specific group
	/// </summary>
	/// <param name="groupId">
	/// integer to define the identifier of the group from which all the related courses will be brought.
	/// <returns>
	/// A IEnumerable list of all the Trainings found from the identifier of the group that was passed as a parameter
	/// </returns>
	public IEnumerable<Training> GetTrainings(int groupId){
		return _connection.Table<Training>().Where(x => x.groupId == groupId);
	}

	/// <summary>
	/// (This is a test method) Description of the method to obtain all the Training
	/// </summary>
	/// <returns>
	/// A IEnumerable list of all the trainings found
	/// </returns>
	public IEnumerable<Training> GetTrainings(){
		return _connection.Table<Training>();
	}

	/// <summary>
	/// Description of the method to delete a training
	/// </summary>
	/// <param name="trainingToDelete">
	/// An object of type training that contain the training that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteCourse(Training trainingToDelete){
		return _connection.Delete(trainingToDelete);
	}

	/// <summary>
	/// Description of the method to update a training
	/// </summary>
	/// <param name="trainingToUpdate">
	/// An object of type training that contain the training that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateCourse(Training trainingToUpdate){
		return _connection.Update(trainingToUpdate, trainingToUpdate.GetType());
	}
}

