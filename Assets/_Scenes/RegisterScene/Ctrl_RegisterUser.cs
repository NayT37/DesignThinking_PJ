using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;


public class Ctrl_RegisterUser : MonoBehaviour {

	#region  variables
	public InputField userName;
	public InputField passName;	
	private DOTweenAnimation animationGame;
	#endregion


	public void GoUser(){

		if(userName.GetComponent<InputField>().text.Equals("juan") && passName.GetComponent<InputField>().text.Equals("1234")){			
			StartCoroutine (ResgisterUser ());
		}else{
//			userName.GetComponent<InputField> ();
//			userName.placeholder.transform.localScale = new Vector3 (1.5f,1.5f,1);
			DOTween.Play ("7");
		}
	}

	IEnumerator ResgisterUser(){
		SceneManager.LoadScene ("SelectGame", LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync ("RegisterUser");
	}
}
