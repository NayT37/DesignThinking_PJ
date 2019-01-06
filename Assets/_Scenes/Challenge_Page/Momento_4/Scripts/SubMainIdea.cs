using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class SubMainIdea : MonoBehaviour, IPointerClickHandler
{
    #region VARIABLES
    //Public Variables
    //Private Variables
    [SerializeField]
    private int _internalID;
    private string _titleType, _internalText;
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
            _titleType = "Requisitos";
        }
        else
        {
            _titleType = "Cómo";
        }
        _internalText = "";
    }
    #endregion


    #region INTERFACE_METHODS
    public void OnPointerClick(PointerEventData eventData)
    {
        PanelSaveIdea.instance.OpenPanel(_titleType, _internalText);
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
