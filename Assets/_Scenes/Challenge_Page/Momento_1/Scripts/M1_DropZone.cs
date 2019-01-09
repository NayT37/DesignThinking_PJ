using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class M1_DropZone : DropBehavior
{

    #region VARIABLES
    //Public Variables
    //Private Variables
    [SerializeField]
    private int _internalNumber;
    private M1_Ctrl _levelCtrl;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    #endregion


    #region CREATED_METHODS
    private void Initializate() { }
    #endregion


    #region INTERFACE_METHODS
    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);

        if (_levelCtrl == null)
        {
            _levelCtrl = GameObject.FindObjectOfType<M1_Ctrl>();
        }
        DragBehavior temp = internalItem.GetComponent<DragBehavior>();
        temp.resetItem();
        temp.SetRaycastTarget(true);
        _levelCtrl.ActivePanelByNumber(_internalNumber, true);
    }
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    #endregion
}
