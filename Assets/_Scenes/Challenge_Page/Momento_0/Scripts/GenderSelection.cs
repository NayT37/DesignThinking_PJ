using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GenderSelection : MonoBehaviour
{

    #region VARIABLES
    //Public Variables
    //Private Variables
    private GameObject _itemHolder;
	private List<Button> _btnList;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _itemHolder = transform.Find("Item_Holder").gameObject;

    }
    #endregion


    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    #endregion
}
