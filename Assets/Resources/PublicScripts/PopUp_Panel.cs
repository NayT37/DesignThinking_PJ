using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;


public class PopUp_Panel : MonoBehaviour, IPointerClickHandler
{
    #region VARIABLES
    //Public Variables
    //Private Variables
    private bool _canClose;
    private GameObject _closeBtn;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _canClose = false;
        _closeBtn = transform.GetChild(0).Find("CloseBtn").gameObject;
        _closeBtn.SetActive(false);
        StartCoroutine(WaitUntilLoad());
    }

    private void ClosePanel()
    {
        if (_canClose)
            this.gameObject.SetActive(false);
    }
    #endregion


    #region INTERFACE_METHODS
    public void OnPointerClick(PointerEventData eventData)
    {
        ClosePanel();
    }
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    private IEnumerator WaitUntilLoad()
    {
        yield return new WaitForSeconds(2);
        _canClose = true;
        _closeBtn.SetActive(true);
    }

    #endregion
}
