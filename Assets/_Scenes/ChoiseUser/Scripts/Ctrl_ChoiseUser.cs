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
		
		yield return null;
		SceneManager.LoadScene ("Challenge_HUD");
	}
	IEnumerator Trainning(){
		
		yield return null;
		SceneManager.LoadScene ("Main_HUD");
	}
}
