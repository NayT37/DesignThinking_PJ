using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class ChildSubMainIdea : CtrlInternalText, IPointerClickHandler
{
    #region VARIABLES
    //Public Variables
    //Private Variables
    private enum ChildType
    {
        risk, advantage
    }
    [SerializeField]
    private ChildType _childType;
    private Image _internalImg;
    private Color _activeClr, _deactiveClr;
    private SubMainIdea _parentIdea;
    private int _internalID;
    private bool _canWrite;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _internalTxt = ""; //DB here to get internal text if exists
        _canWrite = false; // DB here to know if user can write on this
        _activeClr = new Color32(255, 255, 255, 255);
        _deactiveClr = new Color32(255, 255, 255, 100);
        _internalImg = GetComponent<Image>();
        if (_canWrite)
        {
            _internalImg.color = _activeClr;
        }
        else
        {
            _internalImg.color = _deactiveClr;
        }
        _parentIdea = GetComponentInParent<SubMainIdea>();
        _internalID = _parentIdea.GetInternalID();
        if (_childType == ChildType.risk) { _titleTxt = "Riesgos"; } else { _titleTxt = "Ventajas"; }
    }

    #endregion


    #region INTERFACE_METHODS
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_canWrite)
        {
            //Active a panel to change
            PanelSaveIdea.instance.OpenPanel(this);
        }
    }
    #endregion


    #region GETTERS_AND_SETTERS
    public override void SetInternalTxt(string value)
    {
        base.SetInternalTxt(value);
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
