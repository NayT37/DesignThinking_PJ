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
    #endregion


    #region SYSTEM_METHODS
    private void Awake() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        if (_internalID < 4)
        {
            _titleTxt = "Requisitos";
        }
        else
        {
            _titleTxt = "Cómo";
        }
        _internalTxt = "";
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
