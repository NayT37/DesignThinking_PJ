using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgeSelection : MonoBehaviour
{

    #region VARIABLES
    //Public Variables
    //Private Variables
    private GameObject _itemHolder;

    private Color32 _neutralClr, _selectedClr;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _itemHolder = transform.Find("Item_Holder").gameObject;
        //Color
        _neutralClr = new Color32(255, 255, 255, 255);
        _selectedClr = new Color32(0, 255, 170, 255);
    }

	
    #endregion


    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    #endregion
}
