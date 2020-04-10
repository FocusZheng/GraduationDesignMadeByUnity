using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : UIDragDropItem {
    [HideInInspector]
    public UISprite sprite;

    public int id = 0;
    public bool isHover = false;
    public EquipMentItem EquipMentItems;
    public Objectinfomation.ObjectType objectType;
    public GameObject quickItem;
    private void Awake()
    {
        base.Awake();
        sprite = this.GetComponent<UISprite>();
       
        




    }
    private void Start()
    {
        base.Start();
        ObjectInfoSetting();
    }
    private void Update()
    {
        base.Update();

        if (isHover)
        {

            if (Input.GetMouseButtonDown(1))
            {
                InventoryGrid grid = this.transform.parent.GetComponent<InventoryGrid>();
                Objectinfomation info = ObjectInfo._instance.GetInfoByID(this.id);
                switch (objectType)//枚举类型还没更改
                {
                    case Objectinfomation.ObjectType.Drug:
                        Debug.Log("使用药水");//药品成功使用
                        
                        break;
                    case Objectinfomation.ObjectType.Equip:
                        Debug.Log("穿戴装备");//装备穿戴
                        
                        DressManType(info, grid);
                        break;
                    case Objectinfomation.ObjectType.Mat:
                        
                        Debug.Log("材料无法使用");
                        break;


                }


            }
            //IntroDuctionInfo._instance.InfoTextShow();

        }
    }
    /// <summary>
    /// 将装备类型设置好
    /// </summary>
    private void ObjectInfoSetting()
    {
        Objectinfomation info = ObjectInfo._instance.GetInfoByID(this.id);
        switch(info.type)
        {
            case "Drug":               
                objectType = Objectinfomation.ObjectType.Drug;
                break;
            case "Equip":             
                objectType = Objectinfomation.ObjectType.Equip;             
                break;
            case "Mat":
                objectType = Objectinfomation.ObjectType.Mat;
                break;
        }
        Debug.Log("物品类型"+objectType);

    }
  
    private void DressManType(Objectinfomation info, InventoryGrid grid)
    {
        switch (info.DressManType)
        {
            case Objectinfomation.ApplicationType.Swordman:
                if (GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerInfomation>().herotype == PlayerInfomation.HeroType.Swordman)
                {

                    //进入装备位置类型的判断                         
                    grid.MinObject();
                    EquipMentType(info);

                }
                break;
            case Objectinfomation.ApplicationType.Magician:
                if (GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerInfomation>().herotype == PlayerInfomation.HeroType.Magician)
                {
                    grid.MinObject();
                    //进入装备位置类型的判断
                    EquipMentType(info);
                }
                break;
            case Objectinfomation.ApplicationType.Common:
                //进入装备位置类型的判断
                grid.MinObject();
                EquipMentType(info);
                break;
        }
    }
    #region 装备部位判断
    private void EquipMentType(Objectinfomation info)
    {
        EquipMentItems.GetComponent<EquipMentItem>().id = info.id;
        switch (info.EquipType)
        {

            case Objectinfomation.DressType.Accessory:
                if (EquipMent._instance.Accessory.id == 0)
                {
                    Debug.Log("穿戴成功");
                    EquipMent._instance.Accessory.id = info.id;
                    NGUITools.AddChild(EquipMent._instance.Accessory.gameObject, EquipMentItems.gameObject);
                    StatusContorl._intance.UpdateProperty(); //该菜单隐藏的时候无法取到
                    StatusContorl._intance.EquipChangeUpdateShow();
                }
                else
                {
                    Debug.Log("更换成功");
                    Destroy(EquipMent._instance.Accessory.GetComponentInChildren<EquipMentItem>().gameObject);//删除掉旧的装备


                    Inventory._instance.ObjectTake(EquipMent._instance.Accessory.id);//背包里增加一个
                                                                                     //把原来格子的id清0
                    EquipMent._instance.Accessory.id = info.id;//原来格子的ID更新
                    NGUITools.AddChild(EquipMent._instance.Accessory.gameObject, EquipMentItems.gameObject);

                    StatusContorl._intance.UpdateProperty();
                    StatusContorl._intance.EquipChangeUpdateShow();

                }

                break;
            case Objectinfomation.DressType.Armor:
                if (EquipMent._instance.Armor.id == 0)
                {
                    Debug.Log("穿戴成功");
                    EquipMent._instance.Armor.id = info.id;
                    NGUITools.AddChild(EquipMent._instance.Armor.gameObject, EquipMentItems.gameObject);
                    StatusContorl._intance.UpdateProperty(); //该菜单隐藏的时候无法取到
                    StatusContorl._intance.EquipChangeUpdateShow();
                }
                else
                {
                    Debug.Log("更换成功");
                    Destroy(EquipMent._instance.Armor.GetComponentInChildren<EquipMentItem>().gameObject);//删除掉旧的装备


                    Inventory._instance.ObjectTake(EquipMent._instance.Armor.id);//背包里增加一个
                                                                                 //把原来格子的id清0
                    EquipMent._instance.Armor.id = info.id;//原来格子的ID更新
                    NGUITools.AddChild(EquipMent._instance.Armor.gameObject, EquipMentItems.gameObject);

                    StatusContorl._intance.UpdateProperty();
                    StatusContorl._intance.EquipChangeUpdateShow();

                }
                break;
            case Objectinfomation.DressType.Headgear:
                if (EquipMent._instance.Headgear.id == 0)
                {
                    Debug.Log("穿戴成功");
                    EquipMent._instance.Headgear.id = info.id;
                    NGUITools.AddChild(EquipMent._instance.Headgear.gameObject, EquipMentItems.gameObject);
                    StatusContorl._intance.UpdateProperty(); //该菜单隐藏的时候无法取到
                    StatusContorl._intance.EquipChangeUpdateShow();
                }
                else
                {
                    Debug.Log("更换成功");
                    Destroy(EquipMent._instance.Headgear.GetComponentInChildren<EquipMentItem>().gameObject);//删除掉旧的装备


                    Inventory._instance.ObjectTake(EquipMent._instance.Headgear.id);//背包里增加一个
                                                                                    //把原来格子的id清0
                    EquipMent._instance.Headgear.id = info.id;//原来格子的ID更新
                    NGUITools.AddChild(EquipMent._instance.Headgear.gameObject, EquipMentItems.gameObject);

                    StatusContorl._intance.UpdateProperty();
                    StatusContorl._intance.EquipChangeUpdateShow();

                }
                break;
            case Objectinfomation.DressType.Shoe:
                if (EquipMent._instance.Shoe.id == 0)
                {
                    Debug.Log("穿戴成功");
                    EquipMent._instance.Shoe.id = info.id;
                    NGUITools.AddChild(EquipMent._instance.Shoe.gameObject, EquipMentItems.gameObject);
                    StatusContorl._intance.UpdateProperty(); //该菜单隐藏的时候无法取到
                    StatusContorl._intance.EquipChangeUpdateShow();
                }
                else
                {
                    Debug.Log("更换成功");
                    Destroy(EquipMent._instance.Shoe.GetComponentInChildren<EquipMentItem>().gameObject);//删除掉旧的装备


                    Inventory._instance.ObjectTake(EquipMent._instance.Shoe.id);//背包里增加一个
                                                                                //把原来格子的id清0
                    EquipMent._instance.Shoe.id = info.id;//原来格子的ID更新
                    NGUITools.AddChild(EquipMent._instance.Shoe.gameObject, EquipMentItems.gameObject);

                    StatusContorl._intance.UpdateProperty();
                    StatusContorl._intance.EquipChangeUpdateShow();

                }
                break;
            case Objectinfomation.DressType.RightHand:

                if (EquipMent._instance.Right_Hand.id == 0)
                {
                    Debug.Log("穿戴成功");
                    EquipMent._instance.Right_Hand.id = info.id;
                    NGUITools.AddChild(EquipMent._instance.Right_Hand.gameObject, EquipMentItems.gameObject);
                    StatusContorl._intance.UpdateProperty(); //该菜单隐藏的时候无法取到
                    StatusContorl._intance.EquipChangeUpdateShow();
                }
                else
                {
                    Debug.Log("更换成功");
                    Destroy(EquipMent._instance.Right_Hand.GetComponentInChildren<EquipMentItem>().gameObject);//删除掉旧的装备


                    Inventory._instance.ObjectTake(EquipMent._instance.Right_Hand.id);//背包里增加一个
                                                                                      //把原来格子的id清0
                    EquipMent._instance.Right_Hand.id = info.id;//原来格子的ID更新
                    NGUITools.AddChild(EquipMent._instance.Right_Hand.gameObject, EquipMentItems.gameObject);

                    StatusContorl._intance.UpdateProperty();
                    StatusContorl._intance.EquipChangeUpdateShow();

                }
                break;
            case Objectinfomation.DressType.LeftHand:
                if (EquipMent._instance.Left_Hand.id == 0)
                {
                    Debug.Log("穿戴成功");
                    EquipMent._instance.Left_Hand.id = info.id;
                    NGUITools.AddChild(EquipMent._instance.Left_Hand.gameObject, EquipMentItems.gameObject);
                    StatusContorl._intance.UpdateProperty(); //该菜单隐藏的时候无法取到
                    StatusContorl._intance.EquipChangeUpdateShow();
                }
                else
                {
                    Debug.Log("更换成功");
                    Destroy(EquipMent._instance.Left_Hand.GetComponentInChildren<EquipMentItem>().gameObject);//删除掉旧的装备


                    Inventory._instance.ObjectTake(EquipMent._instance.Left_Hand.id);//背包里增加一个
                                                                                     //把原来格子的id清0
                    EquipMent._instance.Left_Hand.id = info.id;//原来格子的ID更新
                    NGUITools.AddChild(EquipMent._instance.Left_Hand.gameObject, EquipMentItems.gameObject);

                    StatusContorl._intance.UpdateProperty();
                    StatusContorl._intance.EquipChangeUpdateShow();

                }
                break;
        }

    }
    #endregion 

    public void OnHoverOver()
    {

        isHover = true;
        //transform.parent.GetComponentInChildren<IntroDuctionInfo>().gameObject.SetActive(true);
        transform.parent.GetChild(1).GetComponent<IntroDuctionInfo>().isHover = true;
        transform.parent.GetChild(1).gameObject.SetActive(true);


    }
    public void OnHoverExit()
    {

        isHover = false;
        transform.parent.GetChild(1).GetComponent<IntroDuctionInfo>().isHover = false;
        transform.parent.GetChild(1).gameObject.SetActive(false);

    }

    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);
        if (surface != null)
        {
            Debug.Log(surface);
            if (surface.tag == Tags.inventoryGrid) //如果是背包格子
            {
                if (surface == this.transform.parent.gameObject)//自己的格子
                {
                    RestPostion();
                }
                else
                {
                    transform.parent.GetChild(1).gameObject.SetActive(false);
                    transform.parent.GetChild(1).GetComponent<IntroDuctionInfo>().UpdateIntro();
                    InventoryGrid oldParent = this.transform.parent.GetComponent<InventoryGrid>(); //拖到空的格子
                    this.transform.parent = surface.transform;//把新的格子的父亲传过来
                    RestPostion();
                    InventoryGrid newParent = surface.GetComponent<InventoryGrid>();
                    newParent.ObjectShowInBag(oldParent.id, oldParent.num); //把旧的格子的物品信息传过来
                    oldParent.ClearInfo();//旧的格子物品信息删除



                }
            }
            else if (surface.tag == Tags.inventoryItem)//背包物品
            {
                InventoryGrid grid1 = this.transform.parent.GetComponent<InventoryGrid>();
                InventoryGrid grid2 = surface.transform.parent.GetComponent<InventoryGrid>(); //surface 是物品 要取到格子 parent
                int id = grid1.id;
                int num = grid1.num;
                grid1.ObjectShowInBag(grid2.id, grid2.num); //两个格子信息交换 在背包里重新生成
                grid2.ObjectShowInBag(id, num);
                RestPostion();

            }
            else if (surface.tag == Tags.quickGrid)//空的快捷栏格子
            {
                if(objectType==Objectinfomation.ObjectType.Drug)//药品才能放入快捷栏
                {
                    QuickGrid[] quickGrids = FindObjectsOfType<QuickGrid>(); 
                    foreach (QuickGrid grid in quickGrids)
                    {
                        if (grid.id == this.id)
                        {
                            Destroy(grid.GetComponentInChildren<QuickItem>().gameObject);
                            grid.id = 0;
                        }
                    }

                    surface.GetComponent<QuickGrid>().quickType = QuickGrid.QuickGridType.Drug;
                    surface.GetComponent<QuickGrid>().id = this.id;
                    quickItem.GetComponent<QuickItem>().Quickitem_Type = QuickItem.QuickItemType.Drug;
                    quickItem.GetComponent<QuickItem>().id = this.id;
                    quickItem.GetComponent<QuickItem>().SetID();
                    NGUITools.AddChild(surface, quickItem);                  
                    RestPostion();

                    
                    

                }
                else
                {
                    RestPostion();
                }
                
            }
            else if(surface.tag==Tags.quickItem)//如果快捷栏上已经有药品
            {
                if(surface.GetComponent<QuickItem>().id==this.id)//如果快捷栏上的ID和物品相同
                {
                    RestPostion();
                }
                else
                {
                    Debug.Log("上面已有快捷方式");
                    QuickGrid[] quickGrids = FindObjectsOfType<QuickGrid>();
                    foreach(QuickGrid grid in quickGrids)
                    {
                        if(grid.id==this.id)
                        {
                            Destroy(grid.GetComponentInChildren<QuickItem>().gameObject);
                            grid.id = 0;
                        }
                    }
                    
                    quickItem.GetComponent<QuickItem>().id = this.id;
                    quickItem.GetComponent<QuickItem>().Quickitem_Type = QuickItem.QuickItemType.Drug;
                    quickItem.GetComponent<QuickItem>().SetID();
                    NGUITools.AddChild(surface.transform.parent.gameObject,quickItem);
                    Destroy(surface);                   
                    surface.GetComponentInParent<QuickGrid>().id = this.id;
                    surface.GetComponentInParent<QuickGrid>().quickType = QuickGrid.QuickGridType.Skill;
                    RestPostion();
                }
            }
            else
            {
                RestPostion();
            }

        }


    }
    void RestPostion()
    {
        transform.localPosition = Vector3.zero;
    }

    public void SetId(int id)
    {
        Objectinfomation info = ObjectInfo._instance.GetInfoByID(id);
        sprite.spriteName = info.icon;     
        Debug.Log("设置完成");
    }
    public void SetIconName(string icon_name)
    {
        sprite.spriteName = icon_name;

    }
}