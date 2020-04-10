using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShop : NPC {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShopUI._instance.ShowShop();
            Debug.Log("NPC点击");
        }
    }
   
}
