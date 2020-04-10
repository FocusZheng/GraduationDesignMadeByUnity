using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public enum WolfState
    {
        Idle,
        Walk,
        Attack,
        Death
    }

    public WolfState state = WolfState.Idle;
    public float hp = 100;//怪物生命值
    public float attack ;//怪物攻击力
    public float crazyAttack;
    public float speed = 2;//移动速度
    public int Exp;//掉落经验
    public int Coin;//掉落金币

    public float timer=0;//巡逻计时器
    public float time = 1;

    private CharacterController enemycc;
    private Animation enemyAnim;
    private Transform Player;

    public float MinDistance=2;//最小距离 大于该距离往玩家方向移动
    public float MaxDistance=5;//最大距离 小于该距离 大于最大距离巡逻

    
    public string Now_Anim;//当前状态
    public string Idel_Anim;//待机动作
    public string Attack_Anim;//攻击动作
    public string CrazyAtt_Anim;//疯狂攻击动作
    public string Damage_Anim;//受伤动作
    public string Death_Anim;//死亡动作
    public string Walk_Anim;//走路动作
    public string NowAttack_Anim;//当前的攻击动作

    private Color normalColor;
    

    public GameObject HUDPreFab;//Prefab
    private GameObject HUDFollow;//HUDFollow的物体
    private GameObject HUDGo;//实例化出来的HUD

    private HUDText HudText;
    private UIFollowTarget followtarget;

    private float miss_rate = 0.1f;//Miss几率
    public float attack_rate = 1;//攻击速度
    private float crazyatt_rate = 0.1f;//疯狂攻击概率
    private float attack_timer = 0;//攻击计时器
    private float time_attack=0.633f;//动画播放时间
    private float Timecrazy_attack=0.733f;//动画播放时间


    private Transform target;
    private GameObject Spwan;

    

    private void Awake()
    {
        Now_Anim = Idel_Anim;
    }
    // Use this for initialization
    void Start () {
        enemycc = this.GetComponent<CharacterController>();
        enemyAnim = this.GetComponent<Animation>();
        Player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        Spwan = GetComponentInParent<WolfBabySpwan>().gameObject;
        normalColor = this.GetComponentInChildren<SkinnedMeshRenderer>().material.color;
        HUDGo = NGUITools.AddChild(HUDTextParent._instance.gameObject,HUDPreFab);
        HUDFollow = transform.Find("HUDPosition").gameObject;
        HudText = HUDGo.GetComponent<HUDText>();
        followtarget = HUDGo.GetComponent<UIFollowTarget>();
        followtarget.target = HUDFollow.transform;
        followtarget.gameCamera = Camera.main;
        followtarget.uiCamera = GameObject.Find("UI Root").GetComponentInChildren<Camera>();
        
       
        
	}

    public void GetDamage(float damage)
    {
        if (state == WolfState.Death)
        {
            return;
        }
        float value = Random.Range(0f, 1f);
        if(value<miss_rate)
        {
            HudText.Add("Miss", Color.gray,1);
        }
        else
        {
            
            state = WolfState.Attack;
            this.hp -= damage;
            HudText.Add("-" + damage, Color.red, 1);
            StartCoroutine(WolfBodyRed());
            enemyAnim.CrossFade(Damage_Anim);
        }
        if (hp <=0)
        {
            
            state = WolfState.Death;
            Destroy(this.gameObject, 2);
            Spwan.GetComponent<WolfBabySpwan>().MinCurrentNum();
            Player.GetComponent<PlayFight>().StopFight();
            AfterDeath();


        }
    }
    IEnumerator WolfBodyRed()
    {
        this.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        this.GetComponentInChildren<SkinnedMeshRenderer>().material.color = normalColor;

    }
    // Update is called once per frame
    void Update()
    {
        
        if(state==WolfState.Death)//死亡
        {
            enemyAnim.CrossFade(Death_Anim);
 
        }
        else if(state == WolfState.Attack)
        {
            //自动攻击
            
            AutoAttack(Player);
        }
        else //巡逻 Idle和Walk
        {
            enemyAnim.CrossFade(Now_Anim);//播放当前状态
            if(Now_Anim==Walk_Anim)
            {
                enemycc.SimpleMove(transform.forward * speed);
            }
            timer += Time.deltaTime;
            if(timer>=time)//计时结束切换状态
            {
                timer = 0;
                RandomState();
            }
        }
        

    }
    void RandomState() //idle和移动之间切换
    {
        int value = Random.Range(0, 4);
        if(value==0)
        {
            if (Now_Anim != Walk_Anim) //先判断当前状态是不是行走 不等于说明原来是Idle状态 需要调整一个随机方向
            {
                transform.Rotate(transform.up * Random.Range(0, 360));
            }
            Now_Anim = Walk_Anim;
           
        }
        else
        {
            Now_Anim = Idel_Anim;

        }
    }
    void AutoAttack(Transform target)
    {
        if(Player.GetComponent<PlayFight>().state==PlayFight.PlayerAnimState.Death)
        {
            state = WolfState.Idle;
            target = null;
            return;
        }
        if(target!=null)
        {
            
            float Distance = Vector3.Distance(target.position,transform.position);
            if(Distance<=MinDistance) //进行攻击
            {
                attack_timer += Time.deltaTime;
                enemyAnim.CrossFade(NowAttack_Anim);
                if (NowAttack_Anim==Attack_Anim)//上一次攻击动作是正常攻击
                {
                    //造成伤害
                    //随机攻击方式
                    if(attack_timer>time_attack)//攻击完成 恢复到Idle
                    {
                        Player.GetComponent<PlayerInfomation>().GetDamage(attack);
                        NowAttack_Anim = Idel_Anim;
                        
                    }
                    
                    
                }
                else if(NowAttack_Anim==CrazyAtt_Anim)
                {
                    if(attack_timer>Timecrazy_attack)
                    {
                        Player.GetComponent<PlayerInfomation>().GetDamage((crazyAttack));//疯狂攻击伤害+20
                        NowAttack_Anim = Idel_Anim;
                    }
                }
                if(attack_timer>(1f/attack_rate))//再次攻击
                {
                    RandomAttack();
                    attack_timer = 0;
                }
                
            }
            else if(Distance>MaxDistance)//大于最大距离停下
            {
                state = WolfState.Idle;
                target = null;
                enemyAnim.CrossFade(Idel_Anim);
            }
            else //移动向角色
            {
                transform.LookAt(target.position);
                enemyAnim.CrossFade(Walk_Anim);
                enemycc.SimpleMove(transform.forward * speed);
            }
               
        }
        else
        {
            enemyAnim.CrossFade(Idel_Anim);
        }
        
    }

    private void OnDestroy()
    {
        GameObject.Destroy(HUDGo);
    }
    void RandomAttack() //随机攻击的判定
    {
        float value = Random.Range(0f, 1f);
       
        if(value<crazyatt_rate)
        {
            NowAttack_Anim = CrazyAtt_Anim;
            Debug.Log("重击");
        }
        else
        {
            NowAttack_Anim = Attack_Anim;
            Debug.Log("普通攻击");
        }

    }
    void AfterDeath()
    {
        Player.GetComponent<PlayerInfomation>().ExpUp(Exp);
        Inventory._instance.GetCoin(Coin);
        if(GameObject.FindObjectOfType<NPCMisson>().MissonIsGoing&& FindObjectOfType<NPCMisson>().MissionCount==0)
        {
            FindObjectOfType<NPCMisson>().MissonProcessCount++;
        }

    }
    private void OnMouseOver()
    {
        CursorChange._instance.AttackCursor();
    }
    private void OnMouseExit()
    {
        CursorChange._instance.NormalCursor();
    }
    
}

