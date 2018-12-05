using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Ctrl_RegisterUser : MonoBehaviour {

	#region  variables
	public InputField userName;
	public InputField passName;

	private string aceptLogin;	
	#endregion
	void Start () {
		// userName.GetComponent<InputField>();
	}


	public void GoUser(){
		// Debug.Log(userName.GetComponent<InputField>().text);

		if(userName.GetComponent<InputField>().text.Equals("juan") && passName.GetComponent<InputField>().text.Equals("1234")){
			// Debug.Log(userName.GetComponent<InputField>().text);
			// Debug.Log(passName.GetComponent<InputField>().text);			
			StartCoroutine (ResgisterUser ());
		}else{
			Debug.Log("No funciona!");
		}
		// StartCoroutine (ResgisterUser ());
	}

	IEnumerator ResgisterUser(){
		SceneManager.LoadScene ("SelectGame", LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync ("RegisterUser");
	}
}
