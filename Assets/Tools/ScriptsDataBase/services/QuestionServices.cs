using SQLite4Unity3d;
using UnityEngine;
using System;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class QuestionServices  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private AnswerServices _answerServices = new AnswerServices();

	private Question _nullQuestion = new Question{
				id = 0,
				grade = 0,
				creationDate = "null",
				description = "null",
				evaluationId = 0,
				lastUpdate = "null"			
		};
	


	/// <summary>
	/// Description to method to create a question
	/// </summary>
	/// <param name="descriptionOfQuestion">
	/// Attribute that contains an string with the dfescription of the question that will be created.
	/// </param>
	/// <returns>
	/// An object of type question with all the data of the question that was created.
	/// </returns>

	public Question CreateQuestion(string descriptionOfQuestion){

		//The identifier of the evaluation is obtained to be able to pass 
		//it as an attribute in the new question that will be created
		int evaluationid = DataBaseParametersCtrl.Ctrl._evaluationLoaded.id;

		//Get the current date to create the new question
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		//Creation of the new question
		var new_q = new Question{
				grade = 0,
				creationDate = date,
				description = descriptionOfQuestion,
				evaluationId = evaluationid,
				lastUpdate = date			
		};

		//Start-Validation that the query is right
		
		int result = _connection.Insert (new_q);

		int counter = 0;

		if (result != 0)
		{
			DataBaseParametersCtrl.Ctrl._questionLoaded = new_q;

			for (int i = 0; i < 5; i++)
			{	
				var a = _answerServices.CreateAnswer(i+1);

				if (a.id != 0)
					counter++;
			}

			if (counter == 5)
				return new_q;
			else 
				return _nullQuestion;
		}else {
			return _nullQuestion;
		}
		//End-Validation that the query		
		
		
	}

	/// <summary>
	/// Description to method Get Question with the specified evaluationId
	/// </summary>
	/// <param name="evaluationId">
	/// evaluation identifier to find the correct question that will be searched
	/// </param>
	/// <returns>
	/// An object of type question with all the data of the question that was searched and if doesnt exist so return an empty question.
	/// </returns>
	public Question GetQuestionNamed( int evaluationId){
		
		var q = _connection.Table<Question>().Where(x => x.evaluationId == evaluationId).FirstOrDefault();

		if (q == null)
			return _nullQuestion;	
		else 
			return q;
	}

	/// <summary>
	/// Description to method Get Question with the specified evaluationId
	/// </summary>
	/// <param name="questionid">
	/// question identifier to find the correct question that will be searched
	/// </param>
	/// <returns>
	/// An object of type question with all the data of the question that was searched and if doesnt exist so return an empty question.
	/// </returns>
	public Question GetQuestionId(int questionid){
		
		var q = _connection.Table<Question>().Where(x => x.id == questionid).FirstOrDefault();

		if (q == null)
			return _nullQuestion;	
		else 
			return q;
	}


	/// <summary>
	/// Description of the method to obtain all the notes of a specific evaluation
	/// </summary>
	/// <param name="evaluationId">
	/// integer to define the identifier of the evaluation from which all the related Questions will be brought.
	/// <returns>
	/// A IEnumerable list of all the Questions found from the identifier of the evaluation that was passed as a parameter
	/// </returns>
	public IEnumerable<Question> GetQuestions(int evaluationId){
		return _connection.Table<Question>().Where(x => x.evaluationId == evaluationId);
	}

	/// <summary>
	/// (This is a test method) Description of the method to obtain all the Questions
	/// </summary>
	/// <returns>
	/// A IEnumerable list of all the notes found
	/// </returns>
	public IEnumerable<Question> GetQuestions(){
		return _connection.Table<Question>();
	}

	/// <summary>
	/// Description of the method to delete a question
	/// </summary>
	/// <param name="questionToDelete">
	/// An object of type question that contain the question that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteEmpathymap(Question questionToDelete){

		return _connection.Delete(questionToDelete);
	}

	/// <summary>
	/// Description of the method to update a question
	/// </summary>
	/// <param name="questionToUpdate">
	/// An object of type question that contain the question that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateQuestion(Question questionToUpdate){
		return _connection.Update(questionToUpdate, questionToUpdate.GetType());
	}

	/// <summary>
	/// Description of the method to update a question
	/// </summary>
	/// <param name="questionid">
	/// An integer with the question identifier that contain the question that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateQuestion(int questionid){

		var q = GetQuestionId(questionid);
		int result = 0;

		if (q.id!=0)
		{
			q.lastUpdate = DataBaseParametersCtrl.Ctrl.GetDateTime();
			result = _connection.Update(q, q.GetType());

		}
		return result;
	}
}

