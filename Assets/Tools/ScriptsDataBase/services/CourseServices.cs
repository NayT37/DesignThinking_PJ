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

public class CourseServices : MonoBehaviour  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;
	private GroupServices _groupServices = new GroupServices();
	private Course _nullCourse = new Course{
				id = 0,
				name = "null",
				percentage = 0,
				creationDate = "null",
				teacherIdentityCard = "0",
				lastUpdate = "null"
		};
	
	public Course[] _coursesLoaded = new Course[]{
		new Course{
				id = 0,
				name = "null",
				percentage = 0,
				creationDate = "null",
				teacherIdentityCard = "0",
				lastUpdate = "null"
		},
		new Course{
				id = 0,
				name = "null",
				percentage = 0,
				creationDate = "null",
				teacherIdentityCard = "0",
				lastUpdate = "null"
		},
		new Course{
				id = 0,
				name = "null",
				percentage = 0,
				creationDate = "null",
				teacherIdentityCard = "0",
				lastUpdate = "null"
		},
		new Course{
				id = 0,
				name = "null",
				percentage = 0,
				creationDate = "null",
				teacherIdentityCard = "0",
				lastUpdate = "null"
		},
		new Course{
				id = 0,
				name = "null",
				percentage = 0,
				creationDate = "null",
				teacherIdentityCard = "0",
				lastUpdate = "null"
		}};
	
	private bool isQueryOk = false;

	private Course _courseGetToDB = new Course();

	private int resultToDB = 0;

    public CourseServices()
    {
    }


    /// <summary>
    /// Description to method to create many courses
    /// </summary>
    /// <returns>
    /// A IEnumerable list of all the courses created
    /// </returns>


    public IEnumerable<Course> CreateCourses(){
		
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();
		var courses = new Course[]{};

		_connection.InsertAll (courses = new[]{
			new Course{
				name = "null",
				percentage = 0,
				creationDate = "null",
				teacherIdentityCard = "0",
				lastUpdate = "null"
			},
			new Course{
				name = "null",
				percentage = 0,
				creationDate = "null",
				teacherIdentityCard = "0",
				lastUpdate = "null"
			},
			new Course{
				name = "null",
				percentage = 0,
				creationDate = "null",
				teacherIdentityCard = "0",
				lastUpdate = "null"
			}
		});

		return courses;

		//Debug.Log(_exception);
	}

	/// <summary>
	/// Description to method to create a course
	/// </summary>
	/// <param name="coursename">
	/// Attribute that contains the name to new course that will be created.
	/// </param>
	/// <returns>
	/// An object of type course with all the data of the course that was created.
	/// </returns>

	public Course CreateCourse(string coursename){

		//valueToResponse = 1
		
		//The identifier of the teacher logged in is obtained to be able to pass 
		//it as an attribute in the new course that will be created
		string teacherid = DataBaseParametersCtrl.Ctrl._teacherLoggedIn.identityCard;

		//Get the current date to create the new course
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		//Creation of the new course
		var new_c = new Course{
				name = coursename,
				percentage = 0,
				creationDate = date,
				teacherIdentityCard = teacherid,
				lastUpdate = date
			};

		//Start-Validation that the course that will be created does not exist
		var courseValidation = GetCourseNamed(coursename, teacherid);

		if ((courseValidation.name).Equals("null"))
		{
			// setDBToWeb("createCourse", 1, new_c);

			// while (!isQueryOk){}
			// isQueryOk = false;
			// return _courseGetToDB;

			_connection.Insert (new_c);
			DataBaseParametersCtrl.Ctrl._courseLoaded = new_c;
			return new_c;
		} else {
			return _nullCourse;
		}
		//End-Validation that the course that will be created does not exist
		
	}

	/// <summary>
	/// Description to method Get Course with the specified name
	/// </summary>
	/// <param name="courseName">
	/// Name of the course that will be searched
	/// </param>
	/// <param name="teacherId">
	/// teacher identifier to find the correct course that will be searched
	/// </param>
	/// <returns>
	/// An object of type course with all the data of the course that was searched and if doesnt exist so return an empty course.
	/// </returns>
	public Course GetCourseNamed(string courseName, string teacherId){

		var c = _connection.Table<Course>().Where(x => x.name == courseName).Where(x => x.teacherIdentityCard == teacherId).FirstOrDefault();

		if (c == null)
			return _nullCourse;	
		else {
			DataBaseParametersCtrl.Ctrl._courseLoaded = c;
			return c;
		}
		//valueToResponse = 2
		// GetToDB("getCourseNamed/" + courseName + "/", teacherId, 2);

		// while (!isQueryOk){}
		// isQueryOk = false;
		// return _courseGetToDB;
		
	}

	/// <summary>
	/// Description of the method to obtain all the groups of a specific course
	/// </summary>
	/// A IEnumerable list of all the courses found from the identifier of the teacher loggedIn
	/// </returns>
	public IEnumerable<Course> GetCourses(){

		Debug.Log("metodo");

		string teacherId = "EHS2";//DataBaseParametersCtrl.Ctrl._teacherLoggedIn.identityCard;
		// return _connection.Table<Course>().Where(x => x.teacherIdentityCard == teacherId);

		//valueToResponse = 3

		// string teacherId = DataBaseParametersCtrl.Ctrl._teacherLoggedIn.identityCard;
	
		//Conexión con base de datos en web 
		StartCoroutine(GetToDB("getTeacherCourses/", teacherId, 3));

		return _coursesLoaded;
	}

	
	private IEnumerator getIsQueryOk(){

		Debug.Log("Waiting to get Courses...");
		yield return new WaitUntil(() => isQueryOk == true);
		// foreach (var item in _coursesLoaded)
		// {
		// 	Debug.Log(item.ToString());
		// }
		// Debug.Log("Finish to get Courses...");
	}

	/// <summary>
	/// Description of the method to obtain number the groups of a specific course
	/// </summary>
	/// A IEnumerable list of all the courses found from the identifier of the teacher loggedIn
	/// </returns>
	public int GetCoursesCount(){

		string teacherId = DataBaseParametersCtrl.Ctrl._teacherLoggedIn.identityCard;
		return _connection.Table<Course>().Where(x => x.teacherIdentityCard == teacherId).Count();

		//valueToResponse = 4

		// string teacherId = DataBaseParametersCtrl.Ctrl._teacherLoggedIn.identityCard;

		// //Conexión con base de datos en web 
		// GetToDB("getCoursesCount", teacherId, 4);

		// while (!isQueryOk){}
		// isQueryOk = false;
		// return resultToDB;

	}

	/// <summary>
	/// Description of the method to delete a course
	/// </summary>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteCourse(){

		var courseToDelete = DataBaseParametersCtrl.Ctrl._courseLoaded;

		int courseid = courseToDelete.id;
		
		int result = _connection.Delete(courseToDelete);

		//All the groups belonging to the course that will be deleted are obtained.
		//var groups = _groupServices.GetGroups(courseid);

		int valueToReturn = 0;

		//If the elimination of the course is correct, then the groups corresponding to that course are eliminated.
		if (result!=0)
		{
			// foreach (var group in groups)
			// {
			// 	valueToReturn += _groupServices.DeleteGroup(group);
			// }
			Debug.Log("Se borró el curso correctamente");
		} else {
			valueToReturn = 0;
			Debug.Log("No se borró el entrenamiento");
		}

		return valueToReturn;

		// //valueToResponse = 5

		// int courseid = courseToDelete.id;

		
		// setDBToWeb("deleteCourse", 5, courseToDelete);
		// while (!isQueryOk){}
		// isQueryOk = false;

		

		// return resultToDB;
	}

	/// <summary>
	/// Description of the method to update a course
	/// </summary>
	/// <param name="newnamecourse">
	/// An object of type course that contain the course that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateCourse(string newnamecourse){

		//valueToResponse = 6

		var courseToUpdate = DataBaseParametersCtrl.Ctrl._courseLoaded;

		courseToUpdate.name = newnamecourse;
		courseToUpdate.lastUpdate = DataBaseParametersCtrl.Ctrl.GetDateTime();

		DataBaseParametersCtrl.Ctrl._courseLoaded = courseToUpdate;

		return _connection.Update(courseToUpdate, courseToUpdate.GetType());
		
		// setDBToWeb("updateCourse", 6, courseToUpdate);

		// while (!isQueryOk){}
		// isQueryOk = false;

		// return resultToDB;
	}

	/// <summary>
	/// Description of the method to update a course
	/// </summary>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateCourse(){


		var courseToUpdate = DataBaseParametersCtrl.Ctrl._courseLoaded;

		var _groupServices = new GroupServices();

		var groups = _groupServices.GetGroups(courseToUpdate.id);

		int valueToReturn = 0;
		int percentagegroups = 0;
		int counter = 0;
		
		if (courseToUpdate.id!=0)
		{
			foreach (var group in groups)
			{
				counter++;
				percentagegroups += group.percentage;
			}

			int averagegroups = percentagegroups/counter;

			courseToUpdate.percentage = averagegroups;
			courseToUpdate.lastUpdate = DataBaseParametersCtrl.Ctrl.GetDateTime();

			valueToReturn = _connection.Update(courseToUpdate, courseToUpdate.GetType());
			Debug.Log(courseToUpdate);
		} else {
			valueToReturn = 0;
		}
		return valueToReturn;
	}

	
	#region METHODS to get data to DB

	public void setDBToWeb(string methodToCall, int valueToResponse, Course course){

		//UserData tempUser = new UserData (player.id, player.cycle, game);
		string json = JsonUtility.ToJson (course, true);
		UnityWebRequest postRequest = SetJsonForm (json, methodToCall);
		if (postRequest != null){
			switch(valueToResponse){
				case 1:

				StartCoroutine (waitDB_ToCreateCourse (postRequest));

				break;

				case 5:

				StartCoroutine (waitDB_ToDeleteCourse (postRequest));
				
				break;

				case 6:

				StartCoroutine (waitDB_ToUpdateCourse (postRequest));
				
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

	IEnumerator waitDB_ToCreateCourse (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseCreateCourse resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseCreateCourse> (www.downloadHandler.text);
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
					_courseGetToDB = resp.courseCreated;
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

	IEnumerator waitDB_ToDeleteCourse (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseDeleteCourse resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseDeleteCourse> (www.downloadHandler.text);
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

	IEnumerator waitDB_ToUpdateCourse (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseUpdateCourse resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseUpdateCourse> (www.downloadHandler.text);
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

            string url = DataBaseParametersCtrl.Ctrl._ipServer + methodToCall + parameterToGet;
			Debug.Log(url);
			WWW postRequest = new WWW (url); // buscar en el servidor al usuario
            switch(valueToResponse){
				case 2:

				yield return (waitDB_ToGetCourseNamed (postRequest));

				break;

				case 3:

				yield return (waitDB_ToGetCourses (postRequest));
				
				break;

				case 4:

				yield return (waitDB_ToGetCoursesCounter (postRequest));
				
				break;
			}
        }

	IEnumerator waitDB_ToGetCourseNamed (WWW www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseGetNamedCourse resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseGetNamedCourse> (www.text);
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
					_courseGetToDB = resp.courseNamed;
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

	private IEnumerator waitDB_ToGetCourses (WWW www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseGetCourses resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseGetCourses> (www.text);
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
					//_coursesLoaded = resp.courses;
					_coursesLoaded = resp.courses; 
					DataBaseParametersCtrl.Ctrl.isQueryOk = true;         
					// foreach (var item in _coursesLoaded)
					// {
					// 	Debug.Log(item.ToString());
					// }
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

	IEnumerator waitDB_ToGetCoursesCounter (WWW www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseGetCoursesCounter resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseGetCoursesCounter> (www.text);
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
					resultToDB = resp.counter;
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
