using SQLite4Unity3d;
using UnityEngine;
using System;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class EmpathymapServices  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private EmpathyMap _nullEmpathymap = new EmpathyMap{
				id = 0,
				percentage = 0,
				creationDate = "null",
				lastUpdate = "null",
				projectId = 0
		};
	


	/// <summary>
	/// Description to method to create a empathyMap
	/// </summary>
	/// <param name="empathyMap">
	/// Attribute that contains an object of type empathyMap with all the data of the empathyMap that will be created.
	/// </param>
	/// <returns>
	/// An object of type empathyMap with all the data of the empathyMap that was created.
	/// </returns>

	public EmpathyMap CreateEmpathymap(EmpathyMap empathyMap){

		// var publicValidation = GetProblemNamed(empathyMap.name, empathyMap.projectId);

		// if ((publicValidation.name).Equals("null"))
		// {
			_connection.Insert (empathyMap);
			return empathyMap;
		// } else {
		// 	return _nullPublic;
		// }
		
		
	}

	/// <summary>
	/// Description to method Get EmpathyMap with the specified projectId
	/// </summary>
	/// <param name="projectId">
	/// project identifier to find the correct empathyMap that will be searched
	/// </param>
	/// <returns>
	/// An object of type empathyMap with all the data of the empathyMap that was searched and if doesnt exist so return an empty empathyMap.
	/// </returns>
	public EmpathyMap GetEmpathymapNamed( int projectId){
		
		var e = _connection.Table<EmpathyMap>().Where(x => x.projectId == projectId).FirstOrDefault();

		if (e == null)
			return _nullEmpathymap;	
		else 
			return e;
	}

	/// <summary>
	/// Description to method Get EmpathyMap that contain in the DataBaseParametersCtrl.!-- _empathyMapLoaded
	/// </summary>
	/// <returns>
	/// An object of type empathyMap with all the data of the empathyMap that was searched and if doesnt exist so return an empty empathyMap.
	/// </returns>
	public EmpathyMap GetEmpathymapNamed(){

		int projectId = DataBaseParametersCtrl.Ctrl._empathyMapLoaded.projectId;
		
		var e = _connection.Table<EmpathyMap>().Where(x => x.projectId == projectId).FirstOrDefault();

		if (e == null)
			return _nullEmpathymap;	
		else 
			return e;
	}

	/// <summary>
	/// Description of the method to obtain all the empathyMaps of a specific project
	/// </summary>
	/// <param name="projectId">
	/// integer to define the identifier of the project from which all the related empathymaps will be brought.
	/// <returns>
	/// A IEnumerable list of all the Empathymaps found from the identifier of the project that was passed as a parameter
	/// </returns>
	public IEnumerable<EmpathyMap> GetEmpathyMaps(int projectId){
		return _connection.Table<EmpathyMap>().Where(x => x.projectId == projectId);
	}

	/// <summary>
	/// (This is a test method) Description of the method to obtain all the Empathymaps
	/// </summary>
	/// <returns>
	/// A IEnumerable list of all the empathymaps found
	/// </returns>
	public IEnumerable<EmpathyMap> GetEmpathyMaps(){
		return _connection.Table<EmpathyMap>();
	}

	/// <summary>
	/// Description of the method to delete a empathymap
	/// </summary>
	/// <param name="empathymapToDelete">
	/// An object of type empathymap that contain the empathymap that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteEmpathymap(EmpathyMap empathymapToDelete){
		
		return _connection.Delete(empathymapToDelete);
	}

	/// <summary>
	/// Description of the method to update a empathyMap
	/// </summary>
	/// <param name="empathymapToUpdate">
	/// An object of type empathyMap that contain the empathyMap that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateEmpathymap(EmpathyMap empathymapToUpdate){
		return _connection.Update(empathymapToUpdate, empathymapToUpdate.GetType());
	}
}

