using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragItemUIScript : ItemUI {

    /// <summary>
    /// 控制显隐 
    /// </summary>
    /// <param name="tf">true为显示</param>
    public void IsShowDragItem(bool tf)
    {
        this.gameObject.SetActive(tf);
    }


    public void SetLocalPosition(Vector2 pos)
    {
        transform.localPosition = pos;
    }
}
