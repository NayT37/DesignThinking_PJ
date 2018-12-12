using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ctrl_LoadGame : MonoBehaviour {


	public Text NameCourse;

	// Use this for initialization
	void Start () {
		
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
