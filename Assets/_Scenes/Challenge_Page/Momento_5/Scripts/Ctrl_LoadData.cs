using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class Ctrl_LoadData : MonoBehaviour {

	public Button _btnClose;

	public Image[] _arraySliders;

	public Text[] _arrayTextsPercentage;

	private float averageFirstValue;

	private float averageSecondValue;

	private float averageThirdValue;

	// Use this for initialization
	void Start () {

		averageFirstValue = 0;

		averageSecondValue = 0;

		averageThirdValue = 0;
		
		_btnClose.onClick.AddListener(delegate{ eventClick(_btnClose.name);});

		int result = Ctrl_Moment5.Ctrl.getAnswersValue();

		int[] arrayv = Ctrl_Moment5.Ctrl._answersValue;


		for (int i = 1; i <= 10; i++)
		{
			Debug.Log(arrayv[i-1]);
			if (i<4)
			{
				averageFirstValue += arrayv[i-1];
			} else if (i<8){
				averageSecondValue += arrayv[i-1];
			} else{
				averageThirdValue += arrayv[i-1];
			}
		} 

		float av1 = averageFirstValue/(15*result);
		float av2 = averageSecondValue/(20*result);
		float av3 = averageThirdValue/(15*result);

		_arraySliders[0].fillAmount = av1;
		_arrayTextsPercentage[0].text = av1*100 + "%";
		_arraySliders[1].fillAmount = av2;
		_arrayTextsPercentage[1].text = av2*100 + "%";
		_arraySliders[2].fillAmount = av3;
		_arrayTextsPercentage[2].text = av3*100 + "%";


	}

    private void eventClick(string name)
    {
		string newSceneToLoad = "M_5A";
		DOTween.Play("bg_transition");
		StartCoroutine(ChangeScene(newSceneToLoad));

    }

    // Update is called once per frame
    void Update () {
		
	}

	#region COROUTINES
    private IEnumerator ChangeScene(string newSceneToLoad)
    {
		
        yield return new WaitForSeconds(1.0f);	
		SceneManager.LoadScene(newSceneToLoad, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("M_5C");
    }
    #endregion
}
