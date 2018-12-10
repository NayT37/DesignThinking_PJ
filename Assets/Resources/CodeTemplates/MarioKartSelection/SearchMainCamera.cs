using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class SearchMainCamera : MonoBehaviour
{
    #region VARIABLES
    //Private Variables
    private Canvas _myCanvas;
    #endregion


    #region SYSTEM_METHODS
    private void Awake() { Initializate(); }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _myCanvas = GetComponent<Canvas>();
        try
        {
            _myCanvas.worldCamera = GameObject.Find("DefaultCamera").GetComponent<Camera>();
        }
        catch (NullReferenceException e)
        {
            Debug.Log("-- Main Camera -- wasn't found, searching -- MainCamera -- instead.");
            _myCanvas.worldCamera = GameObject.Find("DefaultCamera").GetComponent<Camera>();
        }

    }
    #endregion
}
