using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ctrl_CreateTest : MonoBehaviour {

	public Button _btnCreateTest;
	public Button _btnLoadData;

	// Use this for initialization
	void Start () {
		
		_btnCreateTest.onClick.AddListener(delegate{ eventClick(_btnCreateTest.name);});
		_btnLoadData.onClick.AddListener(delegate{ eventClick(_btnLoadData.name);});

	}

    private void eventClick(string name)
    {
		string newSceneToLoad = "";
        DOTween.Play(name);
		DOTween.Play("bg_transition");

		if (name.Equals("NewTestBtn"))
			newSceneToLoad = "M_5B";
		else 
			newSceneToLoad = "M_5C";
		
		StartCoroutine(ChangeScene(newSceneToLoad));

    }

    // Update is called once per frame
    void Update () {
		
	}

	#region COROUTINES
    private IEnumerator ChangeScene(string newSceneToLoad)
    {
		
        yield return new WaitForSeconds(2.0f);	
		SceneManager.LoadScene(newSceneToLoad, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("M_5A");
    }
    #endregion
}
