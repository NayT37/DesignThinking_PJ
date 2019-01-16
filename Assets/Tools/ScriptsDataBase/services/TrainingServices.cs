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

public class TrainingServices:MonoBehaviour  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private Training _nullTraining = new Training{
				id = 0,
				name = "null",
				percentage = 0,
				creationDate = "null",
				groupId = 0,
				lastUpdate = "null"
		};
	
	private CaseServices _caseServices = new CaseServices();

	private bool isQueryOk = false;

	private Training _trainingGetToDB = new Training();

	private int resultToDB = 0;


	private string[] _arraycasesname = new string[]{"case_1","case_2","case_3"};


	/// <summary>
	/// Description to method to create a training
	/// </summary>
	/// <param name="groupid">
	/// Attribute that contains the identifier group to pass as parameter of the new training that will be created.
	/// </param>
	/// <returns>
	/// An integer response of the query (0 = the training was not created correctly. 15 = the training was created correctly and its respective cases and moments too.!-- [>=100] = the training was created correctlly but some case was not created and so its moments neither)
	/// </returns>

	public int CreateTraining(int groupid){

		//valueToResponse = 1
		
		//Get the current date to create the new group
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		int valueToReturn = 0;

		int counter = 0;
		var new_t = new Training{
				name = "Training",
				percentage = 0,
				creationDate = date,
				groupId = groupid,
				lastUpdate = date
		};
		
		int result = _connection.Insert (new_t);

		//If the creation of the training is correct, then the cases corresponding to that training are created.
		if (result!=0){

			DataBaseParametersCtrl.Ctrl._trainingloaded = new_t;
			
			for (int i = 0; i < 3; i++)
			{
				//Creation of the cases
				counter+=_caseServices.Createcase(_arraycasesname[i], new_t.id);
			}
			Debug.Log(new_t);
			valueToReturn = counter;

		}else{
			valueToReturn = 0;
		}
		

		return valueToReturn;
	
	}

	/// <summary>
	/// Description to method Get group that contain in the DataBaseParametersCtrl.!-- _trainingLoaded
	/// </summary>
	/// <param name="trainingid">
	/// integer to define the identifier of the training that will be searched.
	/// <returns>
	/// <returns>
	/// An object of type group with all the data of the group that was searched and if doesnt exist so return an empty group.
	/// </returns>
	public Training GetTrainingId(int trainingid){
		
		//valueToResponse = 2

		var t = _connection.Table<Training>().Where(x => x.id == trainingid).FirstOrDefault();

		if (t == null)
			return _nullTraining;	
		else 
			return t;
	}

	/// <summary>
	/// Description of the method to obtain all the trainings of a specific group
	/// </summary>
	/// <param name="groupId">
	/// integer to define the identifier of the group from which all the related courses will be brought.
	/// <returns>
	/// A IEnumerable list of all the Trainings found from the identifier of the group that was passed as a parameter
	/// </returns>
	public Training GetTraining(int groupId){

		//valueToResponse = 3

		var result = _connection.Table<Training>().Where(x => x.groupId == groupId).FirstOrDefault();

		if (result.id == 0)
			result = _nullTraining;
		
		return result;
	}

	/// <summary>
	/// (This is a test method) Description of the method to obtain all the Training
	/// </summary>
	/// <returns>
	/// A IEnumerable list of all the trainings found
	/// </returns>

	public int DeleteTraining(Training trainingToDelete){

		//All the cases belonging to the training that will be deleted are obtained.
		var cases = _caseServices.GetCases(trainingToDelete.groupId);

		int result = _connection.Delete(trainingToDelete);
		int valueToReturn = 0;

		//If the elimination of the training is correct, then the cases corresponding to that training are eliminated.
		if (result!=0)
		{
			foreach (var _case in cases)
			{
				valueToReturn += _caseServices.DeleteCase(_case);
			}
			Debug.Log("Se borró el entrenamiento correctamente");
		} else {
			valueToReturn = 0;
			Debug.Log("No se borró el entrenamiento");
		}

		return valueToReturn;
	}

	/// <summary>
	/// Description of the method to update a training
	/// </summary>
	/// <param name="trainingid">
	/// Identifier of the training that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateTraining(int trainingid){

		var _groupServices = new GroupServices();

		var trainingToUpdate = GetTrainingId(trainingid);
		var cases = _caseServices.GetCases(trainingid);
		int valueToReturn = 0;
		int average = 0;
		
		if (trainingToUpdate.id!=0)
		{
			foreach (var _case in cases)
			{
				average += _case.percentage;
			}

			average = average/3;

			trainingToUpdate.percentage = average;
			trainingToUpdate.lastUpdate = DataBaseParametersCtrl.Ctrl.GetDateTime();

			valueToReturn = _connection.Update(trainingToUpdate, trainingToUpdate.GetType());

			if (valueToReturn!=0)
			{
				DataBaseParametersCtrl.Ctrl._trainingloaded = trainingToUpdate;
				_groupServices.UpdateGroup();
			}
		}
		return _connection.Update(trainingToUpdate, trainingToUpdate.GetType());
	}

	#region METHODS to get data to DB

	public void setDBToWeb(string methodToCall, int valueToResponse, Training training){

		//UserData tempUser = new UserData (player.id, player.cycle, game);
		string json = JsonUtility.ToJson (training, true);
		UnityWebRequest postRequest = SetJsonForm (json, methodToCall);
		if (postRequest != null){
			
				StartCoroutine (waitDB_ToCreateTraining (postRequest));

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

	IEnumerator waitDB_ToCreateTraining (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseCreateTraining resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseCreateTraining> (www.downloadHandler.text);
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
					_trainingGetToDB = resp.trainingCreated;
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

				yield return (waitDB_ToGetTrainingId (postRequest));

				break;

				case 3:

				yield return (waitDB_ToGetTraining (postRequest));
				
				break;
			}
        }

	IEnumerator waitDB_ToGetTrainingId (WWW www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseGetTrainingId resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseGetTrainingId> (www.text);
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
					_trainingGetToDB = resp.training;
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

	IEnumerator waitDB_ToGetTraining (WWW www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseGetTrainingGroupId resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseGetTrainingGroupId> (www.text);
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
					_trainingGetToDB = resp.training;
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

