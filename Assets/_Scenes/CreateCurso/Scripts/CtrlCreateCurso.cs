using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CtrlCreateCurso : MonoBehaviour {


	#region Variables
	public InputField NameCourse;
	private string tmp;
	private CourseServices _courseServices;
	#endregion

	void Start () {

		_courseServices = new CourseServices ();
		Debug.Log ("empezo");
	}

	void Update(){
	}

	public void Close(){
		StartCoroutine (Back());
	}

	public void SaveData(){
		tmp = NameCourse.text;
		StartCoroutine (SaveNameCourse ());
	}
		
	IEnumerator Back(){
		DOTween.Play("bg_transition");
		yield return new WaitForSeconds(1.0f);
		SceneManager.LoadScene ("SelectGame", LoadSceneMode.Additive);
		SceneManager.UnloadSceneAsync ("CreateCurso");
	}

	IEnumerator SaveNameCourse(){
		Main_Ctrl.instance.NameCourse = NameCourse.text;
		var result = _courseServices.CreateCourse (NameCourse.text);
		if (result.id != 0) {
			DataBaseParametersCtrl.Ctrl._courseLoaded = result;
			Debug.Log ("result " + result);
			DOTween.Play("bg_transition");
			yield return new WaitForSeconds(1.0f);
			SceneManager.LoadScene ("CreateGroup", LoadSceneMode.Additive);
			SceneManager.UnloadSceneAsync ("CreateCurso");
		} else {
			
		}

	}

}
