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
    public static ChMainHUD instance = null;
    //Private Variables
    private int _actualMoment;
    private string _actualScn;
    [SerializeField]
    private int _limitCtrl;
    private GameObject _loadObj;
    private bool _isHide;
    private Transform _menuHolder;
    private Vector3 _showPosition, _hidePosition;

    private PublicServices _publicServices;
    #endregion


    #region SYSTEM_METHODS
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }
    private void Start() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _actualMoment = 0;

        _publicServices = new PublicServices();

        int projectId = DataBaseParametersCtrl.Ctrl._projectLoaded.id;

        var _public = _publicServices.GetPublicNamed(projectId);

        if (_public.id != 0)
        {
            _actualMoment = 1;
        }

        XRSettings.enabled = false;
        _isHide = false;
        _loadObj = GameObject.Find("Loading_Bg");

        _limitCtrl = 0;
        _actualScn = "";
        _menuHolder = GameObject.Find("Menu_Holder").transform;
        _showPosition = new Vector3(0, 0, 0);
        _hidePosition = new Vector3(0, -155, 0);
        StartCoroutine(ChangeScene("M_" + _actualMoment, _actualScn));
    }

    public void MomentBtnClick(int clickMomentValue)
    {
        XRSettings.enabled = false;
        if (_actualMoment != clickMomentValue && clickMomentValue <= _limitCtrl)
        {
            _actualMoment = clickMomentValue;
            StartCoroutine(ChangeScene("M_" + _actualMoment, _actualScn));
        }
    }

    public void ShowHideMenu()
    {
        _isHide = !_isHide;
        if (_isHide)
        {
            _menuHolder.localPosition = _showPosition;
        }
        else
        {
            _menuHolder.localPosition = _hidePosition;
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
