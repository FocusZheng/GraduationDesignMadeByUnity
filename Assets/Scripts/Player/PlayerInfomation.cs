using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfomation : MonoBehaviour {

    public string PlayerName;
    public float Hp = 100;//血量上限
    public float Mp = 100;//蓝量上限
    public float Exp = 0;
    public float Level = 1;
    public float Hp_Remain = 100;
    public float Mp_Remain = 100;



    public float Ad = 20;
    public float Ad_Plus = 0;
    public float Speed = 3;
    public float Speed_Plus = 0;
    public float Defenese = 10;
    public float Defenese_Plus = 0;
    public HeroType herotype = HeroType.Magician;//人物选定界面
    public StatusContorl PlayerStatus;

    
    public float Point_Rest = 0;

    public GameObject HudText_Prefab;
    private GameObject HudGo;
    private GameObject HuDFollow;

    private HUDText HudText;
    private UIFollowTarget followTarget;
    private bool isDead=false;
    public GameObject LevelUpEffcet;
    public GameObject DeathTips;

    private void Start()
    {
        PlayerName = PlayerPrefs.GetString("Name");
        HudGo =NGUITools.AddChild(HUDTextParent._instance.gameObject, HudText_Prefab);
        HuDFollow = transform.Find("HUDPosition").gameObject;
        HudText = HudGo.GetComponent<HUDText>();
        followTarget = HudGo.GetComponent<UIFollowTarget>();
        followTarget.target = HuDFollow.transform;
        followTarget.gameCamera = Camera.main;
        followTarget.uiCamera= GameObject.Find("UI Root").GetComponentInChildren<Camera>();



    }
    public enum HeroType
    {
        Swordman,
        Magician
    }
    public void GetDamage(float hp_remain=0,float mp_remain=0)//扣血扣蓝
    {
        if (isDead)
        {
            return;
        }
        float damage = hp_remain - (hp_remain * (PlayerStatus.Total_Defenese * 0.015f));
        Hp_Remain -=damage ;
        Mp_Remain -= mp_remain;  
        if(hp_remain!=0)
        {
            HudText.Add("-" + (int)damage, Color.red, 1);// Unity中类型强制转换
        }   
        if (Hp_Remain <=0)
        {
            Hp_Remain = 0;
            isDead = true;
            AfterDeath();
        }
    
       
    }
    public void HealSelf(float hp_remain = 0, float mp_remain = 0)//加血
    {
        Hp_Remain += hp_remain;
        Mp_Remain += mp_remain;
        if (Hp_Remain > Hp + Level * 20)
        {
            Hp_Remain = Hp + Level * 20;
            Debug.Log("超出生命值上限");
        }
        if (Mp_Remain > mp_remain + Level * 40)
        {
            Mp_Remain = Mp + Level * 40;
        }
    }
    public void ExpUp(int exp_remain)//升级
    {
        Exp += exp_remain;
        HudText.Add("+" + exp_remain, Color.yellow, 1);
        if(Exp>100+Level*30)
        {
            Level++;
            Exp -= 100 + Level * 30;
            Hp_Remain = Hp;
            Mp_Remain = Mp;
            GameObject.Instantiate(LevelUpEffcet, transform.position, Quaternion.identity);
            
        }
    }
    void AfterDeath()
    {
        transform.GetComponent<PlayFight>().state = PlayFight.PlayerAnimState.Death;
        transform.GetComponent<PlayerMoveToDir>().enabled = false;
        transform.GetComponent<PlayerMove>().enabled = false;
        DeathTips.SetActive(true);
        
    }
    public bool AddPoint(int point = 1)
    {
        if (Point_Rest >= point)
        {
            Point_Rest -= point;

            return true;
        }
        else
        {
            return false;
        }
    }
    public bool MinPoint(int point = 1)
    {
        Point_Rest += point;
        return true;
    }
    
    public float OnadClick(float Plus,int point=1)
    {
        if(Point_Rest>=point)
        {
            Point_Rest -= point;
            Plus += point;
            return Plus;
        }
        else
        {
            return Plus;
        }
    }
    public void ONAd()
    {
        this.Ad_Plus = OnadClick(this.Ad_Plus);
        StatusContorl._intance.UpdateShow();
    }


}
