using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Drop_M3_Zone : MonoBehaviour, IDropHandler
{
    #region VARIABLES
    //Public Variables
    //Private Variables
    protected GameObject internalItem
    {
        get
        {
            //If there is a child...
            if (transform.childCount > 0)
            {
                //Make this reference that child
                return transform.GetChild(0).gameObject;
            }
            else
            {
                //Otherwise is null
                return null;
            }
        }
    }
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate() { }
    #endregion


    #region INTERFACE_METHODS
    public virtual void OnDrop(PointerEventData eventData)
    {
        if (!internalItem)
        {
            //Make the dropped object a child of this...
            Drag_M3_Item.getItemDragged().transform.SetParent(transform);
            //Set to 0,0 position and...
            Drag_M3_Item.getItemDragged().transform.localPosition = new Vector2(0, 0);
        }
    }
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    #endregion
}
