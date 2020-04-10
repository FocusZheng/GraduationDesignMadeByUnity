using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipMent : MonoBehaviour {

    public static EquipMent _instance;
    public EquipGrid Headgear;
    public EquipGrid Right_Hand;
    public EquipGrid Armor;
    public EquipGrid Left_Hand;
    public EquipGrid Accessory;
    public EquipGrid Shoe;
    public TweenPosition EquipTween;

	// Use this for initialization
	void Awake () {
        EquipTween = this.GetComponent<TweenPosition>();
        _instance = this;
        Headgear.EquipType = Objectinfomation.DressType.Headgear;
        Right_Hand.EquipType = Objectinfomation.DressType.RightHand;
        Armor.EquipType = Objectinfomation.DressType.Armor;
        Left_Hand.EquipType = Objectinfomation.DressType.LeftHand;
        Accessory.EquipType = Objectinfomation.DressType.Accessory;
        Shoe.EquipType = Objectinfomation.DressType.Shoe;
        
    }   
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ShowEquip()
    {
        EquipTween.enabled = true;
        EquipTween.PlayForward();
    }
    public void HideEquip()
    {
        EquipTween.PlayReverse();
    }
    //public void EquipChange(EquipGrid Equip)
    //{

    //}
}
