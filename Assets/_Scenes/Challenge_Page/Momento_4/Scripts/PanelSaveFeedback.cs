using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PanelSaveFeedback : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    //Private Variables
    private Text _internalTxt;
    #endregion


    #region SYSTEM_METHODS
    private void Awake() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _internalTxt = transform.GetChild(0).GetComponentInChildren<Text>();
    }

    public void OpenPanel(int ideateVersion, int prototypeVersion)
    {
        _internalTxt.text = "PROTOTIPAR " + ideateVersion + "." + prototypeVersion + " GUARDADO.";
        this.gameObject.SetActive(true);
        StartCoroutine(WaitTimeToClose());
    }

    private void ClosePanel() { this.gameObject.SetActive(false); }
    #endregion


    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    private IEnumerator WaitTimeToClose()
    {
        yield return new WaitForSeconds(1);
        ClosePanel();
    }
    #endregion
}
