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

	private bool _doDataToLoad;

	public GameObject _notData;

	public Transform _TextTransform;

	// Use this for initialization
	void Start () {

		_doDataToLoad = false;
		_btnCreateTest.onClick.AddListener(delegate{ eventClick(_btnCreateTest.name);});
		_btnLoadData.onClick.AddListener(delegate{ eventClick(_btnLoadData.name);});

	}

    private void eventClick(string name)
    {
		bool isChange = false;
		Ctrl_Moment5.Ctrl.getAnswersValue();
		for (int i = 0; i < Ctrl_Moment5.Ctrl._answersValue.Length; i++)
		{
			if (Ctrl_Moment5.Ctrl._answersValue[i] != 0)
			{
				_doDataToLoad = true;
				break;
			}
		}
		
		string newSceneToLoad = "";

		if (name.Equals("NewTestBtn")){
			newSceneToLoad = "M_5B";
			isChange = true;
		}else{ 
			if (_doDataToLoad)
			{
				newSceneToLoad = "M_5C";
				isChange = true;
			} else{
				GameObject obj = Instantiate(_notData, _TextTransform);
				StartCoroutine(DeletePrefab(obj));
				Debug.Log("No hay resultados para cargar");
			}	
		}
		
		if (isChange)
		{
        	DOTween.Play(name);
			DOTween.Play("bg_transition");
			StartCoroutine(ChangeScene(newSceneToLoad));
		}
		

    }

    private IEnumerator DeletePrefab(GameObject obj)
    {
        yield return new WaitForSeconds(4.0f);	
        DestroyImmediate(obj);
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
