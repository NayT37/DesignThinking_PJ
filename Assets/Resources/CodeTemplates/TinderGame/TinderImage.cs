using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class TinderImage : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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
    private void Initializate() { }
    #endregion


    #region INTERFACE_METHODS

    public void OnPointerDown(PointerEventData eventData)
    {
        print(eventData.position);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        print(eventData);

    }
    #endregion


    #region COROUTINES
    #endregion
}
