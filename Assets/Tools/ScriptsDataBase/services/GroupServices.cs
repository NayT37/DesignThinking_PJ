using SQLite4Unity3d;
using UnityEngine;
using System;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class GroupServices  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private TrainingServices _trainingServices = new TrainingServices();

	private ProjectServices _projectServices = new ProjectServices();

	private Group _nullGroup = new Group{
				id = 0,
				name = "null",
				creationDate = "null",
				studentsCounter = 0,
				courseId = 0,
				lastUpdate = "null",
		};
	

	/// <summary>
	/// Description to method to create many groups
	/// </summary>
	/// <returns>
	/// A IEnumerable list of all the groups created
	/// </returns>


	public IEnumerable<Group> CreateGroups(){
		
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();
		var groups = new Group[]{};

		_connection.InsertAll (groups = new[]{
			new Group{
				name = "Jojoa-Team1",
				creationDate = date,
				studentsCounter = 5,
				courseId = 1,
				lastUpdate = date,
			},
			new Group{
				name = "Jojoa-Team2",
				creationDate = date,
				studentsCounter = 5,
				courseId = 1,
				lastUpdate = date,
			},
			new Group{
				name = "Jojoa-Team3",
				creationDate = date,
				studentsCounter = 5,
				courseId = 1,
				lastUpdate = date,
			}
		});

		return groups;

		//Debug.Log(_exception);
	}

	/// <summary>
	/// Description to method to create a group
	/// </summary>
	/// <param name="groupname">
	/// Attribute that contains the name to new group that will be created.
	/// </param>
	/// <param name="studentscounter">
	/// Attribute that contains the numbers of students to new group that will be created.
	/// </param>
	/// <returns>
	/// An object of type group with all the data of the group that was created.
	/// </returns>

	public int CreateGroup(string groupname, int studentscounter){

		//The identifier of the course loaded is obtained to be able to pass 
		//it as an attribute in the new group that will be created
		int courseid = DataBaseParametersCtrl.Ctrl._courseLoaded.id; //studentscounter;

		//Get the current date to create the new group
		string date = DataBaseParametersCtrl.Ctrl.GetDateTime();

		int valueToReturn = 0;

		//Creation of the new group
		var new_g = new Group{
				name = groupname,
				creationDate = date,
				studentsCounter = studentscounter,
				courseId = courseid,
				lastUpdate = date,
			};

		//Start-Validation that the group that will be created does not exist
		var groupValidation = GetGroupNamed(groupname, courseid);

		if ((groupValidation.name).Equals("null"))
		{
			int result = _connection.Insert (new_g);

			//If the creation of the group is correct, then the training corresponding to that group is created.
			if (result!=0){
				int value =_trainingServices.CreateTraining(new_g.id);
				valueToReturn = result;
				Debug.Log(new_g);
			}
		} else{
			valueToReturn = 0;
			Debug.Log("El grupo ya existe");
		}
		//End-Validation that the group that will be created does not exist

		return valueToReturn;
	}

	/// <summary>
	/// Description to method Get Group with the specified name
	/// </summary>
	/// <param name="groupName">
	/// Name of the group that will be searched
	/// </param>
	/// <param name="courseId">
	/// course identifier to find the correct group that will be searched
	/// </param>
	/// <returns>
	/// An object of type group with all the data of the group that was searched and if doesnt exist so return an empty group.
	/// </returns>
	public Group GetGroupNamed(string groupName, int courseId){
		
		var g = _connection.Table<Group>().Where(x => x.name == groupName).Where(x => x.courseId == courseId).FirstOrDefault();

		if (g == null)
			return _nullGroup;	
		else 
			return g;
	}

	/// <summary>
	/// Description to method Get Group that contain in the DataBaseParametersCtrl.!-- _groupLoaded
	/// </summary>
	/// <returns>
	/// An object of type group with all the data of the group that was searched and if doesnt exist so return an empty group.
	/// </returns>
	public Group GetGroupNamed(string groupName){

		int courseId = DataBaseParametersCtrl.Ctrl._courseLoaded.id;
		
		var g = _connection.Table<Group>().Where(x => x.name == groupName).Where(x => x.courseId == courseId).FirstOrDefault();

		if (g == null)
			return _nullGroup;	
		else {
			var training = _trainingServices.GetTraining(g.id);
			DataBaseParametersCtrl.Ctrl._trainingloaded = training;
			return g;

		}
		
	}

	/// <summary>
	/// Description to method Get group that contain in the DataBaseParametersCtrl.!-- _trainingLoaded
	/// </summary>
	/// <param name="groupid">
	/// integer to define the identifier of the group that will be searched.
	/// <returns>
	/// <returns>
	/// An object of type group with all the data of the group that was searched and if doesnt exist so return an empty group.
	/// </returns>
	public Group GetGroupId(int groupid){
		
		var g = _connection.Table<Group>().Where(x => x.id == groupid).FirstOrDefault();

		if (g == null)
			return _nullGroup;	
		else 
			return g;
	}

	/// <summary>
	/// Description of the method to obtain all the groups of a specific course
	/// </summary>
	/// <param name="courseId">
	/// integer to define the identifier of the course from which all the related groups will be brought.
	/// <returns>
	/// A IEnumerable list of all the groups found from the identifier of the course that was passed as a parameter
	/// </returns>
	public IEnumerable<Group> GetGroups(int courseId){
		return _connection.Table<Group>().Where(x => x.courseId == courseId);
	}

	/// <summary>
	/// (This is a test method) Description of the method to obtain all the groups
	/// </summary>
	/// <returns>
	/// A IEnumerable list of all the groups found
	/// </returns>
	public IEnumerable<Group> GetGroups(){
		return _connection.Table<Group>();
	}

	/// <summary>
	/// Description of the method to delete a group
	/// </summary>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteGroup(){

		var groupToDelete = DataBaseParametersCtrl.Ctrl._groupLoaded;

		int groupid = groupToDelete.id;
		//All the trainings belonging to the group that will be deleted are obtained.
		var training = _trainingServices.GetTraining(groupid);

		int result = _connection.Delete(groupToDelete);
		int valueToReturn = 0;

		//All the trainings belonging to the group that will be deleted are obtained.
		var projects = _projectServices.GetProjects(groupid);

		//If the elimination of the group is correct, then the trainings and projects corresponding to that group are eliminated.
		if (result!=0)
		{
			valueToReturn += _trainingServices.DeleteTraining(training);

			foreach (var p in projects)
			{
				valueToReturn += _projectServices.DeleteProject(p);
			}

			Debug.Log("Se borró el grupo correctamente");
		} else {
			valueToReturn = 0;
			Debug.Log("No se borró el grupo");
		}

		return valueToReturn;
	}

	/// <summary>
	/// Description of the method to delete a group
	/// </summary>
	/// <param name="groupToDelete">
	/// An object of type group that contain the group that will be deleted.
	/// <returns>
	/// An integer response of the query (0 = the object was not removed correctly. 1 = the object was removed correctly)
	/// </returns>
	public int DeleteGroup(Group groupToDelete){

		int groupid = groupToDelete.id;
		//All the trainings belonging to the group that will be deleted are obtained.
		var training = _trainingServices.GetTraining(groupid);

		int result = _connection.Delete(groupToDelete);
		int valueToReturn = 0;

		//All the trainings belonging to the group that will be deleted are obtained.
		var projects = _projectServices.GetProjects(groupid);

		//If the elimination of the group is correct, then the trainings and projects corresponding to that group are eliminated.
		if (result!=0)
		{
			valueToReturn += _trainingServices.DeleteTraining(training);

			foreach (var p in projects)
			{
				valueToReturn += _projectServices.DeleteProject(p);
			}
			
			Debug.Log("Se borró el grupo correctamente");
		} else {
			valueToReturn = 0;
			Debug.Log("No se borró el grupo");
		}

		return valueToReturn;
	}

	/// <summary>
	/// Description of the method to update a group
	/// </summary>
	/// <param name="newnamegroup">
	/// An string that contain the new name of the group that will be updated.
	/// <param name="newstudentscount">
	/// An integer that contain the new students quantity of the group that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateGroup( string newnamegroup, int newstudenscount){

		var gU = DataBaseParametersCtrl.Ctrl._groupLoaded;
		
		Debug.Log ("sssss    " + gU );
		gU.name = newnamegroup;
		gU.studentsCounter = newstudenscount;
		gU.lastUpdate = DataBaseParametersCtrl.Ctrl.GetDateTime();
		
		return _connection.Update(gU, gU.GetType());
	}

	/// <summary>
	/// Description of the method to update a group percentage
	/// </summary>
	/// <param name="groupid">
	/// An integer that contain the identifier of the group that will be updated.
	/// <param name="newpercentage">
	/// An integer that contain the new percentage of the group that will be updated.
	/// <returns>
	/// An integer response of the query (0 = the object was not updated correctly. 1 = the object was updated correctly)
	/// </returns>
	public int UpdateGroup(){

		int result = 0;
		
		var _courseServices = new CourseServices();

		var _projectServices = new ProjectServices();
		
		//Todavía falta agregar el proyecto y lo que respecta a eso!
		//Aunque queda la duda de si es necesario que el grupo tenga porcentaje
		var groupToUpdate = DataBaseParametersCtrl.Ctrl._groupLoaded;

		int percentageProjects = _projectServices.GetProjectsAverage();
		int percentageTraining = DataBaseParametersCtrl.Ctrl._trainingloaded.percentage;

		groupToUpdate.percentage = ((percentageProjects+percentageTraining)/2);
		groupToUpdate.lastUpdate = DataBaseParametersCtrl.Ctrl.GetDateTime();

		if (groupToUpdate.id != 0)
		{
			DataBaseParametersCtrl.Ctrl._groupLoaded = groupToUpdate;
			result = _connection.Update(groupToUpdate, groupToUpdate.GetType());

			if (result!=0)
			{
				Debug.Log(groupToUpdate);
				result = _courseServices.UpdateCourse();
			}
			
		} else {
		}
		
		

		return result;
	}
}
