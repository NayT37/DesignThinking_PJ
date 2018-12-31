using SQLite4Unity3d;
using UnityEngine;
using System;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class ProjectServices  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private EmpathymapServices _empathyMapServices = new EmpathymapServices();
	private StorytellingServices _storytellingServices = new StorytellingServices();
	private PublicServices _publicServices = new PublicServices();
	private ProblemServices _problemServices = new ProblemServices();

	private int[] arrayversions = new int[]{1,2,3};

	private Project _nullProject = new Project{
				id = 0,
				name = "null",
				percentage = 0,
				creationDate = "null",
				sectorName = "null",
				groupId = 0,
				lastUpdate = "null"
		};
	


	/// <summary>
	/// Description to method to create a project
	/// </summary>
	/// <param name="sectorname">
	/// Attribute that contains an string with the sector's name of the project that will be created.
	/// </param>
	/// <returns>
	/// An object of type project with all the data of the project that was created.
	/// </returns>

	public Project CreateProject(string sectorname){

		//The identifier of the group is obtained to be able to pass 
		//it as an attribute in the new project that will be created
		int groupid = 1;//DataBaseParametersCtrl.Ctrl._groupLoaded.id;

		int counter = GetProjectsCounter(groupid);

		//Get the current date to create the new course
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		//Creation of the new project
		var new_p  = new Project{
				name = "Proyecto_"+(counter+1),
				percentage = 0,
				creationDate = date,
				sectorName = sectorname,
				groupId = groupid,
				lastUpdate = date
		};

		//Start-Validation that the project that will be created does not exist
		var projectValidation = GetProjectNamed(new_p.name, groupid);

		if ((projectValidation.name).Equals("null"))
		{
			int r = _connection.Insert (new_p);

			if (r!=0)
			{
				DataBaseParametersCtrl.Ctrl._projectLoaded = new_p;
				
				var e = _empathyMapServices.CreateEmpathymap();
				var s =_storytellingServices.CreateStoryTelling(1);

				if (e.id != 0 && s.id!=0){
					Debug.Log(new_p);
					return new_p;
				}else{
					Debug.Log("3");
					return _nullProject;
				}
				
			} else
			{
				Debug.Log("2");
				return _nullProject;
			}
		} else {
			Debug.Log("1");
			return _nullProject;
		}
		//End-Validation that the project that will be created does not exist
	}

	/// <summary>
	/// Description to method Get Project with the specified name and gropuId
	/// </summary>
	/// <param name="projectName">
	/// Name of the project that will be searched
	/// </param>
	/// <param name="groupid">
	/// group identifier to find the correct project that will be searched
	/// </param>
	/// <returns>
	/// An object of type project with all the data of the project that was searched and if doesnt exist so return an empty project.
	/// </returns>
	public Project GetProjectNamed(string projectName, int groupid){
		
		var p = _connection.Table<Project>().Where(x => x.name == projectName).Where(x => x.groupId == groupid).FirstOrDefault();

		if (p == null)
			return _nullProject;	
		else 
			return p;
	}

	/// <summary>
	/// Description to method Get Project that contain in the DataBaseParametersCtrl.!-- _projectLoaded
	/// </summary>
	/// <returns>
	/// An object of type project with all the data of the project that was searched and if doesnt exist so return an empty project.
	/// </returns>
	public Project GetProjectNamed(){

		string projectName = DataBaseParametersCtrl.Ctrl._projectLoaded.name;
		int projectid = DataBaseParametersCtrl.Ctrl._projectLoaded.id;
		
		var p = _connection.Table<Project>().Where(x => x.name == projectName).Where(x => x.id == projectid).FirstOrDefault();

		if (p == null)
			return _nullProject;	
		else 
			return p;
	}

	/// <summary>
	/// Description of the method to obtain the average percentage of all the projects with specified group identifier
	/// </summary>
	/// <returns>
	/// An integer with the average of all projects with specified project identifier
	/// </returns>
	public int GetProjectsAverage(){

		int groupid = DataBaseParametersCtrl.Ctrl._groupLoaded.id;
		
		var projects = _connection.Table<Project>().Where(x => x.groupId == groupid);
		int counter = 0;
		int sum = 0;
		int result = 0;

		foreach (var p in projects)
		{
			sum += p.percentage;
			counter++;
		}

		result = (sum/counter);
		return result;
	}

	/// <summary>
	/// Description of the method to obtain all the projects of a specific group
	/// </summary>
	/// <param name="projectid">
	/// integer to define the identifier of the project from which all the related courses will be brought.
	/// <returns>
	/// A IEnumerable list of all the Trainings found from the identifier of the group that was passed as a parameter
	/// </returns>
	public IEnumerable<Project> GetProjects(int projectid){
		return _connection.Table<Project>().Where(x => x.id == projectid);
	}

	/// <summary>
	/// Description of the method to obtain all the projects of a specific group
	/// </summary>
	/// <param name="groupId">
	/// integer to define the identifier of the group from which all the related courses will be brought.
	/// <returns>
	/// Counter projects related with an specified group
	/// </returns>
	public int GetProjectsCounter(int groupId){
		return _connection.Table<Project>().Where(x => x.groupId == groupId).Count();
	}

	/// <summary>
	/// (This is a test method) Description of the method to obtain all the Project
	/// </summary>
	/// <returns>
	/// A IEnumerable list of all the projects found
	/// </returns>
	public IEnumerable<Project> GetProjects(){
		return _connection.Table<Project>();
	}

	/// <summary>
	/// Description of the method to delete a project
	/// </summary>
	/// <param name="projectToDelete">
	/// An object of type project that contain the project that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteProject(Project projectToDelete){

		int projectid = projectToDelete.id;

		int result = _connection.Delete(projectToDelete);

		int valueToReturn = 0;

		
		//If the elimination of the group is correct, then the trainings and projects corresponding to that group are eliminated.
		if (result!=0)
		{

			var publicToDelete = _publicServices.GetPublicNamed(projectid);

			int resultPublicDeleted = _publicServices.DeletePublic(publicToDelete);

			var problemToDelete = _problemServices.GetProblem(projectid);

			int resultProblemDeleted = _problemServices.DeleteProblem(problemToDelete);

			var storyTellingsToDelete = _storytellingServices.GetStoryTellings(projectid);

			foreach (var st in storyTellingsToDelete)
			{
				_storytellingServices.DeleteStoryTelling(st);
			}

			var empathymapToDelete = _empathyMapServices.GetEmpathyMap(projectid);

			int resultEmpathyMapToDeleted = _empathyMapServices.DeleteEmpathymap(empathymapToDelete);
			
			Debug.Log("Se borró el grupo correctamente");
		} else {
			valueToReturn = 0;
			Debug.Log("No se borró el grupo");
		}

		return valueToReturn;
	}

	/// <summary>
	/// Description of the method to update a project
	/// </summary>
	/// <param name="newsector">
	/// An string that contain the newsector that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateProject(string newsector){

		var projectToUpdate = DataBaseParametersCtrl.Ctrl._projectLoaded;

		projectToUpdate.sectorName = newsector;
		projectToUpdate.lastUpdate = DataBaseParametersCtrl.Ctrl.GetDateTime();


		int result = _connection.Update(projectToUpdate, projectToUpdate.GetType());

		DataBaseParametersCtrl.Ctrl._projectLoaded = projectToUpdate;

		return result;
	}

	/// <summary>
	/// Description of the method to update a project
	/// </summary>
	/// <param name="projectToUpdate">
	/// An object of type project that contain the project that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateProject(bool isPercentageChanged){

		var _groupServices = new GroupServices();

		var projectToUpdate = DataBaseParametersCtrl.Ctrl._projectLoaded;

		int projectid = projectToUpdate.id;

		projectToUpdate.lastUpdate = DataBaseParametersCtrl.Ctrl.GetDateTime();

		if (isPercentageChanged){
			int percentagePublic = _publicServices.GetPublicNamed(projectid).percentage;
			int percentageStoryTelling = _storytellingServices.GetStorytellingAverage(projectid);
			int percentageEmpathymap = DataBaseParametersCtrl.Ctrl._empathyMapLoaded.percentage;
			int percentageProblem = DataBaseParametersCtrl.Ctrl._problemLoaded.percentage;
			projectToUpdate.percentage = ((percentagePublic+percentageStoryTelling+percentageEmpathymap+percentageProblem)/4);
		} 

		int result = _connection.Update(projectToUpdate, projectToUpdate.GetType());

		if (result != 0)
		{
			Debug.Log(projectToUpdate);
			DataBaseParametersCtrl.Ctrl._projectLoaded = projectToUpdate;
			_groupServices.UpdateGroup();
		}
		return result;
	}
}

