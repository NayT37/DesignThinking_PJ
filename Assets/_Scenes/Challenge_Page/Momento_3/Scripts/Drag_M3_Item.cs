﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class Drag_M3_Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region VARIABLES
    //Public Variables
    //Private Variables
    private Transform _mainPanel;
    private Transform _contentParent;
    private VerticalLayoutGroup _parentGL;
    //An static variable to call the dragged item easily from anywhere
    private static GameObject itemDragged;
    //One more to store the initial parent
    private Transform _originalParent, _temporalParent;
    //The raycast must be blocked
    private Image _raycastImg;
    //A variable to store the initial position
    private Vector3 _initPosition;
    private Text _internalText;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _mainPanel = GameObject.Find("TemporalParent").transform;
        _contentParent = GameObject.Find("Content").transform;
        itemDragged = null;
        _initPosition = transform.position;
        _originalParent = transform.parent;
        _temporalParent = _mainPanel;
        _raycastImg = GetComponent<Image>();
        _parentGL = GetComponentInParent<VerticalLayoutGroup>();
        _internalText = GetComponentInChildren<Text>();
    }

    public virtual void resetItem()
    {
        transform.position = _initPosition;
        transform.SetParent(_originalParent);
        transform.SetAsFirstSibling();
    }

    public static GameObject getItemDragged()
    {
        return itemDragged;
    }
    #endregion


    #region INTERFACE_METHODS
    //This method is implemented from the IBeginDragHandler Interface
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        //Set the dragged item to this gameobject
        transform.SetParent(_mainPanel);
        itemDragged = gameObject;
        _parentGL.enabled = false;
        _raycastImg.raycastTarget = false;
        //  _originalParent = _mainPanel;

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
        _raycastImg.raycastTarget = true;
        _parentGL.enabled = true;
        itemDragged = null;
    }

    public void ChangeText(string text)
    {
        _internalText = GetComponentInChildren<Text>();
        _internalText.text = text;
    }
    #endregion


    #region GETTERS_AND_SETTERS


    public virtual void SetRaycastTarget(bool value)
    {
        _raycastImg.raycastTarget = value;
    }
    #endregion


    #region COROUTINES
    #endregion
}