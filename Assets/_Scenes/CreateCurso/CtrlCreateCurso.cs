using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CtrlCreateCurso : MonoBehaviour {


	#region Variables
	public InputField NameCourse;
	private string tmp;
	private CourseServices _courseServices;
	#endregion

	void Start () {

		_courseServices = new CourseServices ();
		
	}

	void Update(){
		
//		Debug.Log ("Mensaje" + tmp);
	}

	public void Close(){
		StartCoroutine (Back());
	}

	public void SaveData(){
		tmp = NameCourse.text;
		StartCoroutine (SaveNameCourse ());
	}
		
	IEnumerator Back(){
		SceneManager.LoadScene ("SelectGame", LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync ("SelectActivity");
	}

	IEnumerator SaveNameCourse(){
		Main_Ctrl.instance.NameCourse = NameCourse.text;

		SceneManager.LoadScene ("CreateGroup", LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync ("SelectActivity");

//		var result = _courseServices.CreateCourse (NameCourse.text);
//
//		if (result.id != 0) {
//			DataBaseParametersCtrl.Ctrl._courseLoaded = result;
//			SceneManager.LoadScene ("CreateGroup", LoadSceneMode.Additive);
//			yield return null;
//			SceneManager.UnloadSceneAsync ("SelectActivity");
//		} else {
//			
//		}

	}

}
