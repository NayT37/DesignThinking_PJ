using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PanelSaveIdea : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    public static PanelSaveIdea instance = null;
    [HideInInspector]
    public InputField inputTxt;
    [HideInInspector]
    public CtrlInternalText ctrlTxtObj;
    //Private Variables
    private Text _titleTxt;

    #endregion


    #region SYSTEM_METHODS
    private void Awake() { Initializate(); }
    private void Start() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }

        _titleTxt = transform.GetChild(0).GetChild(0).Find("Title_Txt").GetComponent<Text>();
        inputTxt = transform.GetChild(0).GetChild(1).Find("InputField_Txt").GetComponent<InputField>();
    }

    public void OpenPanel(CtrlInternalText obj)
    {
        this.gameObject.SetActive(true);
        ctrlTxtObj = obj;
        _titleTxt.text = ctrlTxtObj.GetTitleTxt();
        inputTxt.text = ctrlTxtObj.GetInternalTxt();

    }
    public void ClosePanel()
    {
        this.gameObject.SetActive(false);
    }
    #endregion


    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    #endregion
}
