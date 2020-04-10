using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayFight : MonoBehaviour {

    public static PlayFight _instance;
    
    public enum PlayerAnimState//人物动作状态
    {
        ControlWalk,//移动
        NormalAttack,//普通攻击
        SkillAttack,//技能攻击
        Death

    }
    public enum AttackState
    {
        Attack,
        Moving,
        Idle
    }
    public PlayerAnimState state = PlayerAnimState.ControlWalk;
    public AttackState attack_state = AttackState.Moving;
    public string aniname_normalAttack;//普通攻击动画名称
    public string aniname_idle;//待机状态动画名称
    public string aniname_now;//当前播放的动画名称
    public float time_normalattack;//普通攻击间隔时间
    private float timer=0;
    public float rate_normalattack = 1;//攻击速度
    private float min_distance = 5;//默认攻击的最小距离

    [HideInInspector]
    public  Transform target_normalattack;//攻击目标

    private CharacterController cc;
    private Animation player;
    private bool isShowEffect;//是否已经播放过特效
    public GameObject NormalAttack_Effect;

    public GameObject[] Effcet;
    private Dictionary<string, GameObject> SkillEffect = new Dictionary<string, GameObject>();

    private float FightAttack;//人物攻击
    private float FightDefenese;//人物防御
    private float FightSpeed;//人物移速

    bool isLockTargeting = false;//正在锁定目标中
    private SkillInfomation skillinfo = null;
    private bool isSingle = false;//是否是单体技能


    // Use this for initialization
    private void Awake()
    {
        _instance = this;
    }
    void Start () {
        cc = this.GetComponent<CharacterController>();
        player = this.GetComponent<Animation>();
        SkillEffcetAdd();
        FightSpeed = GetComponent<PlayerMoveToDir>().speed;


    }
	
    void SkillEffcetAdd()
    {
        foreach(GameObject go in Effcet)
        {
            SkillEffect.Add(go.name, go);
        }
    }
    float MakeDamage()
    {
        float AD = StatusContorl._intance.Total_AD;
        return AD;
    }
    public void StopFight()
    {
        this.target_normalattack = null;
        state = PlayerAnimState.ControlWalk;
        this.GetComponent<PlayerMove>().targetPosition = transform.position;
        this.GetComponent<PlayerMoveToDir>().playerMove = PlayerMoveToDir.PlayerMoveState.Idle;
       
    }
    public void UseSkill(int id)
    {
        SkillInfomation info = null;
        info = SkillInfo._instance.GetSkillInfoByID(id);
        
        switch(info.applyType)
        {
            case SkillInfomation.ApplyType.Passive:PassiveSkill(info);break;
            case SkillInfomation.ApplyType.Buff:StartCoroutine(BuffSkill(info));break;
            case SkillInfomation.ApplyType.SingleTarget:LockTarget(info);break;
            case SkillInfomation.ApplyType.MultiTarget:LockTarget(info); break;
        }

    }
    void QuickGridColdTime(int id)
    {
        QuickGrid[] quickGrids = GameObject.FindObjectsOfType<QuickGrid>();
        foreach(QuickGrid grid in quickGrids)
        {
            if(grid.id==id)
            {
                grid.StartColding();
                break;
            }

        }
    }
    /// <summary>
    /// 回复型技能使用
    /// </summary>
    /// <param name="info"></param>
    public void PassiveSkill(SkillInfomation info) 
    {
        if(player.GetComponent<PlayerInfomation>().Mp_Remain>=info.MpCousume)
        {
            QuickGridColdTime(info.id);
            state = PlayerAnimState.SkillAttack;
            player.CrossFade(info.Anim_Name);
            GameObject effect = null;
            SkillEffect.TryGetValue(info.Effect_Name, out effect);
            GameObject.Instantiate(effect, transform.position, Quaternion.identity);
            
            switch (info.applyProperty)
            {
                case SkillInfomation.ApplyProperty.Hp: player.GetComponent<PlayerInfomation>().HealSelf(info.Buffvalue, 0); break;
                case SkillInfomation.ApplyProperty.Mp: player.GetComponent<PlayerInfomation>().HealSelf(0, info.Buffvalue); break;
            }
            //state = PlayerAnimState.ControlWalk;
        }
       
        
    }
   /// <summary>
   /// Buff类型技能使用
   /// </summary>
   /// <param name="info"></param>
   /// <returns></returns>
    IEnumerator BuffSkill(SkillInfomation info)
    {
        if(player.GetComponent<PlayerInfomation>().Mp_Remain>=info.MpCousume)
        {
            QuickGridColdTime(info.id);
            state = PlayerAnimState.SkillAttack;
            player.CrossFade(info.Anim_Name);
            player.GetComponent<PlayerInfomation>().GetDamage(0, info.MpCousume);
            GameObject effect = null;
            SkillEffect.TryGetValue(info.Effect_Name, out effect);
            GameObject.Instantiate(effect, transform.position, Quaternion.identity);                   
            switch (info.applyProperty)
            {
                case SkillInfomation.ApplyProperty.Attack: StatusContorl._intance.Total_AD*= (info.Buffvalue / 100);break;
                case SkillInfomation.ApplyProperty.Defenese: StatusContorl._intance.Total_Defenese *= (info.Buffvalue /100);break;
                case SkillInfomation.ApplyProperty.Speed: FightSpeed *= (info.Buffvalue / 100);break;
                case SkillInfomation.ApplyProperty.AttackSpeed:rate_normalattack *=(info.Buffvalue / 100); break;
            }
            Debug.Log("Buff加成开启");
            BuffStateShow._instance.BuffShow(info.id,info.BuffTime);
            //state = PlayerAnimState.ControlWalk;
            yield return new WaitForSeconds(info.BuffTime);
          
            switch (info.applyProperty)
            {
                case SkillInfomation.ApplyProperty.Attack: StatusContorl._intance.Total_AD /= (info.Buffvalue / 100); break;
                case SkillInfomation.ApplyProperty.Defenese: StatusContorl._intance.Total_Defenese /= (info.Buffvalue / 100); break;
                case SkillInfomation.ApplyProperty.Speed: FightSpeed /= (info.Buffvalue / 100); break;
                case SkillInfomation.ApplyProperty.AttackSpeed: rate_normalattack /= (info.Buffvalue / 100); break;
            }
            Debug.Log("Buff加成关闭");

        }
    }
    void AfterLockTargetSingle(Transform target)
    {
        QuickGridColdTime(this.skillinfo.id);
        state = PlayerAnimState.SkillAttack;
        transform.LookAt(target.position);
        player.CrossFade(this.skillinfo.Anim_Name);
        player.GetComponent<PlayerInfomation>().GetDamage(0, this.skillinfo.MpCousume);
        GameObject effect = null;
        SkillEffect.TryGetValue(this.skillinfo.Effect_Name, out effect);
        GameObject.Instantiate(effect, target.position, Quaternion.identity);
        target.GetComponent<Enemy>().GetDamage(MakeDamage()*(this.skillinfo.Buffvalue/100));
        //state = PlayerAnimState.ControlWalk;

    }
    void AfterLockTargetMuilt(RaycastHit hitinfo)
    {
        QuickGridColdTime(this.skillinfo.id);
        state = PlayerAnimState.SkillAttack;
        transform.LookAt(hitinfo.point+Vector3.up*0.5f);
        player.CrossFade(this.skillinfo.Anim_Name);
        player.GetComponent<PlayerInfomation>().GetDamage(0, this.skillinfo.MpCousume);
        GameObject effect = null;
        SkillEffect.TryGetValue(this.skillinfo.Effect_Name, out effect);
        GameObject.Instantiate(effect, hitinfo.point+Vector3.up*0.5f, Quaternion.identity);
        effect.GetComponent<AreaDamage>().damage = MakeDamage() * (this.skillinfo.Buffvalue / 100);
        //state = PlayerAnimState.ControlWalk;


    }
    /// <summary>
    /// 锁定目标
    /// </summary>
    /// <param name="info"></param>
    void LockTarget(SkillInfomation info)
    {
    if (player.GetComponent<PlayerInfomation>().Mp_Remain >= info.MpCousume)
    {
        
        CursorChange._instance.LockTargetCursor();
        isLockTargeting = true;
        this.skillinfo = info;
        switch (this.skillinfo.applyType)
        {
            case SkillInfomation.ApplyType.SingleTarget:isSingle = true; break;
            case SkillInfomation.ApplyType.MultiTarget:isSingle = false; break;
        }

        }



    }
    

 
   
	// Update is called once per frame
	void Update () {
		if(isLockTargeting == false&&Input.GetMouseButtonDown(0)&&state!=PlayerAnimState.Death)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            bool iscollider = Physics.Raycast(ray,out hitInfo);
            if(iscollider&&hitInfo.collider.tag==Tags.Enemy)
            {
                target_normalattack = hitInfo.collider.transform;
                state = PlayerAnimState.NormalAttack;
            }
            else
            {
                state = PlayerAnimState.ControlWalk;
                target_normalattack = null;
            }

        }
        
        if(state==PlayerAnimState.NormalAttack)
        {
            if(target_normalattack==null)
            {
                state = PlayerAnimState.ControlWalk;  
                
                
            }
            else
            {
                float distance = Vector3.Distance(transform.position, target_normalattack.position);
                if (distance < min_distance)//攻击
                {

                    attack_state = AttackState.Attack;
                    transform.LookAt(target_normalattack.position);//锁定攻击位置
                    timer += Time.deltaTime;
                    player.CrossFade(aniname_now);
                    if (timer >= time_normalattack)//一次攻击完成
                    {
                        attack_state = AttackState.Idle;
                        aniname_now = aniname_idle;
                        if (isShowEffect == false)
                        {
                            isShowEffect = true;
                            Instantiate(NormalAttack_Effect, target_normalattack.position, Quaternion.identity);
                            target_normalattack.GetComponent<Enemy>().GetDamage(MakeDamage());
                        }

                    }
                    if (timer >= (1f / rate_normalattack))//攻击速度 准备发起下次攻击
                    {
                        timer = 0; isShowEffect = false;
                        aniname_now = aniname_normalAttack;
                    }
                }
                else //向敌人移动
                {
                       
                       attack_state=AttackState.Moving;
                        player.CrossFade("Walk");
                        transform.LookAt(target_normalattack.position);
                        cc.SimpleMove(transform.forward * 3);
                    
                   
                }
            }
            
                
        }
        if(state==PlayerAnimState.Death)
        {
            player.CrossFade("Death");
        }
        if(isLockTargeting==true&&Input.GetMouseButton(0))
        {

            if (isSingle)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitinfo;
                bool iscollider = Physics.Raycast(ray, out hitinfo);         
                if (iscollider && hitinfo.collider.tag == Tags.Enemy)
                {
                    isLockTargeting = false;
                    CursorChange._instance.NormalCursor();
                    AfterLockTargetSingle(hitinfo.collider.transform);
                }
                else if (iscollider && hitinfo.collider.tag == Tags.ground)
                {
                    isLockTargeting = false;
                    CursorChange._instance.NormalCursor();

                }
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitinfo;
                bool iscollider = Physics.Raycast(ray, out hitinfo);
                if(iscollider&&hitinfo.collider.tag==Tags.ground)
                {
                    isLockTargeting = false;
                    CursorChange._instance.NormalCursor();
                    AfterLockTargetMuilt(hitinfo);

                }
                else
                {
                    isLockTargeting = false;
                    CursorChange._instance.NormalCursor();
                }
            }
          
           
            
           
        }
       
	}
    
}
