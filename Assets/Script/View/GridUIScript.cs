using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;


public class GridUIScript : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler {

    public static Action onExit;
    public static Action<Transform> onEnter;





    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter.tag == "Grid")
        {
            print("GridPanelUI");
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
}
