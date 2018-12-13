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
	/// <param name="question">
	/// Attribute that contains an object of type question with all the data of the question that will be created.
	/// </param>
	/// <returns>
	/// An object of type question with all the data of the question that was created.
	/// </returns>

	public Question CreateQuestion(Question question){

		// var publicValidation = GetProblemNamed(question.name, question.evaluationId);

		// if ((publicValidation.name).Equals("null"))
		// {
			_connection.Insert (question);
			return question;
		// } else {
		// 	return _nullPublic;
		// }
		
		
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
	public int UpdateEmpathymap(Question questionToUpdate){
		return _connection.Update(questionToUpdate, questionToUpdate.GetType());
	}
}

