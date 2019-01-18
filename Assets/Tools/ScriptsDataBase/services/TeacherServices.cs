using SQLite4Unity3d;
using UnityEngine;
using System;
using System.Collections;
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

		if (isFirstTime)
		{
			Debug.Log("Validar en Web y luego crear el usuario local");
			return _nullTeacher;
		} else {
			Debug.Log("Validar en base de datos local");
			var t = _connection.Table<Teacher>().Where(x => x.email == teacherEmail).Where(x => x.password == password).FirstOrDefault();

			if (t == null){
			
			//Get the current date to create the new empathymap
			string date = DataBaseParametersCtrl.Ctrl.GetDateTime();


			var teacher = new Teacher{
					identityCard = teacherEmail + "-" + password,
					documentTypeId = 1,
					names = "test",
					surnames = "sapare",
					phone = "",
					address = "",
					email = teacherEmail,
					password = password,
					creationDate = date,
					headquartersId = 1,
			};

			int result = _connection.Insert(teacher);

			if (result!=0){
				DataBaseParametersCtrl.Ctrl._teacherLoggedIn = teacher;
				return teacher;
			}else
				return _nullTeacher;
			}	
			else{
				DataBaseParametersCtrl.Ctrl._teacherLoggedIn = t;
				return t;
			}
		}
	}

	#region METHODS to get data to DB
	public IEnumerator GetToDB (string methodToCall, string parameterToGet, int valueToResponse) {

            WWW postRequest = new WWW (DataBaseParametersCtrl.Ctrl._ipServer + methodToCall + parameterToGet); // buscar en el servidor al usuario
           
			yield return (waitDB_ToGetTeacher (postRequest));

        }

	IEnumerator waitDB_ToGetTeacher (WWW www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseGetTeacher resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseGetTeacher> (www.text);
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
					_teacherGetToDB = resp.teacher;
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

