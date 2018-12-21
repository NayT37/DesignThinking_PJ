using SQLite4Unity3d;
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
	/// <param name="stringfield">
	/// Attribute that contains an string with the name of the field that will be created.
	/// </param>
	/// <param name="fielddescription">
	/// Attribute that contains an string with the description of the field that will be created.
	/// </param>
	/// <returns>
	/// An object of type field with all the data of the field that was created.
	/// </returns>

	public Field CreateField(string fieldname, string fielddescription){

		//The identifier of the project is obtained to be able to pass 
		//it as an attribute in the new problem that will be created
		int problemid = DataBaseParametersCtrl.Ctrl._problemLoaded.id;

		//Get the current date to create the new field
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		//Creation of the new problem
		var new_f = new Field{
				name = fieldname,
				description = fielddescription,
				percentage = 100,
				creationDate = date,
				lastUpdate = date,
				problemId = problemid
		};

		//Start-Validation that the query is right
		
		int result = _connection.Insert (new_f);

		if (result != 0)
		{
			Debug.Log(new_f);
			return new_f;
		}else {
			return _nullField;
		}
		
		
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
		int result = _connection.Delete(fieldToDelete);

		if (result!=0)
			Debug.Log("Se borró el campo correctamente");
		else
			Debug.Log("No se borró el campo");
		
		return result;
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

