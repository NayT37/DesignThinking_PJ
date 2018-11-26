using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Ctrl_RegisterUser : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void GoUser(){
		StartCoroutine (ResgisterUser ());
	}

	IEnumerator ResgisterUser(){
		SceneManager.LoadScene ("SelectGame", LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync ("Register_User");
	}
}
