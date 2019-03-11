using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Drag_M3_Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region VARIABLES
    //Public Variables
    public int internalID;

    public Note _note;
    public RectTransform rectTransform;
    //Private Variables
    private Transform _mainPanel;
    private Transform _contentParent;

    private Image __viewPort;
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

    private NoteServices _noteServices;

    private Button _detBtn, _editBtn;
    private M3_Ctrl _mainCtrl;
    private PostIt _mainPostIt;
    #endregion


    #region SYSTEM_METHODS
    private void Awake() { rectTransform = GetComponent<RectTransform>(); }
    private void Start() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _noteServices = new NoteServices();
        _mainPanel = GameObject.Find("TemporalParent").transform;
        _contentParent = GameObject.Find("Content").transform;
        __viewPort = GameObject.Find("VW").GetComponent<Image>();
        itemDragged = null;
        _initPosition = transform.position;
        _originalParent = _contentParent;
        _temporalParent = _mainPanel;
        _raycastImg = GetComponent<Image>();
        _parentGL = GameObject.Find("Content").GetComponent<VerticalLayoutGroup>();
        _internalText = GetComponentInChildren<Text>();
        transform.position = new Vector3(0, 0, 0);
        transform.localPosition = new Vector3(0, 0, 0);

        _detBtn = transform.Find("EditBtn__Drag").GetComponent<Button>();
        _editBtn = transform.Find("DetBtn__Drag").GetComponent<Button>();
        _detBtn.onClick.AddListener(DeleteDragItem);
        _editBtn.onClick.AddListener(EditDragItem);
    }

    private void DeleteDragItem()
    {
        if (_mainCtrl == null)
        {
            GameObject.FindObjectOfType<M3_Ctrl>();
        }
        //May be a M3 reference is required
        //_mainCtrl.DestroyPostIt(this);
        Destroy(this);
    }

    private void EditDragItem()
    {
        if (_mainCtrl == null)
        {
            GameObject.FindObjectOfType<M3_Ctrl>();
        }
        _mainCtrl.AddNewPostIt();
        _mainPostIt.SetInternalInput(_internalText.text);
    }

    public virtual void resetItem()
    {
        transform.position = _initPosition;
        transform.SetParent(_originalParent);
        _noteServices.UpdateNote(0, "");
        transform.SetAsFirstSibling();
    }

    public static GameObject getItemDragged()
    {
        return itemDragged;
    }


    public void ChangeText(string text)
    {
        _internalText = GetComponentInChildren<Text>();
        _internalText.text = text;
    }
    #endregion


    #region INTERFACE_METHODS
    //This method is implemented from the IBeginDragHandler Interface
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        DataBaseParametersCtrl.Ctrl._noteLoaded = _note;
        //Set the dragged item to this gameobject
        //transform.SetParent(_mainPanel);
        itemDragged = gameObject;
        //_parentGL.enabled = false;
        _raycastImg.raycastTarget = false;
        __viewPort.raycastTarget = false;
        __viewPort.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        __viewPort.GetComponent<Mask>().enabled = false;
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
        //_parentGL.enabled = true;
        itemDragged = null;
        __viewPort.raycastTarget = true;
        __viewPort.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        __viewPort.GetComponent<Mask>().enabled = true;
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
