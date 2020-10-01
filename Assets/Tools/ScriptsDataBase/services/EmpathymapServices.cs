using SQLite4Unity3d;
using UnityEngine;
using System;
using UnityEngine.Networking;
using System.Collections;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class EmpathymapServices:MonoBehaviour {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private string[] arraysectorsname = new string[]{"sector_1","sector_2","sector_3", "sector_4", "sector_5", "sector_6"};

	private SectorServices _sectorServices = new SectorServices();

	private Empathymap _nullEmpathymap = new Empathymap{
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
	private Int64 checkId;
	public Empathymap CreateEmpathymap(){

		//valueToResponse = 1

		//The identifier of the project is obtained to be able to pass 
		//it as an attribute in the new empathymap that will be created
		Int64 projectid = DataBaseParametersCtrl.Ctrl._projectLoaded.id;

		//Get the current date to create the new empathymap
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		//Creation of the new empathymap
		var new_e = new Empathymap{
				id = DataBaseParametersCtrl.Ctrl.GenerateCodeToId(),
				percentage = 0,
				creationDate = date,
				lastUpdate = date,
				projectId = projectid
		};

		//Start-Validation that the query is right
		checkId = new_e.id;
        while (GetEmpathymapId(checkId).id == new_e.id)
        {
            new_e.id = DataBaseParametersCtrl.Ctrl.GenerateCodeToId();
        }
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

	public Empathymap GetEmpathymapId(Int64 empathymapid)
    {

        //valueToResponse = 2

        var e = _connection.Table<Empathymap>().Where(x => x.id == empathymapid).FirstOrDefault();

        if (e == null)
            return _nullEmpathymap;
        else
            return e;
    }

	public int GetEmpathymapAverage(){
		
		int counter = _sectorServices.GetSectorWithDescription();
		int result = ((counter*100)/6);
		return result;
	}


	/// <summary>
	/// Description of the method to obtain all the empathyMaps of a specific project
	/// </summary>
	/// <param name="projectId">
	/// integer to define the identifier of the project from which all the related empathymaps will be brought.
	/// <returns>
	/// An object of type Empathymap found from the identifier of the project that was passed as a parameter
	/// </returns>
	public Empathymap GetEmpathyMap(Int64 projectId){

		//valueToResponse = 2

		return _connection.Table<Empathymap>().Where(x => x.projectId == projectId).FirstOrDefault();
	}

	public IEnumerable<Empathymap> GetAllEmpathymaps(){

		//valueToResponse = 2 

		return _connection.Query<Empathymap> ("select * from Empathymap where id LIKE '%" + DataBaseParametersCtrl.Ctrl._teacherLoggedIn.identityCard +"%' ORDER BY creationDate ASC");
	}

	/// <summary>
	/// Description of the method to delete a empathymap
	/// </summary>
	/// <param name="empathymapToDelete">
	/// An object of type empathymap that contain the empathymap that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteEmpathymap(Empathymap empathymapToDelete){

		//valueToResponse = 3

		Int64 empathymapid = empathymapToDelete.id;

		// All the sectors belonging to the empathymap that will be deleted are obtained.
		var sectors = _sectorServices.GetSectors(empathymapid);

		int result = _connection.Delete(empathymapToDelete);

		int valueToReturn = 0;

		//If the elimination of the empathymap is correct, then the sectors corresponding to that empathymap are eliminated.
		if (result!=0)
		{
			foreach (var sector in sectors)
			{
				valueToReturn += _sectorServices.DeleteSector(sector);
			}
		} else {
			valueToReturn = 0;
		}

		return valueToReturn;
	}

	/// <summary>
	/// Description of the method to update a note
	/// </summary>
	/// <param name="empathymapid">
	/// Attribute that contains an integer with theempathymap identifier of the empathymap that will be created.
	/// <returns>
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateEmpathymap(){

		var empathymapToUpdate = DataBaseParametersCtrl.Ctrl._empathyMapLoaded;

		var projectServices = new ProjectServices();

		empathymapToUpdate.percentage = GetEmpathymapAverage();

		empathymapToUpdate.lastUpdate = DataBaseParametersCtrl.Ctrl.GetDateTime();

		int result = _connection.Update(empathymapToUpdate, empathymapToUpdate.GetType());

		if (result!=0)
		{
			DataBaseParametersCtrl.Ctrl._empathyMapLoaded = empathymapToUpdate;
			projectServices.UpdateProject(true);
		}

		return result;
	}
}

