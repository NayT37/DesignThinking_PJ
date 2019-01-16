using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class SubMainIdea : CtrlInternalText, IPointerClickHandler
{
    #region VARIABLES
    //Public Variables
    //Private Variables
    [SerializeField]
    private int _internalID;
    private GameObject _feedbackObj;
    private ChildSubMainIdea[] _childsArray;
    #endregion


    #region SYSTEM_METHODS
    private void Awake() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _childsArray = new ChildSubMainIdea[2];
        if (_internalID < 4)
        {
            _titleTxt = "Requisitos";
        }
        else
        {
            _titleTxt = "Cómo";
        }
        _internalTxt = "";
        _feedbackObj = transform.Find("FeedbackImg").gameObject;
        _feedbackObj.SetActive(false);
    }

    public override void SetInternalTxt(string value)
    {
        base.SetInternalTxt(value);
        if (value != "")
        {
            _feedbackObj.SetActive(true);
            if (_childsArray[0] == null)
            {
                _childsArray[0] = transform.GetChild(0).GetComponent<ChildSubMainIdea>();
                _childsArray[1] = transform.GetChild(1).GetComponent<ChildSubMainIdea>();
            }
            _childsArray[0].SetCanWrite(true);
            _childsArray[1].SetCanWrite(true);
        }
    }

    public void SetChildsText(string oportunityTxt, string riskTxt)
    {
        if (_childsArray[0] == null)
        {
            _childsArray[0] = transform.GetChild(0).GetComponent<ChildSubMainIdea>();
            _childsArray[1] = transform.GetChild(1).GetComponent<ChildSubMainIdea>();
        }
        _childsArray[0].SetInternalTxt(oportunityTxt);
        _childsArray[1].SetInternalTxt(riskTxt);
        _childsArray[0].SetCanWrite(true);
        _childsArray[1].SetCanWrite(true);
    }

    public void SetChildsOportunityTxt(string oportunityTxt)
    {
        if (_childsArray[0] == null)
        {
            _childsArray[0] = transform.GetChild(0).GetComponent<ChildSubMainIdea>();
        }
        _childsArray[0].SetInternalTxt(oportunityTxt);
        _childsArray[0].SetCanWrite(true);
    }

    public void SetChildsRiskTxt(string riskTxt)
    {
        if (_childsArray[1] == null)
        {
            _childsArray[1] = transform.GetChild(1).GetComponent<ChildSubMainIdea>();
        }
        _childsArray[1].SetInternalTxt(riskTxt);
        _childsArray[1].SetCanWrite(true);
    }


    #endregion


    #region INTERFACE_METHODS
    public void OnPointerClick(PointerEventData eventData)
    {
        PanelSaveIdea.instance.OpenPanel(this);
    }
    #endregion


    #region GETTERS_AND_SETTERS
    public int GetInternalID()
    {
        return _internalID;
    }
    #endregion


    #region COROUTINES
    #endregion
}
