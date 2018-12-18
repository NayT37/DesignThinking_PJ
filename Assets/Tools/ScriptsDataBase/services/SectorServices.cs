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
				empathyMapId = 0,
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
				empathyMapId = empathymapid,
				lastUpdate = date
		};

		//Start-Validation that the query is right
		
		int result = _connection.Insert (new_s);

		if (result != 0)
		{
			int value =_connection.Insert (new_s);

			if (value != 0)
				return new_s;
			else
				return _nullSector;
			
			
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
		
		var s = _connection.Table<Sector>().Where(x => x.empathyMapId == empathymapId).FirstOrDefault();

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

		int empathymapId = DataBaseParametersCtrl.Ctrl._sectorLoaded.empathyMapId;
		
		var s = _connection.Table<Sector>().Where(x => x.empathyMapId == empathymapId).FirstOrDefault();

		if (s == null)
			return _nullSector;	
		else 
			return s;
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
		return _connection.Table<Sector>().Where(x => x.empathyMapId == empathymapId);
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

		//Se debe tener el cuenta que al eliminar un proyecto de debe eliminar 
		//todo lo que continua hacia abajo en la jerarquia de la base de datos (problema, publico, etc)
		return _connection.Delete(sectorToDelete);
	}

	/// <summary>
	/// Description of the method to update a sector
	/// </summary>
	/// <param name="sectorToUpdate">
	/// An object of type sector that contain the sector that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateEmpathymap(Sector sectorToUpdate){
		return _connection.Update(sectorToUpdate, sectorToUpdate.GetType());
	}
}

