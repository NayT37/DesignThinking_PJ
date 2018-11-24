using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;


public class DropBehavior : MonoBehaviour, IDropHandler
{
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

    public virtual void OnDrop(PointerEventData eventData)
    {
        if (!internalItem)
        {
            //Make the dropped object a child of this...
            DragBehavior.getItemDragged().transform.SetParent(transform);
            //Set to 0,0 position and...
            DragBehavior.getItemDragged().transform.localPosition = new Vector2(0, 0);
        }
    }
}
