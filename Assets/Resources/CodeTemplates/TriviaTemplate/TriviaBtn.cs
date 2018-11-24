using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TriviaBtn : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    [HideInInspector]
    public Text internalText;
    //Private Variables
    [SerializeField]
    private bool _isCorrect;
    #endregion


    #region SYSTEM_METHODS
    private void Awake() { Initializate(); }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        internalText = GetComponentInChildren<Text>();
    }
    #endregion

    #region GETTERS_AND_SETTERS
    public bool getIsCorrect()
    {
        return _isCorrect;
    }
    #endregion
}
