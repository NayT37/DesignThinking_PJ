using SQLite4Unity3d;
using UnityEngine;
using System;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class TeacherServices  {

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
	public Teacher GetCourseNamed(string teacherEmail, string password){
		
		var t = _connection.Table<Teacher>().Where(x => x.email == teacherEmail).Where(x => x.identityCard == password).FirstOrDefault();

		if (t == null)
			return _nullTeacher;	
		else 
			return t;
	}

	/// <summary>
	/// Description to method Get teacher that contain in the DataBaseParametersCtrl.!-- _teacherLoggedIn
	/// </summary>
	/// <returns>
	/// An object of type teacher with all the data of the teacher that was searched and if doesnt exist so return an empty teacher.
	/// </returns>
	public Teacher GetTeacherNamed(){

		string teacherEmail = DataBaseParametersCtrl.Ctrl._teacherLoggedIn.email;
		string password = DataBaseParametersCtrl.Ctrl._teacherLoggedIn.password;
		
		var t = _connection.Table<Teacher>().Where(x => x.email == teacherEmail).Where(x => x.password == password).FirstOrDefault();

		if (t == null)
			return _nullTeacher;	
		else 
			return t;
	}


}

