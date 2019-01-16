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

public class ProjectServices:MonoBehaviour  {

	private SQLiteConnection _connection = DataBaseParametersCtrl.Ctrl._sqliteConnection;

	private EmpathymapServices _empathyMapServices = new EmpathymapServices();
	private StorytellingServices _storytellingServices = new StorytellingServices();
	private PublicServices _publicServices = new PublicServices();
	private ProblemServices _problemServices = new ProblemServices();

	private int[] arrayversions = new int[]{1,2,3};

	private Project _nullProject = 
		new Project{
				id = 0,
				name = "null",
				percentage = 0,
				creationDate = "null",
				sectorName = "null",
				groupId = 0,
				lastUpdate = "null"
		};

	private bool isQueryOk = false;

	private Project _projectGetToDB = new Project();

	private int resultToDB = 0;

	private IEnumerable<Project> _projectsLoaded = new Project[]{
		new Project{
				id = 0,
				name = "null",
				percentage = 0,
				creationDate = "null",
				sectorName = "null",
				groupId = 0,
				lastUpdate = "null"
		},
		new Project{
				id = 0,
				name = "null",
				percentage = 0,
				creationDate = "null",
				sectorName = "null",
				groupId = 0,
				lastUpdate = "null"
		},
		new Project{
				id = 0,
				name = "null",
				percentage = 0,
				creationDate = "null",
				sectorName = "null",
				groupId = 0,
				lastUpdate = "null"
		},
		new Project{
				id = 0,
				name = "null",
				percentage = 0,
				creationDate = "null",
				sectorName = "null",
				groupId = 0,
				lastUpdate = "null"
		}
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

		//valueToResponse = 1

		//The identifier of the group is obtained to be able to pass 
		//it as an attribute in the new project that will be created
		int groupid = DataBaseParametersCtrl.Ctrl._groupLoaded.id;

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
		//var projectValidation = GetProjectNamed(new_p.name, groupid);

		//if ((projectValidation.name).Equals("null"))
		//{
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
		
		//valueToResponse = 2
		
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
		
		//valueToResponse = 3

		return _connection.Table<Project>().Where(x => x.groupId == groupId).Count();
	}


	/// <summary>
	/// (This is a test method) Description of the method to obtain all the Project
	/// </summary>
	/// <returns>
	/// A IEnumerable list of all the projects found
	/// </returns>
	public IEnumerable<Project> GetProjects(){

		//valueToResponse = 4
		int groupid = DataBaseParametersCtrl.Ctrl._groupLoaded.id;
		return _connection.Table<Project>().Where(x => x.groupId == groupid);
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

		//valueToResponse = 5

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

			var storyTellingsToDelete = _storytellingServices.GetStoryTellings();

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

		//valueToResponse = 6

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
			int percentageProblem = 0;
			if (DataBaseParametersCtrl.Ctrl._problemLoaded != null)
			{
				percentageProblem = DataBaseParametersCtrl.Ctrl._problemLoaded.percentage;
			}
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

		#region METHODS to get data to DB

	public void setDBToWeb(string methodToCall, int valueToResponse, Project project){

		//UserData tempUser = new UserData (player.id, player.cycle, game);
		string json = JsonUtility.ToJson (project, true);
		UnityWebRequest postRequest = SetJsonForm (json, methodToCall);
		if (postRequest != null){
			switch(valueToResponse){
				case 1:

				StartCoroutine (waitDB_ToCreateProject (postRequest));

				break;

				case 5:

				StartCoroutine (waitDB_ToDeleteProject (postRequest));
				
				break;

				case 6:

				StartCoroutine (waitDB_ToUpdateProject (postRequest));
				
				break;
			}
		}
			
	
	}

	private UnityWebRequest SetJsonForm (string json, string method) {
		try {
			UnityWebRequest web = UnityWebRequest.Put (DataBaseParametersCtrl.Ctrl._ipServer + method + "/put", json);
			web.SetRequestHeader ("Content-Type", "application/json");
			return web;
		} catch {
			return null;
		}
	}

	IEnumerator waitDB_ToCreateProject (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseCreateProject resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseCreateProject> (www.downloadHandler.text);
            } catch { }

            //Validacion de la informacion obtenida
            if (!string.IsNullOrEmpty (www.error) && resp == null) { //Error al descargar data
                Debug.Log (www.error);
                try {

                } catch (System.Exception e) { Debug.Log (e); }
                yield return null;
            } else

            if (resp != null) { // Informacion obtenida exitosamente
                if (!resp.error) { // sin error en el servidor
					_projectGetToDB = resp.projectCreated;
					isQueryOk = true;
                    } else { // no existen usuarios
                    }

                } else { //Error en el servidor de base de datos
                    // Debug.Log ("user error: " + resp.error);
                    try {

                    } catch { }
                    // HUDController.HUDCtrl.MessagePanel (resp.msg);
                }
            }
        
        yield return null;
    }

	IEnumerator waitDB_ToDeleteProject (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseDeleteProject resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseDeleteProject> (www.downloadHandler.text);
            } catch { }

            //Validacion de la informacion obtenida
            if (!string.IsNullOrEmpty (www.error) && resp == null) { //Error al descargar data
                Debug.Log (www.error);
                try {

                } catch (System.Exception e) { Debug.Log (e); }
                yield return null;
            } else

            if (resp != null) { // Informacion obtenida exitosamente
                if (!resp.error) { // sin error en el servidor
					resultToDB = resp.result;
					isQueryOk = true;
                    } else { // no existen usuarios
                    }

                } else { //Error en el servidor de base de datos
                    // Debug.Log ("user error: " + resp.error);
                    try {

                    } catch { }
                    // HUDController.HUDCtrl.MessagePanel (resp.msg);
                }
            }
        
        yield return null;
    }

	IEnumerator waitDB_ToUpdateProject (UnityWebRequest www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseUpdateProject resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseUpdateProject> (www.downloadHandler.text);
            } catch { }

            //Validacion de la informacion obtenida
            if (!string.IsNullOrEmpty (www.error) && resp == null) { //Error al descargar data
                Debug.Log (www.error);
                try {

                } catch (System.Exception e) { Debug.Log (e); }
                yield return null;
            } else

            if (resp != null) { // Informacion obtenida exitosamente
                if (!resp.error) { // sin error en el servidor
					resultToDB = resp.result;
					isQueryOk = true;
                    } else { // no existen usuarios
                    }

                } else { //Error en el servidor de base de datos
                    // Debug.Log ("user error: " + resp.error);
                    try {

                    } catch { }
                    // HUDController.HUDCtrl.MessagePanel (resp.msg);
                }
            }
        
        yield return null;
    }

	#endregion

	#region METHODS to get data to DB
	public IEnumerator GetToDB (string methodToCall, string parameterToGet, int valueToResponse) {

            WWW postRequest = new WWW (DataBaseParametersCtrl.Ctrl._ipServer + methodToCall + parameterToGet); // buscar en el servidor al usuario
            switch(valueToResponse){
				case 2:

				yield return (waitDB_ToGetProject (postRequest));

				break;

				case 3:

				yield return (waitDB_ToGetProjectsCounter (postRequest));
				
				break;

				case 4:

				yield return (waitDB_ToGetProjects (postRequest));
				
				break;
			}
        }

	IEnumerator waitDB_ToGetProject (WWW www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseGetProjectId resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseGetProjectId> (www.text);
            } catch { }

            //Validacion de la informacion obtenida
            if (!string.IsNullOrEmpty (www.error) && resp == null) { //Error al descargar data
                Debug.Log (www.error);
                try {

                } catch (System.Exception e) { Debug.Log (e); }
                yield return null;
            } else

            if (resp != null) { // Informacion obtenida exitosamente
                if (!resp.error) { // sin error en el servidor
					_projectGetToDB = resp.project;
					isQueryOk = true;
                    } else { // no existen usuarios
                    }

                } else { //Error en el servidor de base de datos
                    // Debug.Log ("user error: " + resp.error);
                    try {

                    } catch { }
                    // HUDController.HUDCtrl.MessagePanel (resp.msg);
                }
            }
        
        yield return null;
    }

	IEnumerator waitDB_ToGetProjects (WWW www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseGetProjects resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseGetProjects> (www.text);
            } catch { }

            //Validacion de la informacion obtenida
            if (!string.IsNullOrEmpty (www.error) && resp == null) { //Error al descargar data
                Debug.Log (www.error);
                try {

                } catch (System.Exception e) { Debug.Log (e); }
                yield return null;
            } else

            if (resp != null) { // Informacion obtenida exitosamente
                if (!resp.error) { // sin error en el servidor
					_projectsLoaded = resp.projects;
					isQueryOk = true;
                    } else { // no existen usuarios
                    }

                } else { //Error en el servidor de base de datos
                    // Debug.Log ("user error: " + resp.error);
                    try {

                    } catch { }
                    // HUDController.HUDCtrl.MessagePanel (resp.msg);
                }
            }
        
        yield return null;
    }

	IEnumerator waitDB_ToGetProjectsCounter (WWW www) {
        using (www) {
            while (!www.isDone) {
                yield return null;
            }
            // Transformar la informacion obtenida (json) a Object (Response Class)
			ResponseGetProjectsCounter resp = null;
			
            try {
                resp = JsonUtility.FromJson<ResponseGetProjectsCounter> (www.text);
            } catch { }

            //Validacion de la informacion obtenida
            if (!string.IsNullOrEmpty (www.error) && resp == null) { //Error al descargar data
                Debug.Log (www.error);
                try {

                } catch (System.Exception e) { Debug.Log (e); }
                yield return null;
            } else

            if (resp != null) { // Informacion obtenida exitosamente
                if (!resp.error) { // sin error en el servidor
					resultToDB = resp.counter;
					isQueryOk = true;
                    } else { // no existen usuarios
                    }

                } else { //Error en el servidor de base de datos
                    // Debug.Log ("user error: " + resp.error);
                    try {

                    } catch { }
                    // HUDController.HUDCtrl.MessagePanel (resp.msg);
                }
            }
        
        yield return null;
    }

	#endregion
}

