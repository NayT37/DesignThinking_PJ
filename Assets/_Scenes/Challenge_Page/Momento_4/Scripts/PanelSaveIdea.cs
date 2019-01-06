using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PanelSaveIdea : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    public static PanelSaveIdea instance = null;
    public string txtToChange, internalInpuptTxt;
    //Private Variables
    private Text _titleTxt;
    public InputField inputTxt;
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
        txtToChange = null;
        internalInpuptTxt = inputTxt.text;
    }

    public void OpenPanel(string title, string text)
    {
        this.gameObject.SetActive(true);
        _titleTxt.text = title;
        inputTxt.text = text;
        txtToChange = text;
    }
    public void ClosePanel()
    {
        _titleTxt.text = null;
        inputTxt.text = null;
        txtToChange = null;
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
