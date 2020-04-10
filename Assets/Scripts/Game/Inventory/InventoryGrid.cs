using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGrid : MonoBehaviour {

    //public static InventoryGrid _instance;
    public int id = 0;
    public UILabel numLabel;
    
    public int num = 0;//物品数量
    private Objectinfomation info=null;
    
	// Use this for initialization
	void Awake () {
        //_instance = this;
        numLabel = this.GetComponentInChildren<UILabel>();
	}
	
	// Update is called once per frame
    
    public void ObjectShowInBag(int id,int num=1)
    {
        //实例化一个Item 
        //sprite赋值为info.icon
        //new Label显示
       
        info = ObjectInfo._instance.GetInfoByID(id);
        InventoryItem item = this.GetComponentInChildren<InventoryItem>();
        this.id = info.id;//当前格子的物品编号赋值
        item.id = this.id;
        Debug.Log(info.id);
        item.SetIconName(info.icon);

        this.num = num;  
        numLabel.enabled = true;
        numLabel.text = num.ToString();

    }
    public void ClearInfo()
    {
        this.id = 0;
        this.num = 0;
        info = null;
        numLabel.enabled = false;
        numLabel.text = num.ToString();


    }
    public void PlusObject(int num=1)
    {
        this.num += num;
        numLabel.text = this.num.ToString();
        Debug.Log("当前物品数量"+this.num);
    }
    public void MinObject(int num=1)
    {
        if(this.num==0)
        {
            return;
        }
        this.num -= num;
        
        numLabel.text = this.num.ToString();

        Debug.Log(this.num);
        if (this.num==0)
        {        
            Destroy(this.GetComponentInChildren<InventoryItem>().gameObject);
            transform.GetChild(1).gameObject.SetActive(false);
            ClearInfo();
        }
    }
  

}
