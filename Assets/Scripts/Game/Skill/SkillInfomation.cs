using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInfomation : MonoBehaviour {


    public int id;//技能编号
    public string Skill_name;//技能名字
    public string icon_name;//技能图标名称
    public string Skill_Intro;//技能描述

    public string apply_Type;
    public ApplyType applyType;//作用的类型

    public string apply_Property;
    public ApplyProperty applyProperty;//作用的属性

    public int Buffvalue;//Buff增加量
    public float BuffTime;//Buff持续时间
    public int MpCousume;//消耗蓝量
    public float FreezeTime;//冷却

    public string Man_Type;
    public ApplicableRole ManType;//使用角色

    public int Level_Limit;//适用等级
    public string release_Type;
    public ReleaseType releaseType;//释放类型
    public float Skill_distance;//释放距离
    public string Effect_Name;//特效名称
    public string Anim_Name;//施法动作名称
    public float Anim_Time;//动画出现时间

    public int NeedPoint;

    public bool islearned;//是否被学习了
    public int UnLockedId;//需要解锁的ID

    /// <summary>
    /// 适用角色
    /// </summary>
    public enum ApplicableRole
    {
        Swordman,
        Magician
    }
    /// <summary>
    /// 作用类型
    /// </summary>
    public enum ApplyType
    {
        Passive,//回复技能
        Buff,//buff技能
        SingleTarget,//单体技能
        MultiTarget//群体技能
    }
    /// <summary>
    /// 作用的属性
    /// </summary>
    public enum ApplyProperty//作用的属性
    {
        Attack,
        Defenese,
        Speed,
        AttackSpeed,
        Hp,
        Mp,
    }
    /// <summary>
    /// 释放类型
    /// </summary>
    public enum ReleaseType//释放类型
    {
        Self ,//自身位置释放
        Enemy,//指定敌人释放
        Position//指定位置释放
        
    }

        

}
