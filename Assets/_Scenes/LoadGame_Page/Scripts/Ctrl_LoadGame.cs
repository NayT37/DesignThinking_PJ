using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ctrl_LoadGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void back(){
		StartCoroutine (GoBack ());
	}

	IEnumerator GoBack(){
		SceneManager.LoadScene ("CreateGroup", LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync ("LoadGame");
	}
}
