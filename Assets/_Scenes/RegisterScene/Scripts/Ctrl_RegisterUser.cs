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
	
	private TeacherServices _teacherServices;
	#endregion

	public void Start(){
		_teacherServices = new TeacherServices();
	}
	public void GoUser(){

		string name =userName.text;
		string password = passName.text;

		if(!name.Equals("") && !passName.Equals("")){
			var teacher = _teacherServices.GetTeacherNamed(name, password);	

			if (teacher.identityCard.Equals("null"))
			{
				DOTween.Play ("7");
			} else {
				
				DOTween.Play("bg_transition");
				userName.text = "";
				passName.text = "";		
				StartCoroutine (ResgisterUser ());
			}
		}else{
//			userName.GetComponent<InputField> ();
//			userName.placeholder.transform.localScale = new Vector3 (1.5f,1.5f,1);
			DOTween.Play ("7");
		}
	}

	IEnumerator ResgisterUser(){
		
		yield return new WaitForSeconds(1.0f);	
		SceneManager.LoadScene ("SelectGame", LoadSceneMode.Additive);
		SceneManager.UnloadSceneAsync ("RegisterUser");
	}
}
