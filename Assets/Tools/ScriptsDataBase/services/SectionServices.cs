using SQLite4Unity3d;
using UnityEngine;
using System;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class SectionServices  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private Section _nullSection = new Section{
				id = 0,
				name = "null",
				creationDate = "null",
				mindMapId = 0,
				isOptional = false,
				lastUpdate = "null"			
		};
	


	/// <summary>
	/// Description to method to create a section
	/// </summary>
	/// <param name="section">
	/// Attribute that contains an object of type section with all the data of the section that will be created.
	/// </param>
	/// <returns>
	/// An object of type section with all the data of the section that was created.
	/// </returns>

	public Section CreateSection(Section section){

		// var publicValidation = GetProblemNamed(section.name, section.mindmapId);

		// if ((publicValidation.name).Equals("null"))
		// {
			_connection.Insert (section);
			return section;
		// } else {
		// 	return _nullPublic;
		// }
		
		
	}

	/// <summary>
	/// Description to method Get Section with the specified mindmapId
	/// </summary>
	/// <param name="mindmapId">
	/// mindMap identifier to find the correct section that will be searched
	/// </param>
	/// <returns>
	/// An object of type section with all the data of the section that was searched and if doesnt exist so return an empty section.
	/// </returns>
	public Section GetSectionNamed( int mindmapId){
		
		var s = _connection.Table<Section>().Where(x => x.mindMapId == mindmapId).FirstOrDefault();

		if (s == null)
			return _nullSection;	
		else 
			return s;
	}

	/// <summary>
	/// Description of the method to obtain all the sections of a specific mindMap
	/// </summary>
	/// <param name="mindmapId">
	/// integer to define the identifier of the mindMap from which all the related Sections will be brought.
	/// <returns>
	/// A IEnumerable list of all the Sections found from the identifier of the mindMap that was passed as a parameter
	/// </returns>
	public IEnumerable<Section> GetSections(int mindmapId){
		return _connection.Table<Section>().Where(x => x.mindMapId == mindmapId);
	}

	/// <summary>
	/// (This is a test method) Description of the method to obtain all the Sections
	/// </summary>
	/// <returns>
	/// A IEnumerable list of all the sections found
	/// </returns>
	public IEnumerable<Section> GetSections(){
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
	public int DeleteEmpathymap(Section sectionToDelete){

		return _connection.Delete(sectionToDelete);
	}

	/// <summary>
	/// Description of the method to update a section
	/// </summary>
	/// <param name="sectionToUpdate">
	/// An object of type section that contain the section that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateEmpathymap(Section sectionToUpdate){
		return _connection.Update(sectionToUpdate, sectionToUpdate.GetType());
	}
}

