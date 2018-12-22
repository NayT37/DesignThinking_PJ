﻿using SQLite4Unity3d;
using UnityEngine;
using System;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class MindmapServices  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private EvaluationServices _evaluationServices = new EvaluationServices();

	private SectionServices _sectionServices = new SectionServices();

	private string[] arraysectionsname = new string[]{"advantages","opportunities","-requirements","-how","risk_a", "risk_o"};
	private Mindmap _nullMindmap = new Mindmap{
				id = 0,
				percentage = 0,
				creationDate = "null",
				storytellingId = 0,
				image = "null",
				lastUpdate = "null",
				version = 0		
		};
	


	/// <summary>
	/// Description to method to create a mindmap
	/// </summary>
	/// <param name="versionmindmap">
	/// Attribute that contains an integer with version's value of the mindmap that will be created.
	/// </param>
	/// <returns>
	/// An object of type mindmap with all the data of the mindmap that was created.
	/// </returns>

	public Mindmap CreateMindMap(int versionmindmap){

		//The identifier of the storytelling is obtained to be able to pass 
		//it as an attribute in the new mindmap that will be created
		int storytellingid = DataBaseParametersCtrl.Ctrl._storyTellingLoaded.id;

		//Get the current date to create the new empathymap
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		//Creation of the new empathymap
		var new_m = new Mindmap{
				percentage = 0,
				creationDate = date,
				storytellingId = storytellingid,
				image = "",
				lastUpdate = date,
				version = versionmindmap
		};

		//Start-Validation that the query is right
		
		int result = _connection.Insert (new_m);

		int count = 0;

		if (result != 0)
		{

			DataBaseParametersCtrl.Ctrl._mindMapLoaded = new_m;
			//var e = _evaluationServices.CreateEvaluation();	

			for (int i = 0; i < 6; i++)
			{
				var s = _sectionServices.CreateSection(arraysectionsname[i]);

				if (s.id != 0)
					count++;

			}

			if (count == 6){
				Debug.Log(new_m);
				return new_m;
			}else 
				return _nullMindmap;
		}else
			return _nullMindmap;
	
		//End-Validation that the query		
		
	}

	/// <summary>
	/// Description to method Get Mindmap with the specified storytellingId
	/// </summary>
	/// <param name="storytellingId">
	/// storyTelling identifier to find the correct mindmap that will be searched
	/// </param>
	/// <returns>
	/// An object of type mindmap with all the data of the mindmap that was searched and if doesnt exist so return an empty mindmap.
	/// </returns>
	public Mindmap GetMindmapNamed( int storytellingId){
		
		var m = _connection.Table<Mindmap>().Where(x => x.storytellingId == storytellingId).FirstOrDefault();

		if (m == null)
			return _nullMindmap;	
		else 
			return m;
	}

	/// <summary>
	/// Description to method Get Mindmap with the specified storytellingId
	/// </summary>
	/// <param name="mindmapid">
	/// mindmap identifier to find the correct mindmap that will be searched
	/// </param>
	/// <returns>
	/// An object of type mindmap with all the data of the mindmap that was searched and if doesnt exist so return an empty mindmap.
	/// </returns>
	public Mindmap GetMindmapId( int mindmapid){
		
		var m = _connection.Table<Mindmap>().Where(x => x.id == mindmapid).FirstOrDefault();

		if (m == null)
			return _nullMindmap;	
		else{
			return m;
		}
	}

	/// <summary>
	/// Description to method Get Mindmap that contain in the DataBaseParametersCtrl.!-- _empathyMapLoaded
	/// </summary>
	/// <returns>
	/// An object of type mindmap with all the data of the mindmap that was searched and if doesnt exist so return an empty mindmap.
	/// </returns>
	public Mindmap GetMindmapNamed(){

		int storytellingId = DataBaseParametersCtrl.Ctrl._mindMapLoaded.storytellingId;
		
		var m = _connection.Table<Mindmap>().Where(x => x.storytellingId == storytellingId).FirstOrDefault();

		if (m == null)
			return _nullMindmap;	
		else 
			return m;
	}

	/// <summary>
	/// Description of the method to obtain all the notes of a specific project
	/// </summary>
	/// <param name="storytellingId">
	/// integer to define the identifier of the project from which all the related Notes will be brought.
	/// <returns>
	/// A IEnumerable list of all the Notes found from the identifier of the project that was passed as a parameter
	/// </returns>
	public IEnumerable<Mindmap> GetMindmaps(int storytellingId){
		return _connection.Table<Mindmap>().Where(x => x.storytellingId == storytellingId);
	}
	
	
	/// <summary>
	/// Description of the method to obtain the average percentage of all the mindmaps with specified storytelling identifier
	/// </summary>
	/// <param name="storyTellingid">
	/// storytelling identifier to find the correct storytelling that will be searched
	/// </param>
	/// <returns>
	/// An integer with the average of all mindmaps with specified storytelling identifier
	/// </returns>
	public int GetMindmapsAverage(int storyTellingId){
		
		var mindmaps = _connection.Table<Mindmap>().Where(x => x.storytellingId == storyTellingId);
		int counter = 0;
		int sum = 0;
		int result = 0;

		foreach (var m in mindmaps)
		{
			sum += m.percentage;
			counter++;
		}

		result = (sum/counter);
		return result;
	}

	/// <summary>
	/// (This is a test method) Description of the method to obtain all the Notes
	/// </summary>
	/// <returns>
	/// A IEnumerable list of all the notes found
	/// </returns>
	public IEnumerable<Mindmap> GetMindmaps(){
		return _connection.Table<Mindmap>();
	}

	/// <summary>
	/// Description of the method to delete a mindmap
	/// </summary>
	/// <param name="mindmapToDelete">
	/// An object of type mindmap that contain the mindmap that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteMindmap(Mindmap mindmapToDelete){
		
		int mindmapid = mindmapToDelete.id;

		int result = _connection.Delete(mindmapToDelete);

		int valueToReturn = 0;

		//If the elimination of the empathymap is correct, then the notes corresponding to that empathymap are eliminated.
		if (result!=0)
		{
			// All the evaluations belonging to the mindmap that will be deleted are obtained.
			var evaluation = _evaluationServices.GetEvaluationNamed(mindmapid);

			int resultToEvaluationDelete = _evaluationServices.DeleteEvaluation(evaluation);

			//All the sections belonging to the mindmap that will be deleted are obtained.
			var sections = _sectionServices.GetSections(mindmapid);

			foreach (var section in sections)
			{
				valueToReturn += _sectionServices.DeleteSection(section);
			}
			Debug.Log("Se borró el mindmap correctamente");
		} else {
			valueToReturn = 0;
			Debug.Log("No se borró el mindmap");
		}

		return valueToReturn;
	}

	/// <summary>
	/// Description of the method to update a moment
	/// </summary>
	/// <param name="mindmapid">
	/// An integer that contain the identifier section that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateMindmap(int mindmapid){

		var _storytellingServices = new StorytellingServices();

		var mindmapToUpdate = GetMindmapId(mindmapid);
		
		int counter = _sectionServices.GetSectionByAverage(mindmapid);

		int averageSections = ((counter*100)/6);

		int averageEvaluation = _evaluationServices.GetEvaluationNamed(mindmapid).percentage;

		mindmapToUpdate.percentage = ((averageEvaluation+averageSections)/2);
		mindmapToUpdate.lastUpdate = DataBaseParametersCtrl.Ctrl.GetDateTime();

		int result = _connection.Update(mindmapToUpdate, mindmapToUpdate.GetType());

		if (result!=0)
		{
			_storytellingServices.UpdateStoryTelling();
		}

		return result;
	}
}

