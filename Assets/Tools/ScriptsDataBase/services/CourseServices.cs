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

	private Course _nullCourse = new Course{
				id = 0,
				name = "null",
				percentage = 0,
				creationDate = "null",
				teacherIdentityCard = 0,
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
				teacherIdentityCard = 0,
				lastUpdate = date
			},
			new Course{
				name = "Course-2",
				percentage = 0,
				creationDate = date,
				teacherIdentityCard = 0,
				lastUpdate = date
			},
			new Course{
				name = "Course-3",
				percentage = 0,
				creationDate = date,
				teacherIdentityCard = 0,
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
			_connection.Insert (new_c);
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
		else 
			return c;
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
	/// Description of the method to obtain all the groups of a specific course
	/// </summary>
	/// <param name="teacherId">
	/// integer to define the identifier of the teacher from which all the related courses will be brought.
	/// <returns>
	/// A IEnumerable list of all the courses found from the identifier of the teacher that was passed as a parameter
	/// </returns>
	public IEnumerable<Course> GetCourses(string teacherId){
		return _connection.Table<Course>().Where(x => x.teacherIdentityCard == teacherId);
	}

	/// <summary>
	/// (This is a test method) Description of the method to obtain all the courses
	/// </summary>
	/// <returns>
	/// A IEnumerable list of all the courses found
	/// </returns>
	public IEnumerable<Course> GetCourses(){
		return _connection.Table<Course>();
	}

	/// <summary>
	/// Description of the method to delete a course
	/// </summary>
	/// <param name="courseToDelete">
	/// An object of type course that contain the course that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteCourse(Course courseToDelete){
		return _connection.Delete(courseToDelete);
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
}

