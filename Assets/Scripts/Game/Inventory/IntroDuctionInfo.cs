using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroDuctionInfo : MonoBehaviour
{

    public GameObject Introduction;
    private UILabel label;
    //public static IntroDuctionInfo _instance;
    private float timer = 0;
    public bool isHover = false;


    private void Awake()
    {
        //_instance = this;
        Introduction = this.gameObject;
        label = Introduction.GetComponentInChildren<UILabel>();
        this.gameObject.SetActive(false);
       
    }
    private void Update()
    {
       
        if (isHover)
        {
            InfoTextShow();
        }
        else
        {
            this.gameObject.SetActive(false);
        }
        
        //if (this.gameObject.activeInHierarchy==true)
        //{
        //    Debug.Log("1秒后关闭");
        //    timer += Time.deltaTime;
        //    if(timer>1)
        //    {
        //        this.gameObject.SetActive(false);
        //        timer = 0;
        //    }
        //}
        


    }
    public void InfoTextShow()
    {
        this.gameObject.SetActive(true);
        label.text = GetObjInfoShow();
        
      
    }
    public void UpdateIntro()
    {
        label.text = "";
    }
    
  

    string GetObjInfoShow()
    {
        
        string InfoItemText = "";
        InventoryGrid grid = this.transform.parent.GetComponent<InventoryGrid>();
        Objectinfomation info = ObjectInfo._instance.GetInfoByID(grid.id);
        if (info.Objtype == Objectinfomation.ObjectType.Drug)
        {

            InfoItemText += "名称:" + info.objectname + "\n";
            InfoItemText += "功能:HP+" + info.hpAdd + "\n";
            InfoItemText += "出售价:" + info.price_sell + "\n";

            return InfoItemText;
        }
        else if (info.Objtype==Objectinfomation.ObjectType.Equip)
        {
            string InfoItemText1 = "";
            string InfoItemText2 = "";
            switch (info.EquipType)
            {

                case Objectinfomation.DressType.Accessory:

                    InfoItemText1 += "名称:" + info.objectname + "\n";
                    InfoItemText1 += "穿戴部位:项链"  + "\n";
                    InfoItemText1 += "伤害值:+" + info.attack + "\n";
                    InfoItemText1 += "防御值:+" + info.defenese + "\n";
                    InfoItemText1 += "速度值+" + info.speed + "\n";
                    break;
                case Objectinfomation.DressType.RightHand:
                    InfoItemText1 = "";
                    InfoItemText1 += "名称:" + info.objectname + "\n";
                    InfoItemText1 += "穿戴部位:右手武器"  + "\n";
                    InfoItemText1 += "伤害值:+" + info.attack + "\n";
                    InfoItemText1 += "防御值:+" + info.defenese + "\n";
                    InfoItemText1 += "速度值+" + info.speed + "\n";
                    break;
                case Objectinfomation.DressType.Armor:
                    InfoItemText1 = "";
                    InfoItemText1 += "名称:" + info.objectname + "\n";
                    InfoItemText1 += "穿戴部位:盔甲" + "\n";
                    InfoItemText1 += "伤害值:+" + info.attack + "\n";
                    InfoItemText1 += "防御值:+" + info.defenese + "\n";
                    InfoItemText1 += "速度值+" + info.speed + "\n";
                    break;
                case Objectinfomation.DressType.LeftHand:
                    InfoItemText1 = "";
                    InfoItemText1 += "名称:" + info.objectname + "\n";
                    InfoItemText1 += "穿戴部位:左手副武器" + "\n";
                    InfoItemText1 += "伤害值:+" + info.attack + "\n";
                    InfoItemText1 += "防御值:+" + info.defenese + "\n";
                    InfoItemText1 += "速度值+" + info.speed + "\n";
                    break;
                case Objectinfomation.DressType.Headgear:
                    InfoItemText1 = "";
                    InfoItemText1 += "名称:" + info.objectname + "\n";
                    InfoItemText1 += "穿戴部位:帽子"  + "\n";
                    InfoItemText1 += "伤害值:+" + info.attack + "\n";
                    InfoItemText1 += "防御值:+" + info.defenese + "\n";
                    InfoItemText1 += "速度值+" + info.speed + "\n";
                    break;
                case Objectinfomation.DressType.Shoe:
                    InfoItemText1 = "";
                    InfoItemText1 += "名称:" + info.objectname + "\n";
                    InfoItemText1 += "穿戴部位:鞋子"  + "\n";
                    InfoItemText1 += "伤害值:+" + info.attack + "\n";
                    InfoItemText1 += "防御值:+" + info.defenese + "\n";
                    InfoItemText1 += "速度值+" + info.speed + "\n";
                    break;

            }
            switch (info.DressManType)
            {
                case Objectinfomation.ApplicationType.Swordman:

                    InfoItemText2 = "适用类型:剑士"  ;
                    break;
                case Objectinfomation.ApplicationType.Magician:
                    InfoItemText2 = "";
                    InfoItemText2 = "适用类型:魔法师"  ;
                    break;
                case Objectinfomation.ApplicationType.Common:
                    InfoItemText2 = "";
                    InfoItemText2 = "适用类型:通用"  ;
                    break;
            }
            InfoItemText = InfoItemText1 + InfoItemText2;


            return InfoItemText;
        }
        return "";




    }
}
