using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopItems : MonoBehaviour {

    public int id;
    public UILabel NameLabel;
    public UILabel PriceLabel;
    public UILabel EffectLabel;
    public UISprite icon;
    public UIButton Buy;
    public UIButton Sell;
    private Objectinfomation info=null;
    // Use this for initialization
    private void Awake()
    {
        NameLabel = transform.Find("NameLabel").GetComponent<UILabel>();
        PriceLabel = transform.Find("PriceLabel").GetComponent<UILabel>();
        EffectLabel = transform.Find("EffectLabel").GetComponent<UILabel>();
        icon = transform.Find("Icon").GetComponent<UISprite>();
        Buy = transform.Find("Buy").GetComponent<UIButton>();
        Sell = transform.Find("Sell").GetComponent<UIButton>();
        
        
    }
    private void Start()
    {
        Buy.onClick.Add(new EventDelegate(BuyClick));//ButtonOnClick绑定
        Sell.onClick.Add(new EventDelegate(SellClick));
    }

    // Update is called once per frame
    void Update () {
		
	}
    public void SetPosition()
    {
        this.transform.localPosition = new Vector3(-150,0,0);
    }
    public void BuyClick()
    {
        ShopUI._instance.Inform.transform.Find("BuyLabel").GetComponent<UILabel>().text = "你想购买多少?";
       
        ShopUI._instance.Inform.SetActive(true);
        ShopUI._instance.BuyId = id;
        info = ObjectInfo._instance.GetInfoByID(id);
        ShopUI._instance.buyprice = info.price_buy;
        ShopUI._instance.IsBuy = true;


    }
    public void SellClick()
    {
        
        ShopUI._instance.Inform.transform.Find("BuyLabel").GetComponent<UILabel>().text = "你想卖出多少?";  
        ShopUI._instance.Inform.SetActive(true);
        ShopUI._instance.BuyId = id;
        info = ObjectInfo._instance.GetInfoByID(id);
        ShopUI._instance.buyprice = info.price_buy;
        ShopUI._instance.IsBuy = false;
    }
}
