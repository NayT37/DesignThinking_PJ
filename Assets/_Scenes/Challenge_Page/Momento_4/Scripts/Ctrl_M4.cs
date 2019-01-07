using System.Collections;
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
    private SubMainIdea[] _subMainIdeasArray;
    private PanelSaveFeedback _panelFeedback;
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
        _subMainIdeasArray = new SubMainIdea[6];
        for (int i = 0; i < 6; i++)
        {
            _subMainIdeasArray[i] = GameObject.Find("SubIdea_" + (i + 1)).GetComponent<SubMainIdea>();
        }
        //DB stuff
        _internalTxt = "";
        _panelFeedback = GameObject.FindObjectOfType<PanelSaveFeedback>();
        _panelFeedback.gameObject.SetActive(false);
        SetSubMainIdeaText();
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

    private void SetSubMainIdeaText()
    {
        //DB stuff
        for (int i = 0; i < 6; i++)
        {
            _subMainIdeasArray[i].SetInternalTxt("Hola soy " + (i + 1));
            _subMainIdeasArray[i].SetChildsText("Oportunidad " + i, "Riesgo " + (i + 1));
        }
    }

    public void SavePrototypeVersion()
    {
        //DBStuff
        _panelFeedback.OpenPanel(1, 1);
    }
    #endregion


    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    #endregion
}
