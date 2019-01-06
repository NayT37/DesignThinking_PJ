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
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _internalTxt = ""; //DB here to get internal text if exists
        _activeClr = new Color32(255, 255, 255, 255);
        _deactiveClr = new Color32(255, 255, 255, 100);
        _internalImg = GetComponent<Image>();
        if (_internalTxt != "")
        {
            _internalImg.color = _deactiveClr;
        }
        else
        {
            _internalImg.color = _activeClr;
        }
        _parentIdea = GetComponentInParent<SubMainIdea>();
        _internalID = _parentIdea.GetInternalID();
        if (_childType == ChildType.risk) { _titleTxt = "Riesgos"; } else { _titleTxt = "Ventajas"; }
        _internalTxt = "";
    }


    #endregion


    #region INTERFACE_METHODS
    public void OnPointerClick(PointerEventData eventData)
    {
        //Active a panel to change
        PanelSaveIdea.instance.OpenPanel(this);
    }
    #endregion


    #region GETTERS_AND_SETTERS
    public void SetInternalTxt(string value)
    {
        _internalTxt = value;
    }
    #endregion


    #region COROUTINES
    #endregion
}
