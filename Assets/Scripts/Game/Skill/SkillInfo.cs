using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInfo : MonoBehaviour {

    public TextAsset SkillinfoText;
    private Dictionary<int, SkillInfomation> SkillDic = new Dictionary<int, SkillInfomation>();
    public static SkillInfo _instance;
    // Use this for initialization

    private void Awake()
    {
        _instance = this;
        ReadInfo();
        Debug.Log("技能信息读取完毕");
    }
    // Update is called once per frame
    void Update () {
		
	}
    public SkillInfomation GetSkillInfoByID(int id)
    {
        SkillInfomation info = null;
        SkillDic.TryGetValue(id, out info);
        return info;
    }

    void ReadInfo()
    {
        string SkillText = SkillinfoText.text;
        string[] SkillEveryStr = SkillText.Split('\n');
        foreach(string Skillinfo in SkillEveryStr)
        {
            SkillInfomation info = new SkillInfomation();
            string[] skillinfo = Skillinfo.Split(',');
            //Debug.Log(Skillinfo);
            info.id = int.Parse(skillinfo[0]); // ID
            info.Skill_name = skillinfo[1];//技能名字
            info.icon_name = skillinfo[2];//标签名字
            info.Skill_Intro = skillinfo[3];//技能介绍
            info.apply_Type = skillinfo[4];//作用类型
            info.apply_Property = skillinfo[5];//作用属性
            switch (info.apply_Type)
            {
                case "Passive":info.applyType=SkillInfomation.ApplyType.Passive; break;//回复
                case "Buff":info.applyType=SkillInfomation.ApplyType.Buff; break;//Buff
                case "SingleTarget":info.applyType=SkillInfomation.ApplyType.SingleTarget; break;//单体
                case "MultiTarget":info.applyType=SkillInfomation.ApplyType.MultiTarget; break;//群体
            }       
            switch(info.apply_Property)
            {
                case "Attack":info.applyProperty = SkillInfomation.ApplyProperty.Attack;break;
                case "Def":info.applyProperty = SkillInfomation.ApplyProperty.Defenese;break;
                case "Speed":info.applyProperty = SkillInfomation.ApplyProperty.Speed;break;
                case "AttackSpeed":info.applyProperty = SkillInfomation.ApplyProperty.AttackSpeed;break;
                case "HP":info.applyProperty = SkillInfomation.ApplyProperty.Hp;break;
                case "MP":info.applyProperty = SkillInfomation.ApplyProperty.Mp;break;
            }
            info.Buffvalue = int.Parse(skillinfo[6]);//Buff增加量
            info.BuffTime = float.Parse(skillinfo[7]);//Buff持续时间
            info.MpCousume = int.Parse(skillinfo[8]);//技能蓝耗
            info.FreezeTime = float.Parse(skillinfo[9]);//冷却时间
            info.Man_Type = skillinfo[10];//适用角色
            switch(info.Man_Type)//
            {
                case "Swordman":info.ManType=SkillInfomation.ApplicableRole.Swordman; break;
                case "Magician":info.ManType=SkillInfomation.ApplicableRole.Magician; break;
            }
            info.Level_Limit = int.Parse(skillinfo[11]);
            info.release_Type = skillinfo[12];
            switch(info.release_Type)
            {
                case "Self":info.releaseType=SkillInfomation.ReleaseType.Self; break;
                case "Enemy":info.releaseType=SkillInfomation.ReleaseType.Enemy; break;
                case "Position":info.releaseType=SkillInfomation.ReleaseType.Position; break;
            }
            info.Skill_distance = float.Parse(skillinfo[13]);
            info.NeedPoint = int.Parse(skillinfo[14]);
            info.Effect_Name = skillinfo[15];
            info.Anim_Name = skillinfo[16];
            info.Anim_Time = float.Parse(skillinfo[17]);
            SkillDic.Add(info.id, info);
            //Debug.Log(info.id);
            //Debug.Log(info.Skill_name);
            //Debug.Log(info.Skill_Intro);
            //Debug.Log(info.applyType);
            //Debug.Log(info.applyProperty);
            //Debug.Log(info.Buffvalue);
            //Debug.Log(info.BuffTime);
            //Debug.Log(info.MpCousume);
            //Debug.Log(info.FreezeTime);
            //Debug.Log(info.ManType);
            //Debug.Log(info.Level_Limit);
            //Debug.Log(info.releaseType);
            //Debug.Log(info.Skill_distance);

        }
    }
}
