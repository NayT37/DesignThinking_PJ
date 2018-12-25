using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class Ctrl_LoadGame : MonoBehaviour {


	public GameObject prefab_Curse;
	public GameObject parent_group;
	private Slider _sliderCurses;
	private SliderHandler slider_Handler;

	private float max;
	private float min; 

	private CourseServices _courseServices;
	private Text _nameConstant;


	// Use this for initialization
	void Start () {
		_sliderCurses = GameObject.Find ("SliderGame").GetComponent<Slider>();
		slider_Handler = _sliderCurses.GetComponent<SliderHandler> ();
		_courseServices = new CourseServices ();

		var courses = _courseServices.GetCourses();
		foreach (var item in courses) {
			var SetName = Instantiate (prefab_Curse, parent_group.transform);
			SetName.GetComponentInChildren<Text> ().text = item.name;

//			DOTweenAnimation[] animations = g.GetComponentsInChildren<DOTweenAnimation>();
//			for(int i=0;i< animations.Length; i++)
//			{
//				if (animations[i].animationType.Equals("Text"))
//				{
//					Debug.Log("animations " + animations[i].animationType);
//				}
//			}
			Debug.Log ("name" + item.name);
		}
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
//		Main_Ctrl.instance.NameCourse = variableConstant;
		yield return null;
		SceneManager.LoadScene ("Edit_Curse", LoadSceneMode.Additive);
		SceneManager.UnloadSceneAsync ("LoadGame");
	}
}
