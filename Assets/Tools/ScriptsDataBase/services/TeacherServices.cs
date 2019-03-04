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

public class TeacherServices:MonoBehaviour  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private Teacher _nullTeacher = new Teacher{
				identityCard = "null",
				documentTypeId = 0,
				names = "null",
				surnames = "null",
				phone = "null",
				address = "null",
				email = "null",
				password = "null",
				creationDate = "null",
				headquartersId = 0,
		};
	
	private bool isQueryOk = false;

	private Teacher _teacherGetToDB = new Teacher();

	private int resultToDB = 0;

	/// <summary>
	/// Description to method Get Teacher with the specified email and password
	/// </summary>
	/// <param name="teacherEmail">
	/// teacher email to validate if does exist 
	/// </param>
	/// <param name="password">
	/// teacher password to validate if does exist
	/// </param>
	/// <returns>
	/// An object of type teacher with all the data of the teacher that was searched and if doesnt exist so return an empty teacher.
	/// </returns>

	public Teacher GetTeacherNamed(string teacherEmail, string password, bool isFirstTime){

		//Get the current date to create the new empathymap
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		string p = DataBaseParametersCtrl.Ctrl.GenerateSHA512String(password);

		var teacher = new Teacher{
				identityCard = "1112",
				documentTypeId = 0,
				names = "null",
				surnames = "null",
				phone = "null",
				address = "null",
				email = teacherEmail,
				password = password,
				creationDate = date,
				headquartersId = 1
			};

		var teacherweb = new TeacherWeb{
					identityCard = "1112",
					documentTypeId = 0,
					names = "null",
					surnames = "null",
					phone = "null",
					address = "null",
					email = teacherEmail,
					password = password,
					creationDate = date,
					headquartersId = "1"
					};

		var t = new Teacher();
		try
		{
			t = _connection.Table<Teacher>().Where(x => x.email == teacherEmail).Where(x => x.password == p).FirstOrDefault();
		}
		catch (System.Exception)
		{
			
			Debug.Log(t);
		}
		

		if (isFirstTime)
		{
			Debug.Log("Validar en base de datos local antes de web primero...");

			if (t == null){
				
				Debug.Log("Validar en web...");

				var tc = _connection.Table<Teacher>().Where(x => x.email == teacherEmail).Where(x => x.password == p).FirstOrDefault();

				if (tc == null)
				{
					setDBToWeb("loginTeacher", teacherweb);
				} else {

					Debug.Log("El usuario ya existe en la base de datos local");
					return tc;
				}

				return _nullTeacher;
			} else {
				Debug.Log("Validar en base de datos local");
				
						DataBaseParametersCtrl.Ctrl._teacherLoggedIn = t;
						DataBaseParametersCtrl.Ctrl.isQueryOk = true;
						return t;
			}
		} else {
				Debug.Log("Validar en base de datos local....");
				
				if (t == null){
					Debug.Log("El profesor no existe en base de datos local");
					return _nullTeacher;
					
				}else{
					DataBaseParametersCtrl.Ctrl._teacherLoggedIn = t;
					DataBaseParametersCtrl.Ctrl.isQueryOk = true;
					return t;
				}
		}
		
	 }
	
	#region METHODS to get data to DB

	public void setDBToWeb(string methodToCall, TeacherWeb teacher){

		//UserData tempUser = new UserData (player.id, player.cycle, game);
		string json = JsonUtility.ToJson (teacher, true);
		UnityWebRequest postRequest = SetJsonForm (json, methodToCall);
		if (postRequest != null){	
			Debug.Log("in postNotNull");
			StartCoroutine(waitDB_ToGetTeacher (postRequest, methodToCall));
		}
			
	
	}

	private UnityWebRequest SetJsonForm (string json, string method) {
		try {
			// Hashtable headers = new Hashtable();
			// headers.Add("Content-Type", "application/json");
			// json = json.Replace("'", "\"");
			// //Encode the JSON string into a bytes
			// byte[] postData = System.Text.Encoding.UTF8.GetBytes (json);

			// WWW www = new WWW(DataBaseParametersCtrl.Ctrl._ipServer + method, postData, headers);
			UnityWebRequest web = UnityWebRequest.Put (DataBaseParametersCtrl.Ctrl._ipServer + method, json);
			Debug.Log(DataBaseParametersCtrl.Ctrl._ipServer + method);
			Debug.Log(json);
			web.SetRequestHeader("Access-Control-Allow-Credentials", "true");
			web.SetRequestHeader("Access-Control-Allow-Origin", "*");
			web.SetRequestHeader("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time,Authorization");
			web.SetRequestHeader("Access-Control-Allow-Methods", "GET,PUT,POST,DELETE,OPTIONS");
			web.SetRequestHeader ("Content-Type", "application/json");
			web.SetRequestHeader("X-Requested-With", "XMLHttpRequest");
	
			
			return web;
		} catch {
			return null;
		}
			
	}

	IEnumerator waitDB_ToGetTeacher (UnityWebRequest www, string method) {

				yield return www.SendWebRequest ();
				while (!www.isDone) {
					yield return null;
				}
				// Transformar la informacion obtenida (json) a Object (Response Class)
				ResponseGetTeacher resp = null;
				
				try {
					resp = JsonUtility.FromJson<ResponseGetTeacher> (www.downloadHandler.text);
					Debug.Log(www.downloadHandler.text);
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

						Debug.Log(resp.teacher);
						var tw = resp.teacher;

						Teacher newT = new Teacher(){
						identityCard = tw.identityCard,
						documentTypeId = tw.documentTypeId,
						names = tw.names,
						surnames = tw.surnames,
						phone = "",
						address = "",
						email = tw.email,
						password = tw.password,
						creationDate = tw.creationDate,
						headquartersId = int.Parse(tw.headquartersId),
						};
						
						int result = _connection.Insert(newT);
						if (result!=0){
							DataBaseParametersCtrl.Ctrl._teacherLoggedIn = newT;
						}else{
							DataBaseParametersCtrl.Ctrl._teacherLoggedIn = _nullTeacher;
						}	
						DataBaseParametersCtrl.Ctrl.isQueryOk = true;
						
						//Debug.Log(resp.teacher.ToString());
						} else { // no existen usuarios

							DataBaseParametersCtrl.Ctrl._teacherLoggedIn = _nullTeacher;
							DataBaseParametersCtrl.Ctrl.isQueryOk = true;
							DataBaseParametersCtrl.Ctrl.isNotTeacherExist = true;
						}

					} else { //Error en el servidor de base de datos
						Debug.Log ("user error: " + resp.error);
						try {

						} catch { }
						//HUDController.HUDCtrl.MessagePanel (resp.msg);
					}
			
			// yield return new WaitUntil(()=> www.isDone == true);

			
           
           
        
        yield return null;
    }

	#endregion

}

