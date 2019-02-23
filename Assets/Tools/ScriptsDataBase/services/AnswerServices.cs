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

public class AnswerServices : MonoBehaviour  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private Answer _nullAnswer = 
		new Answer{
				id = 0,
				counter = 0,
				value = 0,
				creationDate = "null",
				questionId = 0,
				lastUpdate = "null"			
		};

	private Answer[] _answersLoaded = new Answer[]{
		new Answer{
				id = 0,
				counter = 0,
				value = 0,
				creationDate = "null",
				questionId = 0,
				lastUpdate = "null"			
		},
		new Answer{
				id = 0,
				counter = 0,
				value = 0,
				creationDate = "null",
				questionId = 0,
				lastUpdate = "null"			
		},
		new Answer{
				id = 0,
				counter = 0,
				value = 0,
				creationDate = "null",
				questionId = 0,
				lastUpdate = "null"			
		},
		new Answer{
				id = 0,
				counter = 0,
				value = 0,
				creationDate = "null",
				questionId = 0,
				lastUpdate = "null"			
		}
	};


	/// <summary>
	/// Description to method to create a Answer
	/// </summary>
	/// <param name="valueToAnswer">
	/// Attribute that contains an integer with the value of the Answer that will be created.
	/// </param>
	/// <returns>
	/// An object of type Answer with all the data of the Answer that was created.
	/// </returns>

	public Answer CreateAnswer(int valueToAnswer){

		//valueToResponse = 1

		//The identifier of the question is obtained to be able to pass 
		//it as an attribute in the new Answer that will be created
		Int64 questionid = DataBaseParametersCtrl.Ctrl._questionLoaded.id;

		//Get the current date to create the new Answer
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		//Creation of the new Answer
		var new_a = new Answer{
				id = DataBaseParametersCtrl.Ctrl.GenerateCodeToId(),
				counter = 0,
				value = valueToAnswer,
				creationDate = date,
				questionId = questionid,
				lastUpdate = date			
		};

		//Start-Validation that the query is right
		
		int result = _connection.Insert (new_a);

		if (result != 0)
		{
			return new_a;
				
		}else {
			return _nullAnswer;
		}
		//End-Validation that the query		
		
		
	}

	/// <summary>
	/// Description of the method to obtain all the notes of a specific project
	/// </summary>
	/// <param name="questionId">
	/// integer to define the identifier of the project from which all the related Answers will be brought.
	/// <returns>
	/// A IEnumerable list of all the Answers found from the identifier of the project that was passed as a parameter
	/// </returns>
	public IEnumerable<Answer> GetAnswers(Int64 questionId){

		//valueToResponse = 2

		return _connection.Query<Answer> ("select * from Answer where questionId = " + questionId +" ORDER BY creationDate ASC");
	}

	public IEnumerable<Answer> GetAllAnswers(){

		//valueToResponse = 2 

		return _connection.Query<Answer> ("select * from Answer where id LIKE '%" + DataBaseParametersCtrl.Ctrl._teacherLoggedIn.identityCard +"%' ORDER BY creationDate ASC");
	}
	/// <summary>
	/// Description of the method to delete a Answer
	/// </summary>
	/// <param name="AnswerToDelete">
	/// An object of type Answer that contain the Answer that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteAnswer(Answer AnswerToDelete){

		//valueToResponse = 3
		
		int result = _connection.Delete(AnswerToDelete);

		return result;
	}

	/// <summary>
	/// Description of the method to update a Answer
	/// </summary>
	/// <param name="AnswerToUpdate">
	/// An object of type Answer that contain the Answer that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateAnswer(Answer AnswerToUpdate){

		//valueToResponse = 4

		var _questionServices = new QuestionServices();

		int counter = AnswerToUpdate.counter;
		AnswerToUpdate.counter = counter+1;
		AnswerToUpdate.lastUpdate = DataBaseParametersCtrl.Ctrl.GetDateTime();

		int result = _connection.Update(AnswerToUpdate, AnswerToUpdate.GetType());

		if (result!=0)
		{
			_questionServices.UpdateQuestion(AnswerToUpdate.questionId);
		}

		return result;

	}
}

