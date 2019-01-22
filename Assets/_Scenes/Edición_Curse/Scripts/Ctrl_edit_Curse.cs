using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class Ctrl_edit_Curse : MonoBehaviour {

	public GameObject prefab_editCurse;
	public GameObject parent_Group;
	private Slider _slider_editCurse;
	private slider_EditCurse slider_handlerEditCurse;
	private Text[] Textos;

	private float max;
	private float min;
	private Text texCourse;

	private GroupServices _GroupServices;

	private TrainingServices _trainingServices;
	private Group[] _arrayGroup;

	void Start () {
		var goGroup = gameObject.AddComponent<GroupServices>();
		_GroupServices = goGroup.GetComponent<GroupServices>();
		var goTraining = gameObject.AddComponent<TrainingServices>();
		_trainingServices = goTraining.GetComponent<TrainingServices>();
		
		texCourse = GameObject.Find ("titleCourse").GetComponent<Text> ();
		texCourse.text = "CURSO: " + Main_Ctrl.instance.NameCourse;

		var courseId = DataBaseParametersCtrl.Ctrl._courseLoaded.id;

		int countergroups = _GroupServices.GetGroupsCounter(courseId);

		_arrayGroup = new Group[countergroups];

		var counter = 0;
		var groups = _GroupServices.GetGroups (courseId);
		foreach (var item in groups) {
			var setName = Instantiate (prefab_editCurse, parent_Group.transform);
			setName.name = counter.ToString ();
			_arrayGroup [counter] = item;
			counter++;
			Textos = setName.GetComponentsInChildren<Text> ();
			Textos [0].text = item.name;
			Textos [1].text = item.studentsCounter.ToString ();
			Button[] _btns = new Button[2];
			_btns = setName.GetComponentsInChildren<Button>();
			
			Debug.Log(_btns[0].name);
			Debug.Log(_btns[1].name);
			if (_btns[0].name.Equals("group_Open"))
			{
				_btns[0].onClick.AddListener(delegate{openToGroup (setName.name);});
				_btns[1].onClick.AddListener(delegate{getPushButtonGroups (setName.name, item.name);});
			} else{
				_btns[1].onClick.AddListener(delegate{openToGroup (setName.name);});
				_btns[0].onClick.AddListener(delegate{getPushButtonGroups (setName.name, item.name);});
			}
			//setName.GetComponentInChildren<Button>().onClick.AddListener(delegate{getPushButtonGroups (setName.name, item.name);});
		}
	}


	void getPushButtonGroups(string positionInArrayGroup, string nameGroup){
		int value = int.Parse (positionInArrayGroup);

		Main_Ctrl.instance.NameCourse = nameGroup;

		Debug.Log (_arrayGroup[value]);
		DataBaseParametersCtrl.Ctrl._groupLoaded = _arrayGroup[value];
		Debug.Log (DataBaseParametersCtrl.Ctrl._groupLoaded); 

		StartCoroutine(GotoScene ());

	}

	void openToGroup(string positionToArrayGroup){
		int value = int.Parse (positionToArrayGroup);

		Debug.Log (_arrayGroup[value]);
		DataBaseParametersCtrl.Ctrl._groupLoaded = _arrayGroup[value];
		DataBaseParametersCtrl.Ctrl._trainingloaded = _trainingServices.GetTraining(_arrayGroup[value].id);

		StartCoroutine(GoToGroup ());
	}

	public void addGroup(){
		StartCoroutine (waitAddGroup ());
	}

	void Update () {
		

	}

	IEnumerator waitAddGroup(){
		DOTween.Play("bg_transition");
		yield return new WaitForSeconds(1.0f);
		SceneManager.LoadScene ("CreateGroup");
	}

	public void backScene(){
		StartCoroutine (GobackScene ());
	}

	IEnumerator GobackScene(){
		DOTween.Play("bg_transition");
		yield return new WaitForSeconds(1.0f);
		SceneManager.LoadScene ("LoadGame");
	}

	IEnumerator GotoScene(){
		DOTween.Play("bg_transition");
		yield return new WaitForSeconds(1.0f);
		SceneManager.LoadScene ("Edit_Group");
	}

	IEnumerator GoToGroup(){
		DOTween.Play("bg_transition");
		yield return new WaitForSeconds(1.0f);
		SceneManager.LoadScene ("ChoiseUser");
	}
}
