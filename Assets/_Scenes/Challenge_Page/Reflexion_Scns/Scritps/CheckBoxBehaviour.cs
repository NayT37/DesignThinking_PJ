using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckBoxBehaviour : MonoBehaviour, IPointerClickHandler
{


    #region VARIABLES
    //Public Variables
    //Private Variables
    GameObject _checkObj;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _checkObj = transform.Find("CheckBox").GetChild(0).gameObject;
        _checkObj.SetActive(false);
    }
    #endregion


    #region INTERFACE_METHODS
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        _checkObj.SetActive(!_checkObj.activeInHierarchy);
    }
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    #endregion
}
