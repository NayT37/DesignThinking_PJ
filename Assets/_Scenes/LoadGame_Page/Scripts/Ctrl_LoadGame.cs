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

	private IEnumerable<Course> courses;


	// Use this for initialization
	void Start () {
		
		var goCourses = gameObject.AddComponent<CourseServices>();
        _courseServices = goCourses.GetComponent<CourseServices>();

		courses = _courseServices.GetCourses();
        int countercourses = _courseServices.GetCoursesCount();
		Debug.Log(countercourses);
		_courses = new Course[countercourses];
		int counter = 0;

		foreach (var item in courses) {
			Debug.Log(item.name);
			var SetName = Instantiate (prefab_Curse, parent_group.transform);
			SetName.name = counter.ToString();
			_courses [counter] = item;
			counter++;
			SetName.GetComponentInChildren<Text> ().text = item.name;
			SetName.GetComponentInChildren<Button> ().onClick.AddListener (delegate{GetCoursePressed (SetName.name, item.name);});
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

		
	}

	public void goToScene(){
		StartCoroutine (GoScene ());
	}

	public void backToScene(){
		StartCoroutine (BackOne ());
	}

	IEnumerator GoScene(){
		DOTween.Play("bg_transition");
		yield return new WaitForSeconds(1.0f);
		SceneManager.LoadScene ("Edit_Curse");
	}

	IEnumerator BackOne(){
		DOTween.Play("bg_transition");
		yield return new WaitForSeconds(1.0f);
		SceneManager.LoadScene ("SelectGame");
	}


	void CreatePrefabs(){
		
	}
}
