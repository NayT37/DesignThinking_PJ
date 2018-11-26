using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;

public class GoEvaluateBtn : MonoBehaviour, IPointerClickHandler
{


    #region VARIABLES
    //Public Variables
    //Private Variables
    private CasesHUD_Ctrl HUDCtrl;
    private bool _isTesting;
    [SerializeField]
    private GameObject _selectedPrototype;
    GameObject _parent;
    #endregion


    #region SYSTEM_METHODS
    private void Awake()
    {
        // DontDestroyOnLoad(this);
        _parent = GameObject.Find("EvaluateHolder");
        DontDestroyOnLoad(_parent);
    }
    private void Start() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {

        try
        {
            HUDCtrl = GameObject.FindObjectOfType<CasesHUD_Ctrl>();
            _isTesting = false;
        }
        catch (Exception e)
        {
            HUDCtrl = null;
            _isTesting = true;
        }
    }
    #endregion


    #region INTERFACE_METHODS
    public void OnPointerClick(PointerEventData eventData)
    {

        try
        {
            HUDCtrl.MomentBtnClick(5);
        }
        catch (Exception e)
        {
            //_selectedPrototype = GameObject.Find("3DModel");
            _selectedPrototype.transform.SetParent(_parent.transform);
            _selectedPrototype.transform.localPosition = new Vector3(0, 0, 0);
            //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            SceneManager.LoadScene("EvaluateScn");
        }
    }
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    #endregion
}
