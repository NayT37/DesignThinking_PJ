using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    #region VARIABLES
    //Public Variables
    public bool canBeDragged;
    [HideInInspector]
    public static GameObject itemBeingDragged;
    //Private Variables
    private Vector3 _startPosition;
    private Transform _startParent;
    private Transform _originalParent;
    private Image _internalImg;

    #endregion


    #region SYSTEM_METHODS
    public virtual void Start() { Initializate(); }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _internalImg = GetComponent<Image>();
        canBeDragged = true;
        _originalParent = transform.parent;
    }
    #endregion


    #region INTERFACE_METHODS
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        if (canBeDragged)
        {
            itemBeingDragged = gameObject;
            _startPosition = transform.localPosition;
            _startParent = transform.parent;
            _internalImg.raycastTarget = false;
        }
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if (canBeDragged) { transform.position = eventData.position; }

    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {

        itemBeingDragged = null;
        _internalImg.raycastTarget = true;
        if (transform.parent == _startParent)
        {
            transform.localPosition = _startPosition;
        }

    }
    #endregion


    #region GETTERS_AND_SETTERS
    public Transform GetOriginalParent()
    {
        return _originalParent;
    }
    #endregion


    #region COROUTINES
    #endregion
}
