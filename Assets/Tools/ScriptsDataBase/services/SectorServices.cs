using SQLite4Unity3d;
using UnityEngine;
using System;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class SectorServices  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private Sector _nullSector = new Sector{
				id = 0,
				name = "null",
				creationDate = "null",
				empathymapId = 0,
				lastUpdate = "null"
		};
	


	/// <summary>
	/// Description to method to create a sector
	/// </summary>
	/// <param name="sectorname">
	/// Attribute that contains an string with the sector's name that will be created.
	/// </param>
	/// <returns>
	/// An object of type sector with all the data of the sector that was created.
	/// </returns>

	public Sector CreateSector(string sectorname){

		//The identifier of the empathymap is obtained to be able to pass 
		//it as an attribute in the new sector that will be created
		int empathymapid = DataBaseParametersCtrl.Ctrl._empathyMapLoaded.id;

		//Get the current date to create the new empathymap
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		//Creation of the new empathymap
		var new_s = new Sector{
				name = sectorname,
				creationDate = date,
				empathymapId = empathymapid,
				lastUpdate = date
		};

		//Start-Validation that the query is right
		
		int result = _connection.Insert (new_s);

		if (result != 0)
		{
			Debug.Log(new_s);
			return new_s;	
			
		}else {
			return _nullSector;
		}
		//End-Validation that the query		

			
		// } else {
		// 	return _nullPublic;
		// }
		
		
	}

	/// <summary>
	/// Description to method Get Sector with the specified empathymapId
	/// </summary>
	/// <param name="empathymapId">
	/// project identifier to find the correct sector that will be searched
	/// </param>
	/// <returns>
	/// An object of type sector with all the data of the sector that was searched and if doesnt exist so return an empty sector.
	/// </returns>
	public Sector GetSectorNamed( int empathymapId){
		
		var s = _connection.Table<Sector>().Where(x => x.empathymapId == empathymapId).FirstOrDefault();

		if (s == null)
			return _nullSector;	
		else 
			return s;
	}

	/// <summary>
	/// Description to method Get Sector that contain in the DataBaseParametersCtrl.!-- _sectorLoaded
	/// </summary>
	/// <returns>
	/// An object of type sector with all the data of the sector that was searched and if doesnt exist so return an empty sector.
	/// </returns>
	public Sector GetSectorNamed(){

		int empathymapId = DataBaseParametersCtrl.Ctrl._sectorLoaded.empathymapId;
		
		var s = _connection.Table<Sector>().Where(x => x.empathymapId == empathymapId).FirstOrDefault();

		if (s == null)
			return _nullSector;	
		else 
			return s;
	}

	/// <summary>
	/// Description to method Get Section with the specified empathymapid
	/// </summary>
	/// <returns>
	/// An object of type section with all the data of the section that was searched and if doesnt exist so return an empty section.
	/// </returns>
	public int GetSectorWithDescription(){

		int empathymapid = DataBaseParametersCtrl.Ctrl._empathyMapLoaded.id;	
		int counter = _connection.Table<Sector>().Where(x => x.empathymapId == empathymapid).Where(x => x.description != "").Count();
		return counter;
	}

	/// <summary>
	/// Description of the method to obtain all the sectors of a specific empathyMap
	/// </summary>
	/// <param name="empathymapId">
	/// integer to define the identifier of the empathyMap from which all the related problems will be brought.
	/// <returns>
	/// A IEnumerable list of all the sectorsa found from the identifier of the empathyMap that was passed as a parameter
	/// </returns>
	public IEnumerable<Sector> GetSectors(int empathymapId){
		return _connection.Table<Sector>().Where(x => x.empathymapId == empathymapId);
	}

	/// <summary>
	/// (This is a test method) Description of the method to obtain all the Problem
	/// </summary>
	/// <returns>
	/// A IEnumerable list of all the projects found
	/// </returns>
	public IEnumerable<Sector> GetSectors(){
		return _connection.Table<Sector>();
	}

	/// <summary>
	/// Description of the method to delete a empathymap
	/// </summary>
	/// <param name="sectorToDelete">
	/// An object of type empathymap that contain the empathymap that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteSector(Sector sectorToDelete){
		
		return _connection.Delete(sectorToDelete);
	}

	/// <summary>
	/// Description of the method to update a note
	/// </summary>
	/// <param name="newdescription">
	/// Attribute that contains an string with the new description of the sector that will be created.
	/// <returns>
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateSector(string newdescription){

		var sectorToUpdate = DataBaseParametersCtrl.Ctrl._sectorLoaded;

		var empathymapServices = new EmpathymapServices();

		sectorToUpdate.lastUpdate = DataBaseParametersCtrl.Ctrl.GetDateTime();

		sectorToUpdate.description = newdescription;

		int result = _connection.Update(sectorToUpdate, sectorToUpdate.GetType());

		if (result!=0)
		{
			DataBaseParametersCtrl.Ctrl._sectorLoaded = sectorToUpdate;
			empathymapServices.UpdateEmpathymap();
		}

		return result;
	}
}

