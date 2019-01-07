using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class Ctrl_ChoiseUser : MonoBehaviour {

	private TrainingServices _trainingServices;

	// Use this for initialization
	void Start () {

		_trainingServices = new TrainingServices();
		
	}
	
	public void GoChallenge(){

		DOTween.Play("bg_transition");
		StartCoroutine (Challenge ());
	}
	public void GoTrainning(){
		DataBaseParametersCtrl.Ctrl._trainingloaded = _trainingServices.GetTraining(DataBaseParametersCtrl.Ctrl._groupLoaded.id);
		DOTween.Play("bg_transition");
		StartCoroutine (Trainning());
	}

	public void backToScene(){
		StartCoroutine (BackOne ());
	}

	IEnumerator Challenge(){
		
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene ("CreateViewPJ");
	}
	IEnumerator Trainning(){
		
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene ("Main_HUD");
	}

	IEnumerator BackOne(){
		DOTween.Play("bg_transition");
		yield return new WaitForSeconds(1.0f);
		SceneManager.LoadScene ("RegisterUser");
	}
}
