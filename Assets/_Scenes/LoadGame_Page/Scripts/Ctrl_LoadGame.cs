﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ctrl_LoadGame : MonoBehaviour {


	public Text NameCourse;
	public GameObject prefab_Curse;
	public GameObject parent_group;
	private Slider _sliderCurses;
	private SliderHandler slider_Handler;

	private float max;
	private float min; 


	// Use this for initialization
	void Start () {
		_sliderCurses = GameObject.Find ("SliderGame").GetComponent<Slider>();
		slider_Handler = _sliderCurses.GetComponent<SliderHandler> ();
		Instantiate (prefab_Curse, parent_group.transform);
		Instantiate (prefab_Curse, parent_group.transform);
		Instantiate (prefab_Curse, parent_group.transform);
		Instantiate (prefab_Curse, parent_group.transform);
		Instantiate (prefab_Curse, parent_group.transform);
		Instantiate (prefab_Curse, parent_group.transform);
		Instantiate (prefab_Curse, parent_group.transform);
		Instantiate (prefab_Curse, parent_group.transform);
		Instantiate (prefab_Curse, parent_group.transform);
		Instantiate (prefab_Curse, parent_group.transform);
		Instantiate (prefab_Curse, parent_group.transform);
		Instantiate (prefab_Curse, parent_group.transform);
	}

	void Update(){

		if(slider_Handler.sliderValue < _sliderCurses.value){
			if (max < _sliderCurses.value) {
				parent_group.transform.position += new Vector3 (-_sliderCurses.value, 0.0f, 0.0f);	
			} else if(max > _sliderCurses.value){
				parent_group.transform.position += new Vector3 (_sliderCurses.value, 0.0f, 0.0f);
			}
			max = _sliderCurses.value;
		}else if(slider_Handler.sliderValue > _sliderCurses.value){
			if (min > _sliderCurses.value) {
				parent_group.transform.position += new Vector3 (_sliderCurses.value, 0.0f, 0.0f);	
			} else if(min < _sliderCurses.value){
				parent_group.transform.position += new Vector3 (-_sliderCurses.value, 0.0f, 0.0f);
			}
			min = _sliderCurses.value;

		}
	}

	public void goToScene(){
		StartCoroutine (GoScene ());
	}

	IEnumerator GoScene(){
//		Main_Ctrl.instance.NameCourse = NameCourse.text.ToString();
		SceneManager.LoadScene ("Edit_Curse", LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync ("LoadGame");
	}
}
