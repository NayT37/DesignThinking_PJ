using SQLite4Unity3d;
using UnityEngine;
using System;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class ProblemServices  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;


	private Problem _nullProblem = new Problem{
				id = 0,
				percentage = 0,
				creationDate = "null",
				lastUpdate = "null",
				projectId = 0
		};
	
	private FieldServices _fieldServices = new FieldServices();


	/// <summary>
	/// Description to method to create a problem
	/// </summary>
	/// <param name="arrayfieldsname">
	/// Attribute that contains an string array with names of the each field that will be created.
	/// </param>
	/// <returns>
	/// An object of type problem with all the data of the problem that was created.
	/// </returns>

	public Problem CreateProblem(string[] arrayfieldsname){

		//The identifier of the project is obtained to be able to pass 
		//it as an attribute in the new problem that will be created
		int projectid = DataBaseParametersCtrl.Ctrl._projectLoaded.id;

		//Get the current date to create the new problem
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		//Creation of the new problem
		var new_p = new Problem{
				percentage = 100,
				creationDate = date,
				lastUpdate = date,
				projectId = projectid
		};

		//Start-Validation that the query is right
		
		int result = _connection.Insert (new_p);

		if (result != 0)
		{
			DataBaseParametersCtrl.Ctrl._problemLoaded = new_p;
			for (int i = 0; i < arrayfieldsname.Length; i++)
			{
				//Creation of the fields
				_fieldServices.CreateField("Field_"+(i+1), arrayfieldsname[i]);
			}
			Debug.Log(new_p);
			return new_p;
		}else {
			return _nullProblem;
		}
		//End-Validation that the query
		
	}

	/// <summary>
	/// Description to method Get Problem with the specified name and projectId
	/// </summary>
	/// <param name="id">
	/// identifier of the problem that will be searched
	/// </param>
	/// <param name="projectId">
	/// project identifier to find the correct problem that will be searched
	/// </param>
	/// <returns>
	/// An object of type problem with all the data of the problem that was searched and if doesnt exist so return an empty problem.
	/// </returns>
	public Problem GetProblemNamed(int id, int projectId){
		
		var p = _connection.Table<Problem>().Where(x => x.id == id).Where(x => x.projectId == projectId).FirstOrDefault();

		if (p == null)
			return _nullProblem;	
		else 
			return p;
	}

	/// <summary>
	/// Description to method Get Problem that contain in the DataBaseParametersCtrl.!-- _problemLoaded
	/// </summary>
	/// <returns>
	/// An object of type problem with all the data of the problem that was searched and if doesnt exist so return an empty problem.
	/// </returns>
	// public Problem GetProblemNamed(){

	// 	string problemName = DataBaseParametersCtrl.Ctrl._problemLoaded.name;
	// 	int projectId = DataBaseParametersCtrl.Ctrl._problemLoaded.projectId;
		
	// 	var p = _connection.Table<Problem>().Where(x => x.name == problemName).Where(x => x.projectId == projectId).FirstOrDefault();

	// 	if (p == null)
	// 		return _nullProblem;	
	// 	else 
	// 		return p;
	// }

	/// <summary>
	/// Description of the method to obtain all the problems of a specific project
	/// </summary>
	/// <param name="projectId">
	/// integer to define the identifier of the project from which all the related problems will be brought.
	/// <returns>
	/// A IEnumerable list of all the Problems found from the identifier of the project that was passed as a parameter
	/// </returns>
	public Problem GetProblem(int projectId){
		return _connection.Table<Problem>().Where(x => x.projectId == projectId).FirstOrDefault();
	}

	/// <summary>
	/// (This is a test method) Description of the method to obtain all the Problem
	/// </summary>
	/// <returns>
	/// A IEnumerable list of all the projects found
	/// </returns>
	public IEnumerable<Problem> GetProblems(){
		return _connection.Table<Problem>();
	}

	/// <summary>
	/// (This is a test method) Description of the method to obtain all the Problem
	/// </summary>
	/// <returns>
	/// A IEnumerable list of all the projects found
	/// </returns>
	public int GetProblemsCounter(){
		int projectid = DataBaseParametersCtrl.Ctrl._projectLoaded.id;
		return _connection.Table<Problem>().Where(x => x.projectId == projectid).Count();
	}

	/// <summary>
	/// Description of the method to delete a problem
	/// </summary>
	/// <param name="problemToDelete">
	/// An object of type problem that contain the problem that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteProblem(Problem problemToDelete){

		int problemid = problemToDelete.id;
		// All the fields belonging to the problem that will be deleted are obtained.
		var fields = _fieldServices.GetFields(problemid);

		int result = _connection.Delete(problemToDelete);

		int valueToReturn = 0;

		//If the elimination of the problem is correct, then the fields corresponding to that case are eliminated.
		if (result!=0)
		{
			foreach (var field in fields)
			{
				valueToReturn += _fieldServices.DeleteField(field);
			}
			Debug.Log("Se borró el problema campo correctamente");
		} else {
			valueToReturn = 0;
			Debug.Log("No se borró el problema");
		}

		return valueToReturn;
	}

	/// <summary>
	/// Description of the method to update a problem
	/// </summary>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateProblem(){

		var _projectServices = new ProjectServices();
		
		var problemToUpdate = DataBaseParametersCtrl.Ctrl._problemLoaded;
		problemToUpdate.lastUpdate = DataBaseParametersCtrl.Ctrl.GetDateTime();

		int result = _connection.Update(problemToUpdate, problemToUpdate.GetType());

		if (result!=0)
		{
			DataBaseParametersCtrl.Ctrl._problemLoaded = problemToUpdate;
			_projectServices.UpdateProject(true);
		}
		return result;
	}
}

