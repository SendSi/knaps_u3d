using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 只控制显示 
/// </summary>
public class GridPanelUI : MonoBehaviour {

    public Transform[] Grids;

    public Transform GetEmpeyGrid()
    {
        for (int i = 0; i < Grids.Length; i++)
            {
                if (Grids[i].childCount == 0)
                {
                    return Grids[i];
                }
            }
        return null;
    }

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
