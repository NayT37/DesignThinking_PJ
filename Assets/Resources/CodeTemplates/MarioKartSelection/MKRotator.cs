using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class MKRotator : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    #region VARIABLES
    //Public Variables
    //Private Variables
    private Transform _3dModel;
    private Vector3 _originalModelPos;
    private float _originPos;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void Update() { }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _3dModel = GameObject.Find("DisplayedImg").transform.GetChild(0);
        _originalModelPos = _3dModel.position;
        _originPos = 0;
    }


    #endregion


    #region INTERFACE_METHODS
    public void OnBeginDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        _originPos = Input.mousePosition.x;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //float temp = Input.mousePosition.x / 100;
        float temp = Input.mousePosition.x - _originPos;
        // _3dModel.rotation = new Vector3(_originalModelPos.x, temp, _originalModelPos.z);
        Vector3 rotationVector = new Vector3(_3dModel.localEulerAngles.x, _3dModel.localEulerAngles.y + (temp / 25), _3dModel.localEulerAngles.z);
        _3dModel.localRotation = Quaternion.Euler(rotationVector);
        // _3dModel.localRotation = new Quaternion(0, temp, 0, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    #endregion
}
