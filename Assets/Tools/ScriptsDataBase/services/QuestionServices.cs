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

public class QuestionServices:MonoBehaviour  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private AnswerServices _answerServices = new AnswerServices();

	private Question _nullQuestion = 
		new Question{
				id = 0,
				creationDate = "null",
				description = "null",
				evaluationId = 0,
				lastUpdate = "null",
				category = "null"		
		};
	
	private bool isQueryOk = false;

	private Question _questionGetToDB = new Question();

	private int resultToDB = 0;

	private IEnumerable<Question> _questionsLoaded = new Question[]{
		new Question{
				id = 0,
				creationDate = "null",
				description = "null",
				evaluationId = 0,
				lastUpdate = "null",
				category = "null"		
		},
		new Question{
				id = 0,
				creationDate = "null",
				description = "null",
				evaluationId = 0,
				lastUpdate = "null",
				category = "null"		
		},
		new Question{
				id = 0,
				creationDate = "null",
				description = "null",
				evaluationId = 0,
				lastUpdate = "null",
				category = "null"		
		}
	};

	/// <summary>
	/// Description to method to create a question
	/// </summary>
	/// <param name="descriptionOfQuestion">
	/// Attribute that contains an string with the fescription of the question that will be created.
	/// </param>
	/// <param name="categoryQuestion">
	/// Attribute that contains an string with the category of the question that will be created.
	/// </param>
	/// <returns>
	/// An object of type question with all the data of the question that was created.
	/// </returns>

	public Question CreateQuestion(string descriptionOfQuestion, string categoryQuestion){

		//valueToResponse = 1

		//The identifier of the evaluation is obtained to be able to pass 
		//it as an attribute in the new question that will be created
		int evaluationid = DataBaseParametersCtrl.Ctrl._evaluationLoaded.id;

		//Get the current date to create the new question
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		//Creation of the new question
		var new_q = new Question{
				creationDate = date,
				description = descriptionOfQuestion,
				evaluationId = evaluationid,
				lastUpdate = date,
				category = 	categoryQuestion		
		};

		//Start-Validation that the query is right
		
		int result = _connection.Insert (new_q);

		if (result != 0)
		{
			DataBaseParametersCtrl.Ctrl._questionLoaded = new_q;
			
			for (int i = 0; i < 5; i++)
			{	
				var a = _answerServices.CreateAnswer(i+1);

			}

		return new_q;
		

		}else {
			return _nullQuestion;
		}
		//End-Validation that the query		
		
		
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

		//valueToResponse = 2
		
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
		
		//valueToResponse = 3

		return _connection.Table<Question>().Where(x => x.evaluationId == evaluationId);
	}

	/// <summary>
	/// Description of the method to delete a question
	/// </summary>
	/// <param name="questionToDelete">
	/// An object of type question that contain the question that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteQuestion(Question questionToDelete){

		//valueToResponse = 4

		int questionid = questionToDelete.id;

		int result = _connection.Delete(questionToDelete);

		int valueToReturn = 0;

		//If the elimination of the question is correct, then the answers corresponding to that empathymap are eliminated.
		if (result!=0)
		{
			
			//All the answers belonging to the question that will be deleted are obtained.
			var answers = _answerServices.GetAnswers(questionid);

			foreach (var answer in answers)
			{
				valueToReturn += _answerServices.DeleteAnswer(answer);
			}
			Debug.Log("Se borró la pregunta correctamente");
		} else {
			valueToReturn = 0;
			Debug.Log("No se borró la pregunta");
		}

		return valueToReturn;
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

	#region METHODS to get data to DB

	public void setDBToWeb(string methodToCall, int valueToResponse, Question evaluation){

		//UserData tempUser = new UserData (player.id, player.cycle, game);
		string json = JsonUtility.ToJson (evaluation, true);
		UnityWebRequest postRequest = SetJsonForm (json, methodToCall);
		if (postRequest != null){
			switch(valueToResponse){
				case 1:

				StartCoroutine (waitDB_ToCreateQuestion (postRequest));

				break;

				case 4:

				StartCoroutine (waitDB_ToDeleteQuestion (postRequest));
				
				break;

			}
		}
			
	
	}

	private UnityWebRequest SetJsonForm (string json, string method) {
		try {
			UnityWebRequest web = UnityWebRequest.Put (DataBaseParametersCtrl.Ctrl._ipServer + method + "/put", json);
			web.SetRequestHeader ("Content-Type", "application/json");
			return web;
		} catch {
			return null;
		}
	}

	IEnumerator waitDB_ToCreateQuestion (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseCreateQuestion resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseCreateQuestion> (www.downloadHandler.text);
            } catch { }

            //Validacion de la informacion obtenida
            if (!string.IsNullOrEmpty (www.error) && resp == null) { //Error al descargar data
                Debug.Log (www.error);
                try {

                } catch (System.Exception e) { Debug.Log (e); }
                yield return null;
            } else

            if (resp != null) { // Informacion obtenida exitosamente
                if (!resp.error) { // sin error en el servidor
					_questionGetToDB = resp.questionCreated;
					isQueryOk = true;
                    } else { // no existen usuarios
                    }

                } else { //Error en el servidor de base de datos
                    // Debug.Log ("user error: " + resp.error);
                    try {

                    } catch { }
                    // HUDController.HUDCtrl.MessagePanel (resp.msg);
                }
            }
        
        yield return null;
    }

	IEnumerator waitDB_ToDeleteQuestion (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseDeleteQuestion resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseDeleteQuestion> (www.downloadHandler.text);
            } catch { }

            //Validacion de la informacion obtenida
            if (!string.IsNullOrEmpty (www.error) && resp == null) { //Error al descargar data
                Debug.Log (www.error);
                try {

                } catch (System.Exception e) { Debug.Log (e); }
                yield return null;
            } else

            if (resp != null) { // Informacion obtenida exitosamente
                if (!resp.error) { // sin error en el servidor
					resultToDB = resp.result;
					isQueryOk = true;
                    } else { // no existen usuarios
                    }

                } else { //Error en el servidor de base de datos
                    // Debug.Log ("user error: " + resp.error);
                    try {

                    } catch { }
                    // HUDController.HUDCtrl.MessagePanel (resp.msg);
                }
            }
        
        yield return null;
    }

	#endregion

	#region METHODS to get data to DB
	public IEnumerator GetToDB (string methodToCall, string parameterToGet, int valueToResponse) {

            WWW postRequest = new WWW (DataBaseParametersCtrl.Ctrl._ipServer + methodToCall + parameterToGet); // buscar en el servidor al usuario
			switch(valueToResponse){

				case 2:

				yield return waitDB_ToGetQuestion (postRequest);

				break;

				case 3:

				yield return (waitDB_ToGetQuestions (postRequest));
				
				break;

			}
			
		
        }

	IEnumerator waitDB_ToGetQuestion (WWW www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseGetQuestion resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseGetQuestion> (www.text);
            } catch { }

            //Validacion de la informacion obtenida
            if (!string.IsNullOrEmpty (www.error) && resp == null) { //Error al descargar data
                Debug.Log (www.error);
                try {

                } catch (System.Exception e) { Debug.Log (e); }
                yield return null;
            } else

            if (resp != null) { // Informacion obtenida exitosamente
                if (!resp.error) { // sin error en el servidor
					_questionGetToDB = resp.question;
					isQueryOk = true;
                    } else { // no existen usuarios
                    }

                } else { //Error en el servidor de base de datos
                    // Debug.Log ("user error: " + resp.error);
                    try {

                    } catch { }
                    // HUDController.HUDCtrl.MessagePanel (resp.msg);
                }
            }
        
        yield return null;
    }
	IEnumerator waitDB_ToGetQuestions (WWW www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseGetQuestions resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseGetQuestions> (www.text);
            } catch { }

            //Validacion de la informacion obtenida
            if (!string.IsNullOrEmpty (www.error) && resp == null) { //Error al descargar data
                Debug.Log (www.error);
                try {

                } catch (System.Exception e) { Debug.Log (e); }
                yield return null;
            } else

            if (resp != null) { // Informacion obtenida exitosamente
                if (!resp.error) { // sin error en el servidor
					_questionsLoaded = resp.questions;
					isQueryOk = true;
                    } else { // no existen usuarios
                    }

                } else { //Error en el servidor de base de datos
                    // Debug.Log ("user error: " + resp.error);
                    try {

                    } catch { }
                    // HUDController.HUDCtrl.MessagePanel (resp.msg);
                }
            }
        
        yield return null;
    }

	#endregion
}

