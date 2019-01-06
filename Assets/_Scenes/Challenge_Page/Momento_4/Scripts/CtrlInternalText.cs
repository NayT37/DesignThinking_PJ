using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlInternalText : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    //Private Variables
    public string _titleTxt, _internalTxt;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void Update() { }
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
    public void SetInternalTxt(string value)
    {
        _internalTxt = value;
    }
    #endregion


    #region COROUTINES
    #endregion
}
