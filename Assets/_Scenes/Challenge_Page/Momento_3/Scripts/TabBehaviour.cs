using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class TabBehaviour : MonoBehaviour, IPointerClickHandler
{
    #region VARIABLES
    //Public Variables
    //Private Variables
    [SerializeField]
    private bool _isMain;
    [SerializeField]
    private int _internalID;
    private Text _internalText;
    private M3_Ctrl _mainCtrl;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _internalText = transform.GetChild(0).GetComponent<Text>();
        _internalText.text = "IDEA " + _internalID;
        _mainCtrl = null;
    }

    public void ChangeText(string text)
    {
        _internalText.text = text;
    }
    #endregion


    #region INTERFACE_METHODS
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_isMain)
        {
            if (_mainCtrl == null) _mainCtrl = GameObject.FindObjectOfType<M3_Ctrl>();
            TabBehaviour temp = transform.parent.GetComponent<TabBehaviour>();
            int tempID = temp.GetInernalID();
            temp.SetInernalID(_internalID);
            _mainCtrl.SetActualTab(_internalID);
            this.SetInernalID(tempID);
            _mainCtrl.HideTabs();
        }
    }
    #endregion


    #region GETTERS_AND_SETTERS
    public int GetInernalID()
    {
        return _internalID;
    }
    public void SetInernalID(int value)
    {
        _internalID = value;
        _internalText.text = "IDEA " + _internalID;
    }
    #endregion


    #region COROUTINES
    #endregion
}
