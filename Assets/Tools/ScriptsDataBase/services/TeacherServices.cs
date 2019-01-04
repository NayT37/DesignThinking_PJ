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

