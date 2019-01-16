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

public class AnswerServices:MonoBehaviour  {

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
	
	private bool isQueryOk = false;

	private Answer _answerGetToDB = new Answer();

	private int resultToDB = 0;

	private IEnumerable<Answer> _answersLoaded = new Answer[]{
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
		int questionid = DataBaseParametersCtrl.Ctrl._questionLoaded.id;

		//Get the current date to create the new Answer
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		//Creation of the new Answer
		var new_a = new Answer{
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
	public IEnumerable<Answer> GetAnswers(int questionId){

		//valueToResponse = 2

		return _connection.Table<Answer>().Where(x => x.questionId == questionId);
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

		if (result!=0)
			Debug.Log("Se borró la respuesta correctamente");
		else
			Debug.Log("No se borró la respuesta");
		
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
		AnswerToUpdate.counter = counter++;
		AnswerToUpdate.lastUpdate = DataBaseParametersCtrl.Ctrl.GetDateTime();

		int result = _connection.Update(AnswerToUpdate, AnswerToUpdate.GetType());

		if (result!=0)
		{
			_questionServices.UpdateQuestion(AnswerToUpdate.questionId);
		}

		return result;

	}

	#region METHODS to get data to DB

	public void setDBToWeb(string methodToCall, int valueToResponse, Answer answer){

		//UserData tempUser = new UserData (player.id, player.cycle, game);
		string json = JsonUtility.ToJson (answer, true);
		UnityWebRequest postRequest = SetJsonForm (json, methodToCall);
		if (postRequest != null){
			switch(valueToResponse){
				case 1:

				StartCoroutine (waitDB_ToCreateAnswer (postRequest));

				break;

				case 3:

				StartCoroutine (waitDB_ToDeleteAnswer (postRequest));
				
				break;

				case 4:

				StartCoroutine (waitDB_ToUpdateAnswer (postRequest));
				
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

	IEnumerator waitDB_ToCreateAnswer (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseCreateAnswer resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseCreateAnswer> (www.downloadHandler.text);
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
					_answerGetToDB = resp.answerCreated;
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

	IEnumerator waitDB_ToDeleteAnswer (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseDeleteAnswer resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseDeleteAnswer> (www.downloadHandler.text);
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

	IEnumerator waitDB_ToUpdateAnswer (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseUpdateAnswer resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseUpdateAnswer> (www.downloadHandler.text);
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
           
			yield return (waitDB_ToGetAnswer (postRequest));
		
        }


	IEnumerator waitDB_ToGetAnswer (WWW www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseGetAnswers resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseGetAnswers> (www.text);
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
					_answersLoaded = resp.answers;
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

