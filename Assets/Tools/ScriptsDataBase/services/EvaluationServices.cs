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

public class EvaluationServices:MonoBehaviour  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private string[] arrayDescriptions = new string[]{"Factor innovador de producto/servicio en el mercado actual. (Diferenciador respecto de la competencia).",
													  "Nivel de respuesta a las necesidades, costumbres y hábitos de los potenciales clientes y/o beneficiarios.", 
													  "En que medida el producto/servicio soluciona un problema.", 
													  "Nivel de probabilidad de que existan o pueden aparecer productos /servicios que sustituyan a mi idea.", 
													  "Nivel de dificultad para poner en marcha el desarrollo de la idea (Producto/servicio).", 
													  "Nivel de competencia existente hace que sea complicado el desarrollo del producto/servicio.", 
													  "Necesidad de Financiación externa.", 
													  "Estimación de que suba la demanda y el interés por el producto/servicio prototipado en el tiempo.", 
													  "Nivel de disponibilidad de recursos (humano, técnicos, Financieros) para el desarrollo de producto/servicio.", 
													  "Probabilidad de desarrollar el producto/servicio, en corto tiempo."};

	private string[] arraycategorys = new string[]{"pertinencia", "pertinencia", "pertinencia", "competitividad", "competitividad", "competitividad", "competitividad", "viabilidad", "viabilidad", "viabilidad"};

	private QuestionServices _questionServices = new QuestionServices();

	private Evaluation _nullEvaluation = new Evaluation{
				id = 0,
				category = "null",
				percentage = 0,
				creationDate = "null",
				mindMapId = 0,
				lastUpdate = "null"			
		};
	
	private bool isQueryOk = false;

	private Evaluation _evaluationGetToDB = new Evaluation();

	private int resultToDB = 0;

	/// <summary>
	/// Description to method to create a evaluation
	/// </summary>
	/// <param name="categoryname">
	/// Attribute that contains an string with the evaluation's category that will be created.
	/// </param>
	/// <returns>
	/// An object of type evaluation with all the data of the evaluation that was created.
	/// </returns>

	public Evaluation CreateEvaluation(string categoryname){

		//valueToResponse = 1

		//The identifier of the mindmap is obtained to be able to pass 
		//it as an attribute in the new evaluation that will be created
		int mindmapid = DataBaseParametersCtrl.Ctrl._mindMapLoaded.id;

		//DataBaseParametersCtrl.Ctrl.createEvaluation();

		//Get the current date to create the new evaluation
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		//Creation of the new evaluation
		var new_e = new Evaluation{
				category = categoryname,
				percentage = 100,
				creationDate = date,
				mindMapId = mindmapid,
				lastUpdate = date			
		};

		//Start-Validation that the query is right
		
		int result = _connection.Insert (new_e);

		int questionsCounter = 10;

		if (result != 0)
		{
			DataBaseParametersCtrl.Ctrl._evaluationLoaded = new_e;

			//Queda pendiente saber cuantás preguntas son----
			for (int i = 0; i < questionsCounter; i++)
			{
				var q = _questionServices.CreateQuestion(arrayDescriptions[i], arraycategorys[i]);	
				
			}

			return new_e;
				
		}else {
			return _nullEvaluation;
		}

		//End-Validation that the query		
		
		
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
	public Evaluation GetEvaluationNamed(int mindmapId){

		//valueToResponse = 2
		
		var e = _connection.Table<Evaluation>().Where(x => x.mindMapId == mindmapId).FirstOrDefault();

		if (e == null)
			return _nullEvaluation;	
		else 
			return e;
	}

	/// <summary>
	/// Description to method Get Evaluation with the specified mindmapId
	/// </summary>
	/// <param name="evaluationid">
	/// evaluation identifier to find the correct evaluation that will be searched
	/// </param>
	/// <returns>
	/// An object of type evaluation with all the data of the evaluation that was searched and if doesnt exist so return an empty evaluation.
	/// </returns>
	public Evaluation GetEvaluationId( int evaluationid){
		
		var e = _connection.Table<Evaluation>().Where(x => x.id == evaluationid).FirstOrDefault();

		if (e == null)
			return _nullEvaluation;	
		else 
			return e;
	}


	/// <summary>
	/// Description of the method to delete a evaluation
	/// </summary>
	/// <param name="evaluationToDelete">
	/// An object of type evaluation that contain the evaluation that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteEvaluation(Evaluation evaluationToDelete){

		//valueToResponse = 3

		int evaluationid = evaluationToDelete.id;

		int result = _connection.Delete(evaluationToDelete);

		int valueToReturn = 0;

		//If the elimination of the evaluation is correct, then the questions corresponding to that empathymap are eliminated.
		if (result!=0)
		{
			
			//All the questions belonging to the evaluation that will be deleted are obtained.
			var questions = _questionServices.GetQuestions(evaluationid);

			foreach (var question in questions)
			{
				valueToReturn += _questionServices.DeleteQuestion(question);
			}
			Debug.Log("Se borró la evaluación correctamente");
		} else {
			valueToReturn = 0;
			Debug.Log("No se borró la evaluación");
		}

		return valueToReturn;
	}

	/// <summary>
	/// Description of the method to update a evaluation
	/// </summary>
	/// <param name="evaluationid">
	/// An integer with the evaluation identifier that contain the evaluation that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateEvaluation(int evaluationid){

		var e = GetEvaluationId(evaluationid);
		int result = 0;

		if (e.id!=0)
		{
			e.percentage = 33;
			e.lastUpdate = DataBaseParametersCtrl.Ctrl.GetDateTime();
			result = _connection.Update(e, e.GetType());

		}
		return result;
	}

	#region METHODS to get data to DB

	public void setDBToWeb(string methodToCall, int valueToResponse, Evaluation evaluation){

		//UserData tempUser = new UserData (player.id, player.cycle, game);
		string json = JsonUtility.ToJson (evaluation, true);
		UnityWebRequest postRequest = SetJsonForm (json, methodToCall);
		if (postRequest != null){
			switch(valueToResponse){
				case 1:

				StartCoroutine (waitDB_ToCreateEvaluation (postRequest));

				break;

				case 3:

				StartCoroutine (waitDB_ToDeleteEvaluation (postRequest));
				
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

	IEnumerator waitDB_ToCreateEvaluation (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseCreateEvaluation resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseCreateEvaluation> (www.downloadHandler.text);
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
					_evaluationGetToDB = resp.evaluationCreated;
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

	IEnumerator waitDB_ToDeleteEvaluation (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseDeleteEvaluation resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseDeleteEvaluation> (www.downloadHandler.text);
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
           
			yield return (waitDB_ToGetEvaluation (postRequest));
		
        }


	IEnumerator waitDB_ToGetEvaluation (WWW www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseGetEvaluation resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseGetEvaluation> (www.text);
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
					_evaluationGetToDB = resp.evaluation;
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

