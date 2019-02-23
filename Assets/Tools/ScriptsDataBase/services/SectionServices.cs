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

public class SectionServices:MonoBehaviour  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private NodeServices _nodeServices = new NodeServices();

	private Section _nullSection = 
		new Section{
				id = 0,
				name = "null",
				creationDate = "null",
				mindmapId = 0,
				isOptional = false,
				lastUpdate = "null"			
		};
	

	private IEnumerable<Section> _sectionsLoaded = new Section[]{
		new Section{
				id = 0,
				name = "null",
				creationDate = "null",
				mindmapId = 0,
				isOptional = false,
				lastUpdate = "null"			
		},
		new Section{
				id = 0,
				name = "null",
				creationDate = "null",
				mindmapId = 0,
				isOptional = false,
				lastUpdate = "null"			
		},
		new Section{
				id = 0,
				name = "null",
				creationDate = "null",
				mindmapId = 0,
				isOptional = false,
				lastUpdate = "null"			
		},
		new Section{
				id = 0,
				name = "null",
				creationDate = "null",
				mindmapId = 0,
				isOptional = false,
				lastUpdate = "null"			
		}
	};

	/// <summary>
	/// Description to method to create a section
	/// </summary>
	/// <param name="sectionname">
	/// Attribute that contains an string with the name of the section that will be created.
	/// </param>
	/// <returns>
	/// An object of type section with all the data of the section that was created.
	/// </returns>

	public Section CreateSection(string sectionname){

		//valueToResponse = 1

		//The identifier of the mindmap is obtained to be able to pass 
		//it as an attribute in the new section that will be created
		Int64 mindmapid = DataBaseParametersCtrl.Ctrl._mindMapLoaded.id;

		//Get the current date to create the new section
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		//Creation of the new section
		var new_s = new Section{
				id = DataBaseParametersCtrl.Ctrl.GenerateCodeToId(),
				name = sectionname,
				creationDate = date,
				mindmapId = mindmapid,
				isOptional = false,
				lastUpdate = date			
		};

		//Start-Validation that the query is right
		
		int result = _connection.Insert (new_s);

		int count = 0;

		if (result != 0)
		{
			DataBaseParametersCtrl.Ctrl._sectionLoaded = new_s;
		
				for (int i = 0; i < 3; i++)
				{
					var e = _nodeServices.CreateNode();

					if (e.id != 0){
						count++;
					 	
					 }
				}

				DataBaseParametersCtrl.Ctrl.isQueryOk = true;

				if (count == 3){
					return new_s;
				}else
					return _nullSection;
		}else
			return _nullSection;
	
		//End-Validation that the query		
		
		
	}

	/// <summary>
	/// Description to method Get Section with the specified mindmapId
	/// </summary>
	/// <param name="sectionid">
	/// section identifier to find the correct section that will be searched
	/// </param>
	/// <returns>
	/// An object of type section with all the data of the section that was searched and if doesnt exist so return an empty section.
	/// </returns>
	public Section GetSectionId( Int64 sectionid){
		
		var s = _connection.Table<Section>().Where(x => x.id == sectionid).FirstOrDefault();

		if (s == null)
			return _nullSection;	
		else 
			return s;
	}

	/// <summary>
	/// Description to method Get Section with the specified mindmapId
	/// </summary>
	/// <param name="mindmapid">
	/// mindmap identifier to find the correct section that will be searched
	/// </param>
	/// <returns>
	/// An object of type section with all the data of the section that was searched and if doesnt exist so return an empty section.
	/// </returns>
	public int GetSectionByAverage(Int64 mindmapid){
		
		var sections = _connection.Table<Section>().Where(x => x.mindmapId == mindmapid).Where(x => x.name.StartsWith("-"));
		int counter = 0;

		foreach (var s in sections)
		{
			var nodes = _nodeServices.GetNodes(s.id);

			foreach (var node in nodes)
			{
				if (!node.description.Equals("")){
					counter++;
				}
			}
		}
		
		return counter;
	}


	/// <summary>
	/// Description of the method to obtain all the sections of a specific mindMap
	/// </summary>
	/// <param name="mindmapId">
	/// integer to define the identifier of the mindMap from which all the related Sections will be brought.
	/// <returns>
	/// A IEnumerable list of all the Sections found from the identifier of the mindMap that was passed as a parameter
	/// </returns>
	public IEnumerable<Section> GetSections(Int64 mindmapId){
		return _connection.Query<Section> ("select * from Section where mindmapId = " + mindmapId +" ORDER BY creationDate ASC");
	}

	public IEnumerable<Section> GetAllSections(){
		return _connection.Query<Section> ("select * from Section where id LIKE '%" + DataBaseParametersCtrl.Ctrl._teacherLoggedIn.identityCard +"%' ORDER BY creationDate ASC");
	
	}

	/// <summary>
	/// (This is a test method) Description of the method to obtain all the Sections
	/// </summary>
	/// <returns>
	/// A IEnumerable list of all the sections found
	/// </returns>
	public IEnumerable<Section> GetSections(){

		//valueToResponse = 2

		return _connection.Table<Section>();
	}

	/// <summary>
	/// Description of the method to delete a section
	/// </summary>
	/// <param name="sectionToDelete">
	/// An object of type section that contain the section that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteSection(Section sectionToDelete){

		//valueToResponse = 3

		Int64 sectionid = sectionToDelete.id;

		int result = _connection.Delete(sectionToDelete);

		int valueToReturn = 0;

		//If the elimination of the section is correct, then the nodes corresponding to that empathymap are eliminated.
		if (result!=0)
		{
			// All the nodes belonging to the section that will be deleted are obtained.
			var nodes = _nodeServices.GetNodes(sectionid);

			foreach (var node in nodes)
			{
				valueToReturn += _nodeServices.DeleteNode(node);
			}

			DataBaseParametersCtrl.Ctrl.isQueryOk = true;
		} else {
			valueToReturn = 0;
		}

		return valueToReturn;
	}

	/// <summary>
	/// Description of the method to update a moment
	/// </summary>
	/// <param name="sectionid">
	/// An integer that contain the identifier section that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateSection(Int64 sectionid){

		var sectionToUpdate = GetSectionId(sectionid);

		var _mindmapServices = new MindmapServices();

		sectionToUpdate.lastUpdate = DataBaseParametersCtrl.Ctrl.GetDateTime();

		int result = _connection.Update(sectionToUpdate, sectionToUpdate.GetType());

		if (result!=0)
		{
			_mindmapServices.UpdateMindmap(sectionToUpdate.mindmapId);
		}

		return result;
	}
}

