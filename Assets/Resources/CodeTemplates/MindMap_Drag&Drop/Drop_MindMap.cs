using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_MindMap : DropItem
{
    #region VARIABLES
    //Public Variables
    public int expectedIdeaType;
    public bool isMain;
    //Private Variables
    private Drag_MindMap _mmDragItem;
    [SerializeField]
    private Drop_MindMap[] _childsArray;

    //Event Variables
    public delegate void DropFinished();
    public static event DropFinished OnDropFinish;
    #endregion


    #region SYSTEM_METHODS
    private void Start() { Initializate(); }
    #endregion


    #region CREATED_METHODS
    private void Initializate()
    {
        _mmDragItem = null;
        if (isMain)
        {
            foreach (Drop_MindMap obj in _childsArray)
            {
                obj.admitDrop = false;
            }
        }
    }

    #endregion


    #region INTERFACE_METHODS
    public override void OnDrop(UnityEngine.EventSystems.PointerEventData eventData)
    {
        base.OnDrop(eventData);
        if (item && admitDrop)
        {
            _mmDragItem = item.GetComponent<Drag_MindMap>();
            if (_mmDragItem.ideaType == expectedIdeaType)
            {
                admitDrop = false;
                _mmDragItem.canBeDragged = false;
                OnDropFinish();
            }
            else
            {
                DragItem.itemBeingDragged.transform.SetParent(_mmDragItem.GetOriginalParent());
            }
            if (isMain)
            {
                foreach (Drop_MindMap obj in _childsArray)
                {
                    obj.expectedIdeaType = _mmDragItem.mainIdeaNumber;
                    obj.admitDrop = true;
                }
            }
        }
    }
    #endregion


    #region GETTERS_AND_SETTERS
    #endregion


    #region COROUTINES
    #endregion
}
