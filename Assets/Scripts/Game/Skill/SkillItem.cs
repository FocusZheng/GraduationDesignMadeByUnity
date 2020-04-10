using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItem : UIDragDropItem {

    public int id = 0;
    private SkillInfomation info=null;
    private UISprite Skill_Sprite;
    public UISprite Line;
    public GameObject unlockid;
    public int needPoint;
    public bool islearned=false;
    private string Skill_Intro;
    private bool iscanLearned=false;
    public GameObject quickItem;
    public bool isHover=false;
    
    
    private void Start()
    {
        base.Start();
        info = SkillInfo._instance.GetSkillInfoByID(id);
        needPoint = info.NeedPoint;
        Skill_Sprite = this.GetComponent<UISprite>();
   
        SkillInfoUpdate();
        
    }


    public void SkillItemClick()
    {
        iscanLearned = IsUnlockedLearn();
        if (SkillUI._instance.SkillPointCosume(this.id)==true && islearned == false &&iscanLearned==true)
        {
            islearned = true;           
            Debug.Log("学习成功");
            SkillUI._instance.SkillPoint -=needPoint;
            SkillUI._instance.SkillPointUpdate();
            transform.GetChild(0).gameObject.SetActive(false);
           
        }
        else
        {
            Debug.Log("学习失败");
        }
    }
    bool IsUnlockedLearn()
    {
        if(this.unlockid==null)
        {
            return true;
        }
        if(unlockid.GetComponent<SkillItem>().islearned)//如果前置技能已经学了
        {
            iscanLearned = true;
            return true;
        }
        else
        {
            Debug.Log("缺少前置技能");
            return false;
           
        }
        
    }

    void SkillInfoUpdate()
    {
        Skill_Sprite.spriteName = info.icon_name;
        Skill_Intro = info.Skill_Intro;
        
    }
    protected override void OnDragDropRelease(GameObject surface)
    {
       
            base.OnDragDropRelease(surface);
            if (surface != null&& islearned)
            {

                if (surface.tag == Tags.quickItem)
                {
                    
                    Debug.Log("上面已有快捷方式");
                    surface.GetComponentInParent<QuickGrid>().quickType = QuickGrid.QuickGridType.Skill;
                    if (surface.GetComponent<QuickItem>().id == gameObject.GetComponent<SkillItem>().id)
                    {
                        Debug.Log("重复的快捷方式");
                        //如果与自己的ID重复  不做任何操作
                        return;
                    }
                    else
                    {
                        Debug.Log("不同的快捷方式");
                    
                    QuickGrid[] QuickGrids = FindObjectsOfType<QuickGrid>();
                    foreach (QuickGrid grid in QuickGrids)
                    {
                        Debug.Log(grid.name);
                        if (grid.id == this.id)
                        {
                            Destroy(grid.GetComponentInChildren<QuickItem>().gameObject);
                            grid.id = 0;
                            //销毁原来的
                        }
                    }
                        quickItem.GetComponent<QuickItem>().id = this.id;
                        quickItem.GetComponent<QuickItem>().Quickitem_Type = QuickItem.QuickItemType.Skill;
                        quickItem.GetComponent<QuickItem>().SetID();
                        NGUITools.AddChild(surface.transform.parent.gameObject, quickItem);
                        Destroy(surface);
                        quickItem.GetComponent<QuickItem>().Quickitem_Type = QuickItem.QuickItemType.Skill;
                        surface.GetComponent<QuickItem>().Quickitem_Type = QuickItem.QuickItemType.Skill;
                        surface.transform.parent.GetComponent<QuickGrid>().id = this.id;
                    surface.transform.parent.GetComponent<QuickGrid>().GetColdTime(info.FreezeTime);


                    }
                }
                if (surface.tag == Tags.quickGrid)
                {
                    surface.GetComponent<QuickGrid>().quickType = QuickGrid.QuickGridType.Skill;
                    Debug.Log("上面没有快捷方式");//如果出现重复的 销毁掉原来的 
                    QuickGrid[] QuickGrids = FindObjectsOfType<QuickGrid>();
                    foreach (QuickGrid grid in QuickGrids)
                    {
                        Debug.Log(grid.name);
                        if (grid.id == this.id)
                        {
                            Destroy(grid.GetComponentInChildren<QuickItem>().gameObject);
                            grid.id = 0;
                            //销毁原来的
                        }
                    }
                    quickItem.GetComponent<QuickItem>().id = this.id;
                    quickItem.GetComponent<QuickItem>().Quickitem_Type = QuickItem.QuickItemType.Skill;
                    quickItem.GetComponent<QuickItem>().SetID();
                    NGUITools.AddChild(surface, quickItem);
                    surface.GetComponent<QuickGrid>().id = this.id;
                surface.GetComponent<QuickGrid>().GetColdTime(info.FreezeTime);


            }
            }
        
       
    }
    string GetSkillIntro() //技能信息文本尚未完善
    {
        string text = "";
        text += "技能名字:"+info.Skill_name+"\n";
        text +="技能介绍:"+info.Skill_Intro + "\n";
        text += "作用类型:" + info.apply_Type + "\n";
        text += "冷却时间" + info.FreezeTime + "\n";
       
        return text;
        
    }
    public void Show()
    {
        isHover = true;
        this.transform.GetChild(1).gameObject.SetActive(true);
        this.transform.GetChild(1).GetComponentInChildren<UILabel>().text = GetSkillIntro();
       

    }
    public void Hide()
    {
        isHover = false;
        this.transform.GetChild(1).gameObject.SetActive(false);
        //SkillTool._instance.isHover = false;
    }
    


}
