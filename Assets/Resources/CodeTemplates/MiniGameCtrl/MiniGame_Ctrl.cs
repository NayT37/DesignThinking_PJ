using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame_Ctrl : MonoBehaviour
{

    #region VARIABLES
    //Public Variables
    [HideInInspector]
    public bool isGameFinished;
    [HideInInspector]
    public float progressBar;
    [Header("Total Progress")]
    public float completeProgressValue;
    [HideInInspector]
    public float actualProgressValue;
    //Private Variables
    #endregion


    #region SYSTEM_METHODS
    #endregion


    #region CREATED_METHODS
    public virtual void UpdateProgressBar()
    {
        progressBar += 1;
        UpdateActualProgress();
    }

    public virtual void UpdateActualProgress()
    {
        actualProgressValue = (progressBar * 100) / completeProgressValue;
    }
    #endregion


    #region INTERFACE_METHODS
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    #endregion
}
