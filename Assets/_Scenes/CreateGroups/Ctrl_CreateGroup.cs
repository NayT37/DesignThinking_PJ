using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ctrl_CreateGroup : MonoBehaviour {


	public Text TitleCurse;

	void Start () {
//		Main_Ctrl.instance.NameCourse;
		TitleCurse.text = Main_Ctrl.instance.NameCourse;
		Debug.Log (Main_Ctrl.instance.NameCourse);
	}

	public void GoToSelect(){
		StartCoroutine (SelectUser ());
	}

	IEnumerator SelectUser(){
		SceneManager.LoadScene ("ChoiseUser", LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync ("CreateGroup");
	}

}
