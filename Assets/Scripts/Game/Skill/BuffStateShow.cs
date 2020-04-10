using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffStateShow : MonoBehaviour {

    public static BuffStateShow _instance;
    public BuffItem[] buffItems;
    private void Awake()
    {
        _instance = this;
    }
    // Use this for initialization
    void Start () {
       
	}
	
    public void BuffShow(int id,float BuffTime)
    {
        foreach(BuffItem item in buffItems)
        {
            if(item.id==0)
            {
                item.SetID(id,BuffTime);
                break;
            }
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
