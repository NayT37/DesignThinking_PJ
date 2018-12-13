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
	/// <param name="project">
	/// Attribute that contains an object of type project with all the data of the project that will be created.
	/// </param>
	/// <returns>
	/// An object of type project with all the data of the project that was created.
	/// </returns>

	public Project CreateProject(Project project){

		var projectValidation = GetProjectNamed(project.name, project.groupId);

		if ((projectValidation.name).Equals("null"))
		{
			_connection.Insert (project);
			return project;
		} else {
			return _nullProject;
		}
		
		
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
	public int UpdateProject(Project projectToUpdate){
		return _connection.Update(projectToUpdate, projectToUpdate.GetType());
	}
}

