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
    private Image _internalImg;
    private Color _activeClr, _deactiveClr;
    private bool _canWrite;
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
        _internalImg = GetComponent<Image>();
        _feedbackObj = transform.Find("FeedbackImg").gameObject;
        _feedbackObj.SetActive(false);
        _activeClr = new Color32(255, 255, 255, 255);
        _deactiveClr = new Color32(255, 255, 255, 100);
        _canWrite = false;
        if (_canWrite)
        {
            _internalImg.color = _activeClr;
        }
        else
        {
            _internalImg.color = _deactiveClr;
        }
    }

    public override void SetInternalTxt(string value)
    {
        base.SetInternalTxt(value);
        if (_childsArray[0] == null)
        {
            _childsArray[0] = transform.GetChild(0).GetComponent<ChildSubMainIdea>();
            _childsArray[1] = transform.GetChild(1).GetComponent<ChildSubMainIdea>();
        }
        if (value != "")
        {
            _feedbackObj.SetActive(true);

            _childsArray[0].SetCanWrite(true);
            _childsArray[1].SetCanWrite(true);
        }
        else
        {
            _feedbackObj.SetActive(false);
            _childsArray[0].SetCanWrite(false);
            _childsArray[1].SetCanWrite(false);
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
    }

    public void SetChildsRiskTxt(string riskTxt)
    {
        if (_childsArray[1] == null)
        {
            _childsArray[1] = transform.GetChild(1).GetComponent<ChildSubMainIdea>();
        }
        _childsArray[1].SetInternalTxt(riskTxt);
        //_childsArray[1].SetCanWrite(true);
    }


    #endregion


    #region INTERFACE_METHODS
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_canWrite)
            PanelSaveIdea.instance.OpenPanel(this);
    }
    #endregion


    #region GETTERS_AND_SETTERS
    public int GetInternalID()
    {
        return _internalID;
    }

    public void SetCanWrite(bool value)
    {
        _canWrite = value;
        if (value)
        {
            _internalImg.color = _activeClr;
        }
        else
        {
            _internalImg.color = _deactiveClr;
        }
    }
    #endregion


    #region COROUTINES
    #endregion
}
