using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipGrid : MonoBehaviour {

    public static EquipGrid _instance;
    public Objectinfomation.DressType EquipType;
    public int id=0;
	// Use this for initialization
	void Awake () {
        _instance = this;
       
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Clearinfo()
    {
        this.id = 0;
        
    }

}
