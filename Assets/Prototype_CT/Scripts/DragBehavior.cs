using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragBehavior : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region Initialization of Variables
    //An static variable to call the dragged item easily from anywhere
    protected static GameObject itemDragged;
    //One more to store the initial parent
    protected Transform _originalParent;
    //The raycast must be blocked
    protected Image _raycastImg;
    //A variable to store the initial position
    protected Vector3 _initPosition;
    #endregion


    #region System Methods
    public virtual void Start()
    {
        itemDragged = null;
        _initPosition = transform.position;
        _originalParent = transform.parent;
        _raycastImg = GetComponent<Image>();
    }
    //This method is implemented from the IBeginDragHandler Interface
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        //Set the dragged item to this gameobject
        itemDragged = gameObject;
        _raycastImg.raycastTarget = false;

        if (transform.parent != _originalParent)
        {
            //If it doesn't change the parent (is not attached at an some EmptySlot), reset it
            resetItem();
        }
    }
    //This method is implemented from the IDragHandler Interface
    public virtual void OnDrag(PointerEventData eventData)
    {
        //Make this object follow the mouse
        transform.position = Input.mousePosition;
    }
    //This method is implemented from the IEndDragHandle Interface
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        //The draggable item doest not exists
        itemDragged = null;

        _raycastImg.raycastTarget = true;

        if (transform.parent != _originalParent)
        {
            transform.localPosition = new Vector2(0, 0);
        }
        else
        {
            resetItem();
        }
    }
    #endregion


    #region Created Methods
    public virtual void resetItem()
    {
        transform.position = _initPosition;
        transform.SetParent(_originalParent);
        transform.SetAsFirstSibling();
    }

    public virtual void SetRaycastTarget(bool value)
    {
        _raycastImg.raycastTarget = value;
    }

    public static GameObject getItemDragged()
    {
        return itemDragged;
    }
    #endregion
}
