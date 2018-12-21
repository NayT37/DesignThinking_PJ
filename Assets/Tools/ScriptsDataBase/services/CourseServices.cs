using SQLite4Unity3d;
using UnityEngine;
using System;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class CourseServices  {

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
				name = "Course-1",
				percentage = 0,
				creationDate = date,
				teacherIdentityCard = "0",
				lastUpdate = date
			},
			new Course{
				name = "Course-2",
				percentage = 0,
				creationDate = date,
				teacherIdentityCard = "0",
				lastUpdate = date
			},
			new Course{
				name = "Course-3",
				percentage = 0,
				creationDate = date,
				teacherIdentityCard = "0",
				lastUpdate = date
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
		
		//The identifier of the teacher logged in is obtained to be able to pass 
		//it as an attribute in the new course that will be created
		string teacherid = "1143";//DataBaseParametersCtrl.Ctrl._teacherLoggedIn.identityCard;

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
			// DataBaseParametersCtrl.Ctrl._courseLoaded = c;
			return c;
		}
	}

	/// <summary>
	/// Description to method Get Course that contain in the DataBaseParametersCtrl.!-- _courseLoaded
	/// </summary>
	/// <returns>
	/// An object of type course with all the data of the course that was searched and if doesnt exist so return an empty course.
	/// </returns>
	public Course GetCourseNamed(){

		string courseName = DataBaseParametersCtrl.Ctrl._courseLoaded.name;
		string teacherId = DataBaseParametersCtrl.Ctrl._courseLoaded.teacherIdentityCard;
		
		var c = _connection.Table<Course>().Where(x => x.name == courseName).Where(x => x.teacherIdentityCard == teacherId).FirstOrDefault();

		if (c == null)
			return _nullCourse;	
		else 
			return c;
	}

	/// <summary>
	/// Description to method Get course that contain in the DataBaseParametersCtrl.!-- _courseLoaded
	/// </summary>
	/// <param name="courseId">
	/// integer to define the identifier of the course that will be searched.
	/// <returns>
	/// <returns>
	/// An object of type course with all the data of the course that was searched and if doesnt exist so return an empty course.
	/// </returns>
	public Course GetCourseId(int courseId){
		
		var c = _connection.Table<Course>().Where(x => x.id == courseId).FirstOrDefault();

		if (c == null)
			return _nullCourse;	
		else 
			return c;
	}

	/// <summary>
	/// Description of the method to obtain all the groups of a specific course
	/// </summary>
	/// A IEnumerable list of all the courses found from the identifier of the teacher loggedIn
	/// </returns>
	public IEnumerable<Course> GetCourses(){
		string teacherId = DataBaseParametersCtrl.Ctrl._teacherLoggedIn.identityCard;
		return _connection.Table<Course>().Where(x => x.teacherIdentityCard == teacherId);
	}

	/// <summary>
	/// (This is a test method) Description of the method to obtain all the courses
	/// </summary>
	/// <returns>
	/// A IEnumerable list of all the courses found
	/// </returns>
	// public IEnumerable<Course> GetCourses(){
	// 	return _connection.Table<Course>();
	// }

	/// <summary>
	/// Description of the method to delete a course
	/// </summary>
	/// <param name="courseToDelete">
	/// An object of type course that contain the course that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteCourse(Course courseToDelete){

		int courseid = courseToDelete.id;
		
		int result = _connection.Delete(courseToDelete);

		//All the groups belonging to the course that will be deleted are obtained.
		var groups = _groupServices.GetGroups(courseid);

		int valueToReturn = 0;

		//If the elimination of the course is correct, then the groups corresponding to that course are eliminated.
		if (result!=0)
		{
			foreach (var group in groups)
			{
				valueToReturn += _groupServices.DeleteGroup(group);
			}
			Debug.Log("Se borró el curso correctamente");
		} else {
			valueToReturn = 0;
			Debug.Log("No se borró el entrenamiento");
		}

		return valueToReturn;
	}

	/// <summary>
	/// Description of the method to update a course
	/// </summary>
	/// <param name="courseToUpdate">
	/// An object of type course that contain the course that will be updated.
	/// <param name="newnamecourse">
	/// An object of type course that contain the course that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateCourse(Course courseToUpdate, string newnamecourse){

		courseToUpdate.name = newnamecourse;
		courseToUpdate.lastUpdate = DataBaseParametersCtrl.Ctrl.GetDateTime();

		return _connection.Update(courseToUpdate, courseToUpdate.GetType());
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

