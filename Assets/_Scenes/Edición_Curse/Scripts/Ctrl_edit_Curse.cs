﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ctrl_edit_Curse : MonoBehaviour {

	public GameObject prefab_editCurse;
	public GameObject parent_Group;
	public GameObject NameObj;
	private Slider _slider_editCurse;
	private slider_EditCurse slider_handlerEditCurse;

	private float max;
	private float min;

	private GroupServices _GroupServices;

	void Start () {


		_slider_editCurse = GameObject.Find ("SliderEditCurse").GetComponent<Slider>();
		slider_handlerEditCurse = _slider_editCurse.GetComponent<slider_EditCurse> ();

		NameObj = GameObject.Find ("CreateNameGroup");

		_GroupServices = new GroupServices ();

		var groups = _GroupServices.GetGroups ();
		foreach (var item in groups) {
			var setName = Instantiate (prefab_editCurse, parent_Group.transform);
			NameObj.GetComponentInChildren<Text>().text = item.name;
			setName.GetComponentInChildren<Text> ().text = item.name;
			Debug.Log ("this " + item.name);
			setName.GetComponentInChildren<Text> ().text = item.studentsCounter.ToString();
			Debug.Log ("number " + item.studentsCounter.ToString());
		}
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

	IEnumerator GobackScene(){
		SceneManager.LoadScene ("LoadGame", LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync ("Edit_Curse");
	}
}
