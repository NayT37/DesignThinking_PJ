using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class VersionTab : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    //Private Variables
    private Text _internalTxt;
    #endregion


    #region SYSTEM_METHODS
    private void Awake() { Initializate(); }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _internalTxt = GetComponentInChildren<Text>();
    }
    #endregion


    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    public void SetInternalText(int ideaNumber)
    {
        _internalTxt.text = "IDEA " + ideaNumber;
    }
    #endregion


    #region COROUTINES
    #endregion
}
