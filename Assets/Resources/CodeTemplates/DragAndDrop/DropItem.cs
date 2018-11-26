using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class DropItem : MonoBehaviour, IDropHandler
{
    #region VARIABLES
    //Public Variables
    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    public bool admitDrop = true;
    //Private Variables
    #endregion


    #region SYSTEM_METHODS

    #endregion


    #region CREATED_METHODS

    #endregion


    #region INTERFACE_METHODS
    public virtual void OnDrop(PointerEventData eventData)
    {
        if (admitDrop)
        {
            if (!item)
            {
                DragItem.itemBeingDragged.transform.SetParent(transform);
                item.transform.localPosition = new Vector3(0, 0, 0);
            }
        }
    }
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    #endregion






}
