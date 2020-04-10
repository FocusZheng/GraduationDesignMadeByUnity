using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickItem : MonoBehaviour {

    public int id;
    private UISprite icon;
    public QuickItemType Quickitem_Type=QuickItemType.Skill;
    private UISprite ColdTime;
    // Use this for initialization
    private void Awake()
    {
        
    }
    void Start () {
     
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetID()
    {
        if(Quickitem_Type==QuickItemType.Drug)
        {
            Objectinfomation info = null;
            info=ObjectInfo._instance.GetInfoByID(this.id);
            icon = this.GetComponent<UISprite>();
            icon.spriteName = info.icon;
            

          
        }
        else if(Quickitem_Type==QuickItemType.Skill)
        {
            SkillInfomation info = null;
            info=SkillInfo._instance.GetSkillInfoByID(this.id);
            icon = this.GetComponent<UISprite>();
            icon.spriteName = info.icon_name;
           



        }
    }
    public enum QuickItemType
    {
        Drug,
        Skill
    }

}
