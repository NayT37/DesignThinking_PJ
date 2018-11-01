using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TinderImage : MonoBehaviour
{
    #region VARIABLES
    //Public Variables
    [HideInInspector]
    public Image internalImage;
    [HideInInspector]
    public bool isCorrect;
    //Private Variables
    #endregion
    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void Update() { }
    #endregion
    #region CREATED_METHODS
    private void Initializate() {}
    #endregion
    #region COROUTINES
    #endregion
}
