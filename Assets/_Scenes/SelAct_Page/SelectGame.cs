using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class SelectGameBehaviour : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    //Private Variables
    private Image newGameBtn, loadGameBtn;
    private DOTweenAnimation newGameCtrl;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        DOTween.Init();
        // EXAMPLE B: initialize with custom settings, and set capacities immediately
        DOTween.Init(true, true, LogBehaviour.Verbose).SetCapacity(200, 10);

        newGameBtn = GameObject.Find("NewGameBtn").GetComponent<Image>();
        loadGameBtn = GameObject.Find("LoadGameBtn").GetComponent<Image>();
        newGameBtn.raycastTarget = false;
        loadGameBtn.raycastTarget = false;

        newGameCtrl = GameObject.Find("BtnNewGame").GetComponent<DOTweenAnimation>();

        StartCoroutine(WaitAnimationTime());
    }

    public void NewGameBtnBhvr() { Main_Ctrl.instance.GoToScene("SelectActivity"); }

    public void LoadGameBtnBhvr() { Main_Ctrl.instance.GoToScene("LoadGame"); }
    #endregion


    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    private IEnumerator WaitAnimationTime()
    {
        yield return new WaitForSeconds(newGameCtrl.duration * 3);
        newGameBtn.raycastTarget = true;
        loadGameBtn.raycastTarget = true;
    }
    #endregion
}