using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipMentItem : MonoBehaviour {

    public int id = 0;
    public Objectinfomation.DressType EquipType;
    public Objectinfomation.ApplicationType DressManType;
    private UISprite sprite;
    private int attack;
    private int Defenece;
    private int speed;
    private string EquipName;
    private bool isHover=false;
	// Use this for initialization
	void Start () {
        Objectinfomation info = ObjectInfo._instance.GetInfoByID(this.id);
        sprite = this.GetComponent<UISprite>();
        sprite.spriteName = info.icon;
        EquipType = info.EquipType;
        DressManType = info.DressManType;
        attack = info.attack;
        Defenece = info.defenese;
        speed = info.speed;
        EquipName = info.objectname;
        
	}
	
	// Update is called once per frame
	void Update () {
		if(isHover)
        {
            if(Input.GetMouseButtonDown(1))
            {
                Inventory._instance.ObjectTake(this.id);
                this.GetComponentInParent<EquipGrid>().Clearinfo();
                Destroy(this.gameObject);
                StatusContorl._intance.UpdateProperty();
                StatusContorl._intance.EquipChangeUpdateShow();
               //Destroy后脚本还是会继续调用执行下去
            }
            
        }
	}
    public void IsHoverOn()
    {
        isHover = true;
    }
    public void IsHoverExit()
    {
        isHover = false;
    }

}
