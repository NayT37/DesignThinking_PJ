using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Drag_MindMap : DragItem
{
    #region VARIABLES
    //Public Variables
    [HideInInspector]
    public string internalText;
    public int ideaType;
    [Header("If ideaType is 0:")]
    public int mainIdeaNumber;
    //Private Variables
    private Text _internalUITxt;
    #endregion


    #region SYSTEM_METHODS
    public void Awake()
    {
        _internalUITxt = GetComponentInChildren<Text>();
    }
    public override void Start()
    {
        base.Start();
        _internalUITxt.text = internalText;
        this.gameObject.transform.localPosition = new Vector3(0f,0f,0f);
    }

    public void UpdateContent(string txt, int idea, int mainIdea)
    {
        internalText = txt;
        ideaType = idea;
        mainIdeaNumber = mainIdea;
    }
    #endregion


    #region CREATED_METHODS
    #endregion


    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    #endregion
}
