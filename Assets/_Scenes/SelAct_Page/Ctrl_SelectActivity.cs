using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ctrl_SelectActivity : MonoBehaviour {


	#region Variables
	public InputField NameCourse;
	private string tmp;
	#endregion

	void Start () {
		
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
		SceneManager.UnloadSceneAsync ("CreateCurso");
	}

	IEnumerator SaveNameCourse(){
		Main_Ctrl.instance.NameCourse = NameCourse.text;
		SceneManager.LoadScene ("CreateGroup", LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync ("CreateCurso");
	}

}
