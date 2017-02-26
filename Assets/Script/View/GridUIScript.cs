using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;


public class GridUIScript : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IDragHandler,IBeginDragHandler,IEndDragHandler {

    #region pointEnter,exit
    public static Action onExit;
    public static Action<Transform> onEnter;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter.tag == "Grid")
        {
            //print("GridPanelUI");
            if (onEnter != null)
                onEnter(this.transform);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter.tag == "Grid")
        {
            if (onExit != null)
                onExit();
        }
    } 
    #endregion




    public static Action<Transform> onLeftBeginDrag;
    public static Action<Transform, Transform> onLeftEndDrag;//<Transform,Transform>当前的,进入的

    public void OnDrag(PointerEventData eventData)
    {
 
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (onLeftBeginDrag != null)
                onLeftBeginDrag(this.transform);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (onLeftEndDrag != null)
            {
                if (eventData.pointerEnter == null)
                    onLeftEndDrag(this.transform,null);
                else
                    onLeftEndDrag(this.transform,eventData.pointerEnter.transform);
            }
        }
    }
}
