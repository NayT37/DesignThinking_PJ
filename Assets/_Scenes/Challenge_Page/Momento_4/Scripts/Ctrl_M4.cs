﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Ctrl_M4 : CtrlInternalText
{
    #region VARIABLES
    //Public Variables
    //Private Variables
    private string _mainIdea;
    [SerializeField]
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        PanelSaveIdea.instance.ClosePanel();
        _titleTxt = "Idea Principal";
        //DB stuff
        _internalTxt = "";
    }
    public void OpenSavePanel()
    {
        //DB stuff
        PanelSaveIdea.instance.OpenPanel(this);
    }
    public void CloseSavePanel()
    {
        PanelSaveIdea.instance.ClosePanel();
    }

    public void SaveInfoPanel()
    {
        if (PanelSaveIdea.instance.inputTxt.text != "")
        {
            PanelSaveIdea.instance.ctrlTxtObj.SetInternalTxt(PanelSaveIdea.instance.inputTxt.text);
            //DB stuff
            PanelSaveIdea.instance.ClosePanel();
        }
    }
    #endregion


    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    #endregion
}
