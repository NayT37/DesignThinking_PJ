﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PostIt : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    //Private Variables
    private InputField _internalInput;

    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _internalInput = GetComponentInChildren<InputField>();
        print(_internalInput);
    }
    #endregion


    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    public InputField GetInternalInput()
    {
        return _internalInput;
    }
    #endregion


    #region COROUTINES
    #endregion
}
