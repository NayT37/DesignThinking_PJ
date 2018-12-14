﻿using SQLite4Unity3d;
using UnityEngine;
using System;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class EvaluationServices  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private Evaluation _nullEvaluation = new Evaluation{
				id = 0,
				percentage = 0,
				creationDate = "null",
				mindMapId = 0,
				lastUpdate = "null"			
		};
	


	/// <summary>
	/// Description to method to create a evaluation
	/// </summary>
	/// <param name="evaluation">
	/// Attribute that contains an object of type evaluation with all the data of the evaluation that will be created.
	/// </param>
	/// <returns>
	/// An object of type evaluation with all the data of the evaluation that was created.
	/// </returns>

	public Evaluation CreateEvaluation(Evaluation evaluation){

		// var publicValidation = GetProblemNamed(evaluation.name, evaluation.mindmapId);

		// if ((publicValidation.name).Equals("null"))
		// {
			_connection.Insert (evaluation);
			return evaluation;
		// } else {
		// 	return _nullPublic;
		// }
		
		
	}

	/// <summary>
	/// Description to method Get Evaluation with the specified mindmapId
	/// </summary>
	/// <param name="mindmapId">
	/// mindMap identifier to find the correct evaluation that will be searched
	/// </param>
	/// <returns>
	/// An object of type evaluation with all the data of the evaluation that was searched and if doesnt exist so return an empty evaluation.
	/// </returns>
	public Evaluation GetEvaluationNamed( int mindmapId){
		
		var e = _connection.Table<Evaluation>().Where(x => x.mindMapId == mindmapId).FirstOrDefault();

		if (e == null)
			return _nullEvaluation;	
		else 
			return e;
	}

	/// <summary>
	/// Description to method Get Evaluation that contain in the DataBaseParametersCtrl.!-- _empathyMapLoaded
	/// </summary>
	/// <returns>
	/// An object of type evaluation with all the data of the evaluation that was searched and if doesnt exist so return an empty evaluation.
	/// </returns>
	public Evaluation GetEvaluationNamed(){

		int mindmapId = DataBaseParametersCtrl.Ctrl._evaluationLoaded.mindMapId;
		
		var e = _connection.Table<Evaluation>().Where(x => x.mindMapId == mindmapId).FirstOrDefault();

		if (e == null)
			return _nullEvaluation;	
		else 
			return e;
	}

	/// <summary>
	/// Description of the method to obtain all the notes of a specific project
	/// </summary>
	/// <param name="mindmapId">
	/// integer to define the identifier of the project from which all the related Evaluations will be brought.
	/// <returns>
	/// A IEnumerable list of all the Evaluations found from the identifier of the project that was passed as a parameter
	/// </returns>
	public IEnumerable<Evaluation> GetEvaluations(int mindmapId){
		return _connection.Table<Evaluation>().Where(x => x.mindMapId == mindmapId);
	}

	/// <summary>
	/// (This is a test method) Description of the method to obtain all the Evaluations
	/// </summary>
	/// <returns>
	/// A IEnumerable list of all the notes found
	/// </returns>
	public IEnumerable<Evaluation> GetEvaluations(){
		return _connection.Table<Evaluation>();
	}

	/// <summary>
	/// Description of the method to delete a evaluation
	/// </summary>
	/// <param name="evaluationToDelete">
	/// An object of type evaluation that contain the evaluation that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteEmpathymap(Evaluation evaluationToDelete){

		return _connection.Delete(evaluationToDelete);
	}

	/// <summary>
	/// Description of the method to update a evaluation
	/// </summary>
	/// <param name="evaluationToUpdate">
	/// An object of type evaluation that contain the evaluation that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateEmpathymap(Evaluation evaluationToUpdate){
		return _connection.Update(evaluationToUpdate, evaluationToUpdate.GetType());
	}
}
