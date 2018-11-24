using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CasesHUD_Ctrl : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    //Private Variables
    private int _actualMoment;
    private int _actualCase;
    private string _actualScn;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _actualMoment = 1;
        _actualCase = 1;
        _actualScn = "";
        StartCoroutine(ChangeScene("C" + _actualCase + "_M" + _actualMoment, _actualScn));
    }
    public void CaseBtnClick(int caseValue)
    {
        if (_actualCase != caseValue)
        {

            _actualMoment = 1;
            switch (caseValue)
            {
                case 1:
                    print(caseValue + " was clicked. At " + _actualMoment + " moment.");
                    break;
                case 2:
                    print(caseValue + " was clicked. At " + _actualMoment + " moment.");
                    break;
                case 3:
                    print(caseValue + " was clicked. At " + _actualMoment + " moment.");
                    break;
            }
            _actualCase = caseValue;
            StartCoroutine(ChangeScene("C" + _actualCase + "_M" + _actualMoment, _actualScn));
        }
    }
    public void MomentBtnClick(int momentValue)
    {
        if (_actualMoment != momentValue)
        {
            switch (momentValue)
            {
                case 1:
                    //  print(_actualCase + "Case's " + momentValue + " moment was clicked.");
                    StartCoroutine(ChangeScene("C" + _actualCase + "_M" + momentValue, _actualScn));
                    break;
                case 2:
                    //  print(_actualCase + "Case's " + momentValue + " moment was clicked.");
                    StartCoroutine(ChangeScene("C" + _actualCase + "_M" + momentValue, _actualScn));
                    break;
                case 3:
                    //  print(_actualCase + "Case's " + momentValue + " moment was clicked.");
                    StartCoroutine(ChangeScene("C" + _actualCase + "_M" + momentValue, _actualScn));
                    break;
                case 4:
                    //  print(_actualCase + "Case's " + momentValue + " moment was clicked.");
                    StartCoroutine(ChangeScene("C" + _actualCase + "_M" + momentValue, _actualScn));
                    break;
                case 5:
                    //  print(_actualCase + "Case's " + momentValue + " moment was clicked.");
                    StartCoroutine(ChangeScene("C" + _actualCase + "_M" + momentValue, _actualScn));
                    break;
            }
            _actualMoment = momentValue;
        }
    }
    #endregion


    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    private IEnumerator ChangeScene(string sceneToLoad, string sceneToUnload)
    {
        if (sceneToUnload != "")
        {
            SceneManager.UnloadSceneAsync(sceneToUnload);
        }

        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
        yield return null;
        _actualScn = sceneToLoad;

    }
    #endregion
}
