using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolKitUIScript : MonoBehaviour {
    public Text outLineText;
    public Text contentText;
    void Awake()
    {
     //   outLineText = this.GetComponent<Text>();
        contentText = this.GetComponentInChildren<Text>();
    }

    public void updateText(string strName)
    {
        //Debug.LogError("strName");
        outLineText.text = strName;
        contentText.text = strName;
    }

    /// <summary>
    /// 控制显隐 
    /// </summary>
    /// <param name="tf">true为显示</param>
    public void IsShowToolKit(bool tf)
    {
        this.gameObject.SetActive(tf);
    }


    public void SetLocalPosition(Vector2 pos)
    {
        transform.localPosition = pos;
    }
}
