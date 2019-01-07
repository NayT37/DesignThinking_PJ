using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlInternalText : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    [HideInInspector]
    public string _titleTxt, _internalTxt;
    //Private Variables
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    #endregion


    #region CREATED_METHODS
    private void Initializate() { }
    #endregion


    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    public string GetTitleTxt()
    {
        return _titleTxt;
    }
    public string GetInternalTxt()
    {
        return _internalTxt;
    }
    public virtual void SetInternalTxt(string value)
    {
        _internalTxt = value;
    }
    #endregion


    #region COROUTINES
    #endregion
}
