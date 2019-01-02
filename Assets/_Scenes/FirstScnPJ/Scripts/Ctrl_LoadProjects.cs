using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class Ctrl_LoadProjects : MonoBehaviour {


	public GameObject prefab_project;
	public GameObject parent_project;

	private ProjectServices _projectServices;
	private Text _nameConstant;

	private Project[] _projects;


	// Use this for initialization
	void Start () {
		
		
		// for (int i = 0; i < 8; i++)
		// {
		// 	Instantiate (prefab_project, parent_project.transform);
		// }

		_projectServices = new ProjectServices ();

		int groupid = DataBaseParametersCtrl.Ctrl._groupLoaded.id;
		int counterProjects = _projectServices.GetProjectsCounter(groupid);

		_projects = new Project[counterProjects];

		var projects = _projectServices.GetProjects(groupid);

		int counter = 0;
		foreach (var item in projects) {
			var SetName = Instantiate (prefab_project, parent_project.transform);
			SetName.name = counter.ToString();
			_projects [counter] = item;
			counter++;
			SetName.GetComponentInChildren<Text> ().text = item.name;
			SetName.GetComponentInChildren<Button> ().onClick.AddListener (delegate{GetCoursePressed (SetName.name, item.name);});

			Debug.Log ("name" + item.name);
		}
	}

	void GetCoursePressed(string positionInToArray, string nameCourse) {

		// int value = int.Parse (positionInToArray);
		// Debug.Log ("position " + positionInToArray);

		// DataBaseParametersCtrl.Ctrl._projectLoaded = _projects[value];

		DOTween.Play("bg_transition");
		goToScene ();
	}

	void Update(){

		
	}

	public IEnumerator goToScene(){

		yield return new WaitForSeconds(1);
		SceneManager.LoadScene ("Challenge_HUD", LoadSceneMode.Additive);
	}

}
