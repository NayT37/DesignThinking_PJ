using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ctrl_edit_Curse : MonoBehaviour {




	void Start () {
		
	}


	void Update () {
		
	}

	public void editBtn(){
		StartCoroutine (GotoScene ());
	}

	public void backScene(){
		StartCoroutine (GobackScene ());
	}

	IEnumerator GotoScene(){
		SceneManager.LoadScene ("Edit_Group", LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync ("Edit_Curse");
	}

	IEnumerator GobackScene(){
		SceneManager.LoadScene ("LoadGame", LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync ("Edit_Curse");
	}
}
