using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
	private Group[] _arrayGroup;

	void Start () {

		_arrayGroup = new Group[10];
		_slider_editCurse = GameObject.Find ("SliderEditCurse").GetComponent<Slider>();
		slider_handlerEditCurse = _slider_editCurse.GetComponent<slider_EditCurse> ();
		_GroupServices = new GroupServices ();
		texCourse = GameObject.Find ("TitleCurse").GetComponent<Text> ();
		texCourse.text = Main_Ctrl.instance.NameCourse;

		var courseId = DataBaseParametersCtrl.Ctrl._courseLoaded.id;

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
			setName.GetComponentInChildren<Button>().onClick.AddListener(delegate{getPushButtonGroups (setName.name, item.name);});
		}
	}


	void getPushButtonGroups(string positionInArrayGroup, string nameGroup){
		int value = int.Parse (positionInArrayGroup);


		Main_Ctrl.instance.NameCourse = nameGroup;

		DataBaseParametersCtrl.Ctrl._groupLoaded = _arrayGroup[value];
		Debug.Log ("position " + positionInArrayGroup);

		goNextEscene ();

	}

	void Update () {
		
		if(slider_handlerEditCurse.slider_value < _slider_editCurse.value){
			if (max < _slider_editCurse.value) {
				parent_Group.transform.position += new Vector3 (0.0f, _slider_editCurse.value, 0.0f);	
			} else if(max > _slider_editCurse.value){
				parent_Group.transform.position += new Vector3 (0.0f, -_slider_editCurse.value , 0.0f);
			}
			max = _slider_editCurse.value;
		}else if(slider_handlerEditCurse.slider_value > _slider_editCurse.value){
			if (min > _slider_editCurse.value) {
				parent_Group.transform.position += new Vector3 (0.0f, -_slider_editCurse.value , 0.0f);	
			} else if(min < _slider_editCurse.value){
				parent_Group.transform.position += new Vector3 (0.0f, _slider_editCurse.value, 0.0f);
			}
			min = _slider_editCurse.value;

		}

	}

	public void backScene(){
		StartCoroutine (GobackScene ());
	}

	void goNextEscene(){
		StartCoroutine (GotoScene ());
	}

	IEnumerator GobackScene(){
		SceneManager.LoadScene ("LoadGame", LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync ("Edit_Curse");
	}

	IEnumerator GotoScene(){
		SceneManager.LoadScene ("Edit_Group", LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync ("Edit_Curse");
	}
}
