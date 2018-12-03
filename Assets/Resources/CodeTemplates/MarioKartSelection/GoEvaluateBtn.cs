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
    private GameObject _parent;

    private Animator evaluateHolder;
    private EvaluateHolder _evaluateHolder;

    #endregion


    #region SYSTEM_METHODS
    private void Awake()
    {
        _parent = GameObject.Find("EvaluateHolder");
        _evaluateHolder = GameObject.Find("EvaluateHolder").GetComponent<EvaluateHolder>();
        DontDestroyOnLoad(_parent);
    }
    private void Start() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        HUDCtrl = GameObject.FindObjectOfType<CasesHUD_Ctrl>();
        _isTesting = false;
    }

    #endregion


    #region INTERFACE_METHODS
    public void OnPointerClick(PointerEventData eventData)
    {
        int temp = HUDCtrl._actualCase;
        //evaluateHolder = GameObject.Find("EvaluateHolder").GetComponent<Animator>();
        if (_parent.transform.GetChild(temp - 1).childCount > 0)
        {
            Destroy(_parent.transform.GetChild(temp - 1).GetChild(0).gameObject);
        }
        _selectedPrototype.transform.SetParent(_parent.transform.GetChild(temp - 1));
        _selectedPrototype.transform.localPosition = new Vector3(0, 0, 0);

        switch (temp)
        {
            case 1:
                _selectedPrototype.transform.localEulerAngles = new Vector3(0, 180, 0);
                break;
            case 2:
                _selectedPrototype.transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
            case 3:
                _selectedPrototype.transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
        }

        _evaluateHolder.playAnimation(HUDCtrl._actualCase);
        //        HUDCtrl.playAnimation();
        HUDCtrl.MomentBtnClick(5);
    }
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    #endregion
}
