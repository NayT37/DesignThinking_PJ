using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Ctrl_ChoiseUser : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void GoChallenge(){
		StartCoroutine (Challenge ());
	}
	public void GoTrainning(){
		StartCoroutine (Trainning());
	}

	IEnumerator Challenge(){
		SceneManager.LoadScene ("Challenge_HUD", LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync ("ChoiseUser");
	}
	IEnumerator Trainning(){
		SceneManager.LoadScene ("Main_HUD", LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync ("ChoiseUser");
	}
}
