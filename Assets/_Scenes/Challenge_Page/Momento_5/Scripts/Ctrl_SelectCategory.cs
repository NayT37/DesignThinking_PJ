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
		string nameCategory = "";
		if (name.Equals("ServiceBtn"))
			nameCategory = "Servicio";
		else 
			nameCategory = "Producto";
		
		Ctrl_Moment5.Ctrl._evaluationCategory = nameCategory;
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
		var result = Ctrl_Moment5.Ctrl.createEvaluation();
		DataBaseParametersCtrl.Ctrl._evaluationLoaded = result;
		SceneManager.LoadScene("M_5A", LoadSceneMode.Additive);
		DOTween.Play("bg_transition_end");
        SceneManager.UnloadSceneAsync("M_5");
		
    }
    #endregion
}
