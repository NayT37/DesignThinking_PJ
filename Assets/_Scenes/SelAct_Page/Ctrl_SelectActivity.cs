using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ctrl_SelectActivity : MonoBehaviour {


	#region Variables
	public InputField NameCourse;
	private string tmp;
	#endregion

	void Start () {
		
	}

	void Update(){
		
//		Debug.Log ("Mensaje" + tmp);
	}

	public void Close(){
		StartCoroutine (Back());
	}

	public void SaveData(){
		Debug.Log (NameCourse.text);
		tmp = NameCourse.text;
		StartCoroutine (SaveNameCourse ());
	}
		
	IEnumerator Back(){
		SceneManager.LoadScene ("SelectGame", LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync ("SelectActivity");
	}

	IEnumerator SaveNameCourse(){
		SceneManager.LoadScene ("CreateGroup", LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync ("SelectGame");
		tmp = NameCourse.text;
	}

}
