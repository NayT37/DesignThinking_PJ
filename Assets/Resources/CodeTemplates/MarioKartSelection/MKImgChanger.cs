using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class MKImgChanger : MonoBehaviour, IPointerClickHandler
{
    #region VARIABLES
    //Public Variables
    public enum Direction
    {
        Left, Right
    }
    public Direction arrowDirection;
    //Private Variables
    private MKItem _itemHolder;
    #endregion
    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    private void Update() { }
    #endregion
    #region CREATED_METHODS
    private void Initializate()
    {
        //If arrow direction is left, flip image
        if (arrowDirection == Direction.Left)
        {
            Vector3 temp = transform.localScale;
            transform.localScale = new Vector3(temp.x * -1, temp.y, temp.z);
        }
        _itemHolder = GetComponentInParent<MKItem>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (arrowDirection == Direction.Left)
        {
            _itemHolder.ShowPreviousImage();
        }
        else { _itemHolder.ShowNextImage(); }
    }
    #endregion
    #region COROUTINES
    #endregion
}
