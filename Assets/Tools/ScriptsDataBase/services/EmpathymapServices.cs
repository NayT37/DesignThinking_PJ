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

	private string[] arraysectorsname = new string[]{"sector_1","sector_2","sector_3", "sector_4", "sector_5", "sector_6"};

	private SectorServices _sectorServices = new SectorServices();

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
	
	/// <returns>
	/// An object of type empathyMap with all the data of the empathyMap that was created.
	/// </returns>

	public EmpathyMap CreateEmpathymap(){

		//The identifier of the project is obtained to be able to pass 
		//it as an attribute in the new empathymap that will be created
		int projectid = DataBaseParametersCtrl.Ctrl._projectLoaded.id;

		//Get the current date to create the new empathymap
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		//Creation of the new empathymap
		var new_e = new EmpathyMap{
				percentage = 0,
				creationDate = date,
				lastUpdate = date,
				projectId = projectid
		};

		//Start-Validation that the query is right
		
		int result = _connection.Insert (new_e);

		if (result != 0)
		{
			DataBaseParametersCtrl.Ctrl._empathyMapLoaded = new_e;
			for (int i = 0; i < 6; i++)
			{
				//Creation of the sectors
				_sectorServices.CreateSector(arraysectorsname[i]);
			}
			return new_e;
		}else {
			return _nullEmpathymap;
		}
		//End-Validation that the query		
		
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

