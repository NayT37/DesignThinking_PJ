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
				name = "null",
				percentage = 0,
				creationDate = "null",
				lastUpdate = "null",
				projectId = 0
		};
	


	/// <summary>
	/// Description to method to create a problem
	/// </summary>
	/// <param name="problem">
	/// Attribute that contains an object of type problem with all the data of the problem that will be created.
	/// </param>
	/// <returns>
	/// An object of type problem with all the data of the problem that was created.
	/// </returns>

	public Problem CreateProblem(Problem problem){

		var problemValidation = GetProblemNamed(problem.name, problem.projectId);

		if ((problemValidation.name).Equals("null"))
		{
			_connection.Insert (problem);
			return problem;
		} else {
			return _nullProblem;
		}
		
		
	}

	/// <summary>
	/// Description to method Get Problem with the specified name and projectId
	/// </summary>
	/// <param name="problemName">
	/// Name of the problem that will be searched
	/// </param>
	/// <param name="projectId">
	/// project identifier to find the correct problem that will be searched
	/// </param>
	/// <returns>
	/// An object of type problem with all the data of the problem that was searched and if doesnt exist so return an empty problem.
	/// </returns>
	public Problem GetProblemNamed(string problemName, int projectId){
		
		var p = _connection.Table<Problem>().Where(x => x.name == problemName).Where(x => x.projectId == projectId).FirstOrDefault();

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
	public Problem GetProblemNamed(){

		string problemName = DataBaseParametersCtrl.Ctrl._problemLoaded.name;
		int projectId = DataBaseParametersCtrl.Ctrl._problemLoaded.projectId;
		
		var p = _connection.Table<Problem>().Where(x => x.name == problemName).Where(x => x.projectId == projectId).FirstOrDefault();

		if (p == null)
			return _nullProblem;	
		else 
			return p;
	}

	/// <summary>
	/// Description of the method to obtain all the problems of a specific project
	/// </summary>
	/// <param name="projectId">
	/// integer to define the identifier of the project from which all the related problems will be brought.
	/// <returns>
	/// A IEnumerable list of all the Problems found from the identifier of the project that was passed as a parameter
	/// </returns>
	public IEnumerable<Problem> GetProblems(int projectId){
		return _connection.Table<Problem>().Where(x => x.projectId == projectId);
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
	/// Description of the method to delete a problem
	/// </summary>
	/// <param name="problemToDelete">
	/// An object of type problem that contain the problem that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteProject(Problem problemToDelete){

		//Se debe tener el cuenta que al eliminar un proyecto de debe eliminar 
		//todo lo que continua hacia abajo en la jerarquia de la base de datos (problema, publico, etc)
		return _connection.Delete(problemToDelete);
	}

	/// <summary>
	/// Description of the method to update a problem
	/// </summary>
	/// <param name="problemToUpdate">
	/// An object of type problem that contain the problem that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateProblem(Problem problemToUpdate){
		return _connection.Update(problemToUpdate, problemToUpdate.GetType());
	}
}

