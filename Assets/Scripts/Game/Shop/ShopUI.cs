using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour {

    public static ShopUI _instance;

    public GameObject Mat;
    public GameObject[] MatGrid; //商品格子
    public UISlider BuyNumSlider;
    public UISlider SellNumSlider;
    public UIInput MatNum;
    public int CurrentPage=1;//当前页
    public int ItemsPage;//总页数
    public UIButton PreButton;
    public UIButton NextButton;
    private UILabel PageNum;
    public GameObject Inform;
    private UIButton CloseButton;
    public int BuyId;
    [HideInInspector]
    public int buyprice;//购买价格
    public int buyNum; //购买数量
    public int buySell;//卖出价格
    public bool IsBuy=false;
    public TweenPosition TweenShopUI;
    public int[] Items; //(1001,1002,1003,) //物品序号
                         // Use this for initialization

    public void ShowShop()
    {
        TweenShopUI.enabled = true;
        TweenShopUI.PlayForward();
    }
    public void HideShop()
    {
        TweenShopUI.PlayReverse();
    }
    private void Awake()
    {
        _instance = this;
        
    }
    void Start() {
        TweenShopUI = this.GetComponent<TweenPosition>();
        PageNum =transform.Find("PageNum").GetComponent<UILabel>();
        if(Items.Length%3==0)
        {
            ItemsPage = Items.Length / 3;
        }
        else
        {
            ItemsPage = (Items.Length / 3) + 1;
        }
        
        PageHideShow();
        if (Items.Length<=3) 
        {
           for(int i=0;i<Items.Length;i++)
            {
                Objectinfomation info = null;
                info = ObjectInfo._instance.GetInfoByID(Items[i]);
                NGUITools.AddChild(MatGrid[i].gameObject, Mat);
                MatGrid[i].GetComponentInChildren<ShopItems>().id = info.id;
                MatGrid[i].GetComponentInChildren<ShopItems>().NameLabel.text = info.objectname;
                MatGrid[i].GetComponentInChildren<ShopItems>().PriceLabel.text = info.price_buy.ToString();
                MatGrid[i].GetComponentInChildren<ShopItems>().EffectLabel.text = info.price_sell.ToString();//介绍暂无 用卖的价格代替
                MatGrid[i].GetComponentInChildren<ShopItems>().icon.spriteName = info.icon;
                MatGrid[i].GetComponentInChildren<ShopItems>().SetPosition();
            }
                          
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                Objectinfomation info = null;
                info = ObjectInfo._instance.GetInfoByID(Items[i]);
                NGUITools.AddChild(MatGrid[i].gameObject, Mat);
                MatGrid[i].GetComponentInChildren<ShopItems>().id = info.id;
                MatGrid[i].GetComponentInChildren<ShopItems>().NameLabel.text = info.objectname;
                MatGrid[i].GetComponentInChildren<ShopItems>().PriceLabel.text = info.price_buy.ToString();
                MatGrid[i].GetComponentInChildren<ShopItems>().EffectLabel.text = info.price_sell.ToString();//介绍暂无 用卖的价格代替
                MatGrid[i].GetComponentInChildren<ShopItems>().icon.spriteName = info.icon;
                MatGrid[i].GetComponentInChildren<ShopItems>().SetPosition();
            }
        }
        CloseButton = Inform.transform.Find("CloseButton").GetComponent<UIButton>();
        CloseButton.onClick.Add(new EventDelegate(CloseClick));
        EventDelegate.Add(MatNum.onChange, BuyMatNumChangeClick);//数字控制进度条

        
        }
    private void Update()
    {       
       
        if(float.Parse(MatNum.value)>99)
        {
            MatNum.value = 99.ToString();
        }
        if(float.Parse(MatNum.value)<0)
        {
            MatNum.value = 0.ToString();
        }
        if(BuyNumSlider.value>1)
        {
            BuyNumSlider.value = 1;
        }
        if(BuyNumSlider.value<0)
        {
            BuyNumSlider.value = 0;
        }
        //数字控制进度条
        //BuyNumSliderlider.value= float.Parse(MatNum.value) / 99;
        //MatNum.value = (BuyNumSliderlider.value * 99).ToString();
        PageNum.text = CurrentPage + "/" + ItemsPage;
      
 


    }
    /// <summary>
    /// 进度条控制数字
    /// </summary>
    
    public void SliderOnClick()
    {
       
        MatNum.value = (BuyNumSlider.value * 99).ToString();

        //BuyNumSliderlider.value = float.Parse(MatNum.value) / 99;
    }
    /// <summary>
    /// 数字控制进度条
    /// </summary>
    public void BuyMatNumChangeClick()
    {
        if (MatNum.value == "")
        {
            Debug.Log("为空");
            MatNum.value = 0.ToString();
        }
        if (int.Parse(MatNum.value) * buyprice >= Inventory._instance.Coins)
        {
            Debug.Log("超出购买范围");
            MatNum.value = (Inventory._instance.Coins / buyprice).ToString();
        }
        else
        {
            Debug.Log("未超出");
        }

        BuyNumSlider.value= float.Parse(MatNum.value) / 99; // MatNum.value不够精确  Slider拖动条很飘
    }
    public void SellMatNumChangeClick()
    {
        if(MatNum.value=="")
        {
            Debug.Log("为空");
            MatNum.value = 0.ToString();
        }
        InventoryGrid[] inventorylist = GameObject.FindObjectsOfType<InventoryGrid>();
        foreach(InventoryGrid grid in inventorylist)
        {
            if(grid.id==BuyId)
            {
                MatNum.value = grid.num.ToString();
            }
        }
    }
    /// <summary>
    /// 金币消费
    /// </summary>
    /// <param name="buyprice"></param>
    /// <param name="buyNum"></param>
    public void ConSumeCoin(int buyprice,int buyNum)
    {
        int CoinCousume = buyprice * buyNum;
        if(buyNum==0)
        {
            return;
        }
        if(Inventory._instance.ReduceCoin(CoinCousume))
        {
            Inventory._instance.ObjectTake(BuyId,buyNum);
            Debug.Log("消费成功");
        }
        else
        {
            Debug.Log("消费失败");
        }

    }
    /// <summary>
    /// 金币增加
    /// </summary>
    /// <param name="sellprice"></param>
    /// <param name="buyNum"></param>
    public void IncreaseCoin(int sellprice,int buyNum)
    {
        int Coinincrease = sellprice * buyNum;//金币增加量
        Inventory._instance.Coins += Coinincrease;
        InventoryGrid[] inventorylist = GameObject.FindObjectsOfType<InventoryGrid>();
        foreach (InventoryGrid grid in inventorylist)
        {
            if (grid.id == BuyId)
            {
                grid.MinObject(buyNum); //金币增加 物品减少
            }
        }



    }
    /// <summary>
    /// 购买确定按钮
    /// </summary>
    public void BuyOKClick()
    {
        Objectinfomation info = ObjectInfo._instance.GetInfoByID(BuyId);
        buyprice = info.price_buy;
        buyNum = int.Parse(Inform.transform.Find("NumInput").GetComponent<UIInput>().value);
        ConSumeCoin(buyprice, buyNum);
        CloseClick();
        
    }
    /// <summary>
    /// 卖出消费按钮
    /// </summary>
    public void SellOKClick()
    {
        Objectinfomation info = ObjectInfo._instance.GetInfoByID(BuyId);
        buySell = info.price_sell;
        buyNum = int.Parse(Inform.transform.Find("NumInput").GetComponent<UIInput>().value);
        IncreaseCoin(buySell, buyNum);
        CloseClick();

        
    }
    /// <summary>
    /// 关闭窗口按钮
    /// </summary>
    public void CloseClick()
    {
       Inform.gameObject.SetActive(false);
    }
    
    public void SellOrBuySilder()
    {
        if(IsBuy)
        {
            EventDelegate.Add(MatNum.onChange, BuyMatNumChangeClick);//数字控制进度条
            
        }
        else
        {
            EventDelegate.Add(MatNum.onChange, SellMatNumChangeClick);//数字控制进度条
        }
    }
 /// <summary>
 /// 下一页
 /// </summary>
    public void NextPage()
    {
        CurrentPage++;
        Debug.Log("当前页"+CurrentPage);
        PageHideShow();
        if (CurrentPage==ItemsPage)//最后一页
        {
            Debug.Log("最后一页");
            int LastPageId = (CurrentPage - 1) * 3;
            foreach(GameObject items in MatGrid)
            {
                items.GetComponentInChildren<ShopItems>().id = 0;
                items.GetComponentInChildren<ShopItems>().NameLabel.text = "";
                items.GetComponentInChildren<ShopItems>().PriceLabel.text = "";
                items.GetComponentInChildren<ShopItems>().EffectLabel.text = "";
                items.GetComponentInChildren<ShopItems>().icon.spriteName = "LabelTag";
                items.GetComponentInChildren<ShopItems>().SetPosition();    
                             
            }
            for(int i=LastPageId;i<Items.Length;i++)
            {
               
                Objectinfomation info = null;
                info = ObjectInfo._instance.GetInfoByID(Items[i]);
            
                MatGrid[i % 3].GetComponentInChildren<ShopItems>().id = info.id;
                MatGrid[i % 3].GetComponentInChildren<ShopItems>().NameLabel.text = info.objectname;
                MatGrid[i % 3].GetComponentInChildren<ShopItems>().PriceLabel.text = info.price_buy.ToString();
                MatGrid[i % 3].GetComponentInChildren<ShopItems>().EffectLabel.text = info.price_sell.ToString();//介绍暂无 用卖的价格代替
                MatGrid[i % 3].GetComponentInChildren<ShopItems>().icon.spriteName = info.icon;
                MatGrid[i % 3].GetComponentInChildren<ShopItems>().SetPosition();
            }
            

        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                Objectinfomation info = null;
                info = ObjectInfo._instance.GetInfoByID(Items[(CurrentPage-1)*3+i]);
                MatGrid[i].GetComponentInChildren<ShopItems>().id = info.id;
                MatGrid[i].GetComponentInChildren<ShopItems>().NameLabel.text = info.objectname;
                MatGrid[i].GetComponentInChildren<ShopItems>().PriceLabel.text = info.price_buy.ToString();
                MatGrid[i].GetComponentInChildren<ShopItems>().EffectLabel.text = info.price_sell.ToString();//介绍暂无 用卖的价格代替
                MatGrid[i].GetComponentInChildren<ShopItems>().icon.spriteName = info.icon;
                MatGrid[i].GetComponentInChildren<ShopItems>().SetPosition();
            }
            //}1  0 1 2    CurrentPage1
            // 2  3 4 5    CurrentPage2
            // 3  6 7 8    CurrentPage3
            // 4  9 10 11  CurrentPage4
            // 5  12 13 14 CurrentPage5  (CurrentPage-1)*3
        }
    }
    /// <summary>
    /// 箭头的隐藏和显示
    /// </summary>
    public void PageHideShow()
    {
        if(CurrentPage==1)
        {
            PreButton.gameObject.SetActive(false);
        }
        else
        {
            PreButton.gameObject.SetActive(true);
        }
        if(CurrentPage==ItemsPage)
        {
            NextButton.gameObject.SetActive(false);
        }
        else
        {
           
            NextButton.gameObject.SetActive(true);
        }

    }
    /// <summary>
    /// 上一页
    /// </summary>
    public void PrePage()
    {
        
        CurrentPage--;
    
        Debug.Log("当前页"+CurrentPage);
        PageHideShow();

            for (int i = 0; i < 3; i++)
            {
                Objectinfomation info = null;
                info = ObjectInfo._instance.GetInfoByID(Items[(CurrentPage - 1) * 3 + i]);
                MatGrid[i].GetComponentInChildren<ShopItems>().id = info.id;
                MatGrid[i].GetComponentInChildren<ShopItems>().NameLabel.text = info.objectname;
                MatGrid[i].GetComponentInChildren<ShopItems>().PriceLabel.text = info.price_buy.ToString();
                MatGrid[i].GetComponentInChildren<ShopItems>().EffectLabel.text = info.price_sell.ToString();//介绍暂无 用卖的价格代替
                MatGrid[i].GetComponentInChildren<ShopItems>().icon.spriteName = info.icon;
                MatGrid[i].GetComponentInChildren<ShopItems>().SetPosition();
            }
        
    }

   





}
   
	


