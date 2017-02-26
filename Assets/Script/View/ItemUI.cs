using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour {

    public Text ItemName;

    public void UpdateItem(string name)
    {
    
        ItemName.text = name;
    }


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
