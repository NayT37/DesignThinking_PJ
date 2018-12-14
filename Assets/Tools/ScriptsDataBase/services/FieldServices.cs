﻿using SQLite4Unity3d;
using UnityEngine;
using System;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class FieldServices  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private Field _nullField = new Field{
				id = 0,
				name = "null",
				description = "null",
				percentage = 0,
				creationDate = "null",
				lastUpdate = "null",
				problemId = 0
		};
	


	/// <summary>
	/// Description to method to create a field
	/// </summary>
	/// <param name="field">
	/// Attribute that contains an object of type field with all the data of the field that will be created.
	/// </param>
	/// <returns>
	/// An object of type field with all the data of the field that was created.
	/// </returns>

	public Field CreateField(Field field){

		// var problemValidation = GetFieldNamed(field.name, field.problemId);

		// if ((problemValidation.name).Equals("null"))
		// {
			_connection.Insert (field);
			return field;
		// } else {
		// 	return _nullField;
		// }
		
		
	}

	/// <summary>
	/// Description to method Get Field with the specified name and problemId
	/// </summary>
	/// <param name="fieldName">
	/// Name of the field that will be searched
	/// </param>
	/// <param name="problemId">
	/// problem identifier to find the correct field that will be searched
	/// </param>
	/// <returns>
	/// An object of type field with all the data of the field that was searched and if doesnt exist so return an empty field.
	/// </returns>
	public Field GetFieldNamed(string fieldName, int problemId){
		
		var f = _connection.Table<Field>().Where(x => x.name == fieldName).Where(x => x.problemId == problemId).FirstOrDefault();

		if (f == null)
			return _nullField;	
		else 
			return f;
	}

	/// <summary>
	/// Description to method Get Field that contain in the DataBaseParametersCtrl.!-- _fieldLoaded
	/// </summary>
	/// <returns>
	/// An object of type field with all the data of the field that was searched and if doesnt exist so return an empty field.
	/// </returns>
	public Field GetFieldNamed(){

		string fieldName = DataBaseParametersCtrl.Ctrl._fieldLoaded.name;
		int problemId = DataBaseParametersCtrl.Ctrl._fieldLoaded.problemId;
		
		var f = _connection.Table<Field>().Where(x => x.name == fieldName).Where(x => x.problemId == problemId).FirstOrDefault();

		if (f == null)
			return _nullField;	
		else 
			return f;
	}

	/// <summary>
	/// Description of the method to obtain all the fields of a specific project
	/// </summary>
	/// <param name="problemId">
	/// integer to define the identifier of the problem from which all the related fields will be brought.
	/// <returns>
	/// A IEnumerable list of all the Problems found from the identifier of the project that was passed as a parameter
	/// </returns>
	public IEnumerable<Field> GetFields(int problemId){
		return _connection.Table<Field>().Where(x => x.problemId == problemId);
	}

	/// <summary>
	/// (This is a test method) Description of the method to obtain all the Field
	/// </summary>
	/// <returns>
	/// A IEnumerable list of all the projects found
	/// </returns>
	public IEnumerable<Field> GetFields(){
		return _connection.Table<Field>();
	}

	/// <summary>
	/// Description of the method to delete a field
	/// </summary>
	/// <param name="fieldToDelete">
	/// An object of type field that contain the field that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteField(Field fieldToDelete){
		return _connection.Delete(fieldToDelete);
	}

	/// <summary>
	/// Description of the method to update a field
	/// </summary>
	/// <param name="fieldToUpdate">
	/// An object of type field that contain the field that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateField(Field fieldToUpdate){
		return _connection.Update(fieldToUpdate, fieldToUpdate.GetType());
	}
}
