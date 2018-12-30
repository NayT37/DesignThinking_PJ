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

	private Course[] _courses;


	// Use this for initialization
	void Start () {

		_courses = new Course[10];
		_sliderCurses = GameObject.Find ("SliderGame").GetComponent<Slider>();
		slider_Handler = _sliderCurses.GetComponent<SliderHandler> ();
		_courseServices = new CourseServices ();

		var courses = _courseServices.GetCourses();

		int counter = 0;
		foreach (var item in courses) {
			var SetName = Instantiate (prefab_Curse, parent_group.transform);
			SetName.name = counter.ToString();
			_courses [counter] = item;
			counter++;
			SetName.GetComponentInChildren<Text> ().text = item.name;
			SetName.GetComponentInChildren<Button> ().onClick.AddListener (delegate{GetCoursePressed (SetName.name, item.name);});




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

	void GetCoursePressed(string positionInToArray, string nameCourse) {

		Main_Ctrl.instance.NameCourse = nameCourse;

		int value = int.Parse (positionInToArray);
		Debug.Log ("position " + positionInToArray);

		DataBaseParametersCtrl.Ctrl._courseLoaded = _courses[value];


		goToScene ();
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

	public void backToScene(){
		StartCoroutine (BackOne ());
	}

	IEnumerator GoScene(){
		SceneManager.LoadScene ("Edit_Curse", LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync ("LoadGame");
	}

	IEnumerator BackOne(){
		SceneManager.LoadScene ("SelectGame", LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync ("LoadGame");
	}
}
