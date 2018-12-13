using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ctrl_Edit_Group : MonoBehaviour {


	void Start () {
		
	}
	

	void Update () {
		
	}

	public void GoScene(){
		StartCoroutine (GotoScene ());
	}

	public void backScene(){
		StartCoroutine (GobackScene ());
	}

	IEnumerator GotoScene(){
		SceneManager.LoadScene ("SelectGame", LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync ("Edit_Group");
	}

	IEnumerator GobackScene(){
		SceneManager.LoadScene ("Edit_Curse", LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync ("Edit_Group");
	}

}
