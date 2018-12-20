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
	/// <param name="projectname">
	/// Attribute that contains an string with the name of the project that will be created.
	/// </param>
	/// <param name="sectorname">
	/// Attribute that contains an string with the sector's name of the project that will be created.
	/// </param>
	/// <returns>
	/// An object of type project with all the data of the project that was created.
	/// </returns>

	public Project CreateProject(string projectname, string sectorname){

		//The identifier of the group is obtained to be able to pass 
		//it as an attribute in the new project that will be created
		int groupId = DataBaseParametersCtrl.Ctrl._groupLoaded.id;

		//Get the current date to create the new course
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		//Creation of the new project
		var new_p  = new Project{
				name = projectname,
				percentage = 0,
				creationDate = date,
				sectorName = sectorname,
				groupId = groupId,
				lastUpdate = date
		};

		//Start-Validation that the project that will be created does not exist
		var projectValidation = GetProjectNamed(projectname, groupId);

		if ((projectValidation.name).Equals("null"))
		{
			int r = _connection.Insert (new_p);

			if (r!=0)
			{
				DataBaseParametersCtrl.Ctrl._projectLoaded = new_p;
				
				var e = _empathyMapServices.CreateEmpathymap();
				var s =_storytellingServices.CreateStoryTelling(1);

				if (e.id != 0 && s.id!=0)
					return new_p;
				else 
					return _nullProject;
				
			} else
			{
				return _nullProject;
			}
		} else {
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
	/// <param name="groupId">
	/// group identifier to find the correct project that will be searched
	/// </param>
	/// <returns>
	/// An object of type project with all the data of the project that was searched and if doesnt exist so return an empty project.
	/// </returns>
	public Project GetProjectNamed(string projectName, int groupId){
		
		var p = _connection.Table<Project>().Where(x => x.name == projectName).Where(x => x.groupId == groupId).FirstOrDefault();

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
		int groupId = DataBaseParametersCtrl.Ctrl._projectLoaded.groupId;
		
		var p = _connection.Table<Project>().Where(x => x.name == projectName).Where(x => x.groupId == groupId).FirstOrDefault();

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
	/// <param name="groupId">
	/// integer to define the identifier of the group from which all the related courses will be brought.
	/// <returns>
	/// A IEnumerable list of all the Trainings found from the identifier of the group that was passed as a parameter
	/// </returns>
	public IEnumerable<Project> GetProjects(int groupId){
		return _connection.Table<Project>().Where(x => x.groupId == groupId);
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

		//Se debe tener el cuenta que al eliminar un proyecto de debe eliminar 
		//todo lo que continua hacia abajo en la jerarquia de la base de datos (problema, publico, etc)
		return _connection.Delete(projectToDelete);
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
		
		var _publicServices = new PublicServices();

		var _problemServices = new ProblemServices();

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
			DataBaseParametersCtrl.Ctrl._projectLoaded = projectToUpdate;
			_groupServices.UpdateGroup();
		}
		return 
	}
}

