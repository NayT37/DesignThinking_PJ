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
	
	public IEnumerable<Course> _coursesLoaded = new Course[]{
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
				id = DataBaseParametersCtrl.Ctrl.GenerateCodeToId(),
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

		string teacherId = DataBaseParametersCtrl.Ctrl._teacherLoggedIn.identityCard;
		return _connection.Query<Course> ("select * from Course where teacherIdentityCard = " + teacherId +" ORDER BY creationDate ASC");

		//valueToResponse = 3

		// string teacherId = DataBaseParametersCtrl.Ctrl._teacherLoggedIn.identityCard;
	
		//Conexión con base de datos en web 
		//StartCoroutine(GetToDB("getTeacherCourses/", teacherId, 3));

		//return _coursesLoaded;
	}

	public IEnumerable<Course> GetAllCourses(){

		//valueToResponse = 2 

		return _connection.Query<Course> ("select * from Course where id LIKE '%" + DataBaseParametersCtrl.Ctrl._teacherLoggedIn.identityCard +"%' ORDER BY creationDate ASC");
	}

	public int GetCoursesCount(){

		string teacherId = DataBaseParametersCtrl.Ctrl._teacherLoggedIn.identityCard;
		return _connection.Query<Course> ("select * from Course where teacherIdentityCard = " + teacherId +" ORDER BY creationDate ASC").Count;

		//valueToResponse = 4

		// string teacherId = DataBaseParametersCtrl.Ctrl._teacherLoggedIn.identityCard;

		// //Conexión con base de datos en web 
		// GetToDB("getCoursesCount", teacherId, 4);

		// while (!isQueryOk){}
		// isQueryOk = false;
		// return resultToDB;

	}


	/// <summary>
	/// Description of the method to obtain number the groups of a specific course
	/// </summary>
	/// A IEnumerable list of all the courses found from the identifier of the teacher loggedIn
	/// </returns>
	public int GetAllCoursesCount(){

		return _connection.Query<Course> ("select * from Course where id LIKE '%" + DataBaseParametersCtrl.Ctrl._teacherLoggedIn.identityCard +"%' ORDER BY creationDate ASC").Count;
	
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

		Int64 courseid = courseToDelete.id;
		
		int result = _connection.Delete(courseToDelete);

		//All the groups belonging to the course that will be deleted are obtained.
		var groups = _groupServices.GetGroups(courseid);

		Debug.Log(groups);

		int valueToReturn = 0;

		//If the elimination of the course is correct, then the groups corresponding to that course are eliminated.
		Debug.Log("CURSO BORRADO NICE");
		foreach (var group in groups)
		{
				Debug.Log(group);
				valueToReturn += _groupServices.DeleteGroup(group);
		}
		
		DataBaseParametersCtrl.Ctrl.isQueryOk = true;
		

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
		} else {
			valueToReturn = 0;
		}
		return valueToReturn;
	}
}
