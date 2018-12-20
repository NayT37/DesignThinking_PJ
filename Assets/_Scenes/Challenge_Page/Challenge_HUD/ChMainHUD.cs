using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine;
using DG.Tweening;
using System;

public class ChMainHUD : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    //Private Variables
    private int _actualMoment;
    private string _actualScn;
    [SerializeField]
    private int _limitCtrl;
    private GameObject _loadObj;
    private bool _isHide;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        XRSettings.enabled = false;
        _isHide = false;
        _loadObj = GameObject.Find("Loaging_Obj");
        _actualMoment = 0;
        _limitCtrl = 0;
        _actualScn = "";
        StartCoroutine(ChangeScene("M_" + _actualMoment, _actualScn));
    }

    public void MomentBtnClick(int clickMomentValue)
    {
        XRSettings.enabled = false;
        if (_actualMoment != clickMomentValue && clickMomentValue <= _limitCtrl)
        {
            /*            switch (momentValue)
            {
                case 1:
                    VuforiaControl.instance.ResetRA();
                    //  print(_actualCase + "Case's " + momentValue + " moment was clicked.");
                    StartCoroutine(ChangeScene("M" + _actualMoment, _actualScn));

                    //Sprites for moments
                    Emp_Btn.sprite = EmpatizarSelected;
                    Def_Btn.sprite = DefinirNull;
                    Ide_Btn.sprite = IdearNull;
                    Pro_Btn.sprite = PrototiparPointer;
                    Eva_Btn.sprite = EvaluarNull;

                    //				DOTween.Play ("3");
                    //Texto que indica el nombre de la fase
                    Text_Changed.text = "EMPATIZAR";
                    try { Destroy(GameObject.Find("EvaluateHolder")); }
                    catch (Exception e)
                    {
                        Debug.Log("There is no Evaluate Holder");
                    }
                    break;
                case 2:
                    VuforiaControl.instance.ResetRA();
                    //  print(_actualCase + "Case's " + momentValue + " moment was clicked.");
                    StartCoroutine(ChangeScene("C" + _actualCase + "_M" + momentValue, _actualScn));
                    //Sprites for moments
                    Emp_Btn.sprite = EmpatizarPointer;
                    Def_Btn.sprite = DefinirSelected;
                    Ide_Btn.sprite = IdearNull;
                    Pro_Btn.sprite = PrototiparPointer;
                    Eva_Btn.sprite = EvaluarNull;

                    //				DOTween.Play ("3");
                    //Texto que indica el nombre de la fase
                    Text_Changed.text = "DEFINIR";
                    try { Destroy(GameObject.Find("EvaluateHolder")); }
                    catch (Exception e)
                    {
                        Debug.Log("There is no Evaluate Holder");
                    }
                    break;
                case 3:
                    VuforiaControl.instance.ResetRA();
                    //  print(_actualCase + "Case's " + momentValue + " moment was clicked.");
                    StartCoroutine(ChangeScene("C" + _actualCase + "_M" + momentValue, _actualScn));
                    //Sprites for moments
                    Emp_Btn.sprite = EmpatizarPointer;
                    Def_Btn.sprite = DefinirNull;
                    Ide_Btn.sprite = IdearSelected;
                    Pro_Btn.sprite = PrototiparPointer;
                    Eva_Btn.sprite = EvaluarNull;

                    //				DOTween.Play ("3");
                    //Texto que indica el nombre de la fase
                    Text_Changed.text = "IDEAR";
                    try { Destroy(GameObject.Find("EvaluateHolder")); }
                    catch (Exception e)
                    {
                        Debug.Log("There is no Evaluate Holder");
                    }
                    break;
                case 4:
                    VuforiaControl.instance.ResetRA();
                    //  print(_actualCase + "Case's " + momentValue + " moment was clicked.");
                    StartCoroutine(ChangeScene("C" + _actualCase + "_M" + momentValue, _actualScn));

                    //Sprites for moments
                    Emp_Btn.sprite = EmpatizarPointer;
                    Def_Btn.sprite = DefinirNull;
                    Ide_Btn.sprite = IdearNull;
                    Pro_Btn.sprite = PrototiparSelected;
                    Eva_Btn.sprite = EvaluarNull;


                    //				DOTween.Play ("3");
                    //Texto que indica el nombre de la fase
                    Text_Changed.text = "PROTOTIPAR";
                    try { Destroy(GameObject.Find("EvaluateHolder")); }
                    catch (Exception e)
                    {
                        Debug.Log("There is no Evaluate Holder");
                    }
                    break;
                case 5:
                    //  print(_actualCase + "Case's " + momentValue + " moment was clicked.");
                    StartCoroutine(ChangeScene("C" + _actualCase + "_M" + momentValue, _actualScn));

                    //Sprites for moments
                    Emp_Btn.sprite = EmpatizarPointer;
                    Def_Btn.sprite = DefinirNull;
                    Ide_Btn.sprite = IdearNull;
                    Pro_Btn.sprite = PrototiparPointer;
                    Eva_Btn.sprite = EvaluarSelected;

                    //				DOTween.Play ("3");
                    //Texto que indica el nombre de la fase
                    Text_Changed.text = "EVALUAR";
                    break;
            }*/
            _actualMoment = clickMomentValue;
            StartCoroutine(ChangeScene("M_" + _actualMoment, _actualScn));
        }
    }

    public void ShowHideMenu()
    {
        _isHide = !_isHide;
        if (_isHide)
        {

        }
        else
        {

        }
    }
    #endregion


    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    public void SetLimitCtrl(int value)
    {
        _limitCtrl = value;
    }
    #endregion


    #region COROUTINES
    private IEnumerator ChangeScene(string sceneToLoad, string sceneToUnload)
    {
        if (sceneToUnload != "")
        {
            SceneManager.UnloadSceneAsync(sceneToUnload);
            _loadObj.SetActive(true);
            DOTween.Play("1");
        }
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
        yield return null;
        _loadObj.SetActive(false);
        DOTween.Pause("1");
        _actualScn = sceneToLoad;
    }
    #endregion
}
