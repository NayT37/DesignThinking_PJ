using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class Ctrl_ChoiseUser : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void GoChallenge(){
		DOTween.Play("bg_transition");
		StartCoroutine (Challenge ());
	}
	public void GoTrainning(){
		DOTween.Play("bg_transition");
		StartCoroutine (Trainning());
	}

	IEnumerator Challenge(){
		
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene ("CreateViewPJ");
	}
	IEnumerator Trainning(){
		
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene ("Main_HUD");
	}
}
