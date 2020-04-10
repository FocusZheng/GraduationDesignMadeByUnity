using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public static Inventory _instance;
    public List<InventoryGrid> inventoryGrids = new List<InventoryGrid>();
    // Use this for initialization
    public TweenPosition inventoryTween;
    public int Coins = 1000;
    public UILabel coinNumberLabel;
    public GameObject inventoryitem;
    
    public void ShowInventory()
    {
        inventoryTween.enabled = true;
        inventoryTween.PlayForward();
        
    }
    public void CloseInventory()
    {
        inventoryTween.PlayReverse();
    }
    private void Awake()
    {
        _instance = this;
        GameObject.Find("coin-count").GetComponentInChildren<UILabel>().text = Coins.ToString();
        inventoryTween = this.GetComponent<TweenPosition>();

        //tween.AddOnFinished(this.OnTweenPlayFinished);
        //this.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update () {
		//按下Y键 拾取物品
        if(Input.GetKeyDown(KeyCode.Y))
        {
          
            ObjectTake(1001);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
          
            ObjectTake(2001);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
          
            ObjectTake(2002);
        }  
	}
 

    public bool ReduceCoin(int coin)
    {
        if(Coins>=coin)
        {
            Coins -= coin;
            Debug.Log("金币减少");
            GameObject.Find("coin-count").GetComponentInChildren<UILabel>().text = Coins.ToString();
            return true;
        }
        return false;
        

    }
    public void GetCoin(int coin)
    {
       
        Coins += coin;
        Debug.Log("金币增加");
        GameObject.Find("coin-count").GetComponentInChildren<UILabel>().text = Coins.ToString();
    }
    public void ObjectTake(int id,int num=1)//捡起物品调用该方法 并添加到物品栏里面
    {

        InventoryGrid grid = null;//格子为空
        //如果该物品大于规定数量 继续生成新的该物品 num+1;
        foreach (InventoryGrid temp in inventoryGrids)
        {
            
            if (temp.id == id) //如果物品相同 num+1
            {
                
                //Debug.Log("物品相同,直接数量+1");
                grid = temp;
                //Debug.Log(temp.name);
                break;
            }
        } 
            if(grid!=null) //grid已被赋值 直接
            {
                //Debug.Log("数量+1");
            grid.PlusObject(num);
               /* grid.PlusObject(int.Parse(ShopUI._instance.MatNum.value)); *///购买时调用这个
            }
            else  //不存在的情况
            {
            //Debug.Log("找个空格子放下");
                foreach(InventoryGrid temp in inventoryGrids)  //查找空的格子
                {
                    if (temp.id == 0)
                {
                    grid = temp;
                  
                    break;
                }
              
                }
            if (grid != null)
            {

                GameObject itemGo = NGUITools.AddChild(grid.gameObject, inventoryitem);
                itemGo.transform.localPosition = Vector3.zero;
                grid.ObjectShowInBag(id,num);
            }
        }
            
        }

    }
  


