using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ctrl_SelectCategory : MonoBehaviour {

	public Button _btnProduct;
	public Button _btnService;

	public GameObject _serviceFolder;

	public GameObject _productFolder;

	// Use this for initialization
	void Start () {
		
		_btnProduct.onClick.AddListener(delegate{ eventClick(_btnProduct.name);});
		_btnService.onClick.AddListener(delegate{ eventClick(_btnService.name);});

	}

    private void eventClick(string name)
    {
        DOTween.Play(name);
		DOTween.Play("bg_transition");
		StartCoroutine(ChangeScene());

    }

    // Update is called once per frame
    void Update () {
		
	}

	#region COROUTINES
    private IEnumerator ChangeScene()
    {
		
        yield return new WaitForSeconds(2.0f);
		SceneManager.LoadScene("M_5A", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("M_5");
    }
    #endregion
}
