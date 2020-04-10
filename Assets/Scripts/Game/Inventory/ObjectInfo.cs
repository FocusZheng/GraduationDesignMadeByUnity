using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo : MonoBehaviour {

    public static ObjectInfo _instance;

    public TextAsset ObjectInfoList;
    private Dictionary<int, Objectinfomation> Objectdic= new Dictionary<int, Objectinfomation>();
  


    private void Awake()
    {
        _instance = this;
        ReadInfo();
        print("物品信息读取");
    

    }
    public Objectinfomation GetInfoByID(int id) //
    {
        Objectinfomation info = null;//先把info取空
        Objectdic.TryGetValue(id, out info);//取得对应id的物品信息
        return info;


    }


   void ReadInfo()
    {
       
        string ObjectStr = ObjectInfoList.text;       
        string[] ObjectEveryStr = ObjectStr.Split('\n');
        foreach (string Oinfo in ObjectEveryStr)
        {
            Objectinfomation info=new Objectinfomation();
            string[] objectinfo = Oinfo.Split(',');
            info.id = int.Parse(objectinfo[0]);
           
            info.objectname = objectinfo[1];     
            info.icon = objectinfo[2];
            info.type = objectinfo[3];
     
            switch (objectinfo[3])
            {

                case "Drug": info.Objtype = Objectinfomation.ObjectType.Drug;
                           
                    info.hpAdd = int.Parse(objectinfo[4]);
                    info.mpAdd = int.Parse(objectinfo[5]);
                    info.price_sell = int.Parse(objectinfo[6]);
                    info.price_buy = int.Parse(objectinfo[7]);
                    break;
                case "Equip": info.Objtype=Objectinfomation.ObjectType.Equip;
                    
                    info.attack = int.Parse(objectinfo[4]);
                    info.defenese = int.Parse(objectinfo[5]);
                    info.speed = int.Parse(objectinfo[6]);
                    info.DressPosition = objectinfo[7];
                    info.DressPlayer= objectinfo[8];
                    info.price_sell = int.Parse(objectinfo[9]);
                    info.price_buy = int.Parse(objectinfo[10]);
                    break;
                case "Mat": info.Objtype=Objectinfomation.ObjectType.Mat;
                            
                    info.price_sell = int.Parse(objectinfo[4]);
                    info.price_buy = int.Parse(objectinfo[5]);
                    break;
            }           
            switch (info.DressPosition)
            {
                case "Headgear":info.EquipType = Objectinfomation.DressType.Headgear; break;
                case "Armor": info.EquipType = Objectinfomation.DressType.Armor; break;
                case "LeftHand": info.EquipType = Objectinfomation.DressType.LeftHand; break;
                case "RightHand": info.EquipType = Objectinfomation.DressType.RightHand; break;
                case "Shoe": info.EquipType = Objectinfomation.DressType.Shoe; break;
                case "Accessory": info.EquipType = Objectinfomation.DressType.Accessory; break;
            }
            
            switch (info.DressPlayer)
            {
                case "Swordman":info.DressManType = Objectinfomation.ApplicationType.Swordman;break;
                case "Magician": info.DressManType = Objectinfomation.ApplicationType.Magician; break;
                case "Common": info.DressManType = Objectinfomation.ApplicationType.Common;break;
            }  
            Objectdic.Add(info.id, info);
            //Debug.Log(info.id);
            //Debug.Log(info.objectname);
            //Debug.Log(info.icon);
            //Debug.Log(info.type);
            //Debug.Log(info.Dress);
            //Debug.Log(info.attack);
            
            
        }
    }

}

