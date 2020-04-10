using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectinfomation : MonoBehaviour
{

    public int id;
    public int hpAdd;
    public int mpAdd;
    public int price_sell;
    public int price_buy;


    public string objectname;
    public string icon;
    /// <summary>
    /// 物品类型 药品装备材料
    /// </summary>
      public string type;

    /// <summary>
    /// 装备穿戴部位 手腕 头盔
    /// </summary>
     public string DressPosition;

    /// <summary>
    /// //装备适用类型 职业
    /// </summary>
    public string DressPlayer;

    public int attack;
    public int defenese;
    public int speed;

    public enum ObjectType
    {
        Drug,
        Equip,
        Mat
    }
    public enum ApplicationType
    {
        Swordman,//剑士
        Magician,//魔法师
        Common//通用
    }
    public enum DressType
    {
        Headgear,//
        Armor,
        RightHand,
        LeftHand,
        Shoe,
        Accessory

    }
    public ObjectType Objtype;
    public ApplicationType DressManType;
    public DressType EquipType;



}
