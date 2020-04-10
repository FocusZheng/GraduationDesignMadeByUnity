using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMisson : NPC {
    public static NPCMisson _instance;
    public TweenPosition MissonPauel;
    public GameObject AcceptObj;
    public GameObject GiveUpObj;
    public GameObject CommitObj;
    public GameObject CloseObj;
    public UILabel MissonText;
    public float MissonProcessCount=0;//任务进度
    public bool MissonIsGoing=false;//任务是否在进行
    private PlayerInfomation player;
    public int MissionCount=0;
    private InventoryGrid grid = null;
    // Use this for initialization
    private void Awake()
    {
        _instance = this;
    }
    void Start () {
        player = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerInfomation>();
	}
	
	// Update is called once per frame

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ShowMissonPanel();
            Debug.Log("NPC点击");
        }
    }
    void ShowMissonPanel() //显示任务面板
    {
        MissonPauel.gameObject.SetActive(true);
        MissonPauel.PlayForward();
        ShowMissonMessage();


    }
   public void CloseMissonPanel()//关闭任务面板
    {
        MissonPauel.PlayReverse();
    }
   public void OnAcceptButtonClick()//接受任务
    {
        MissonIsGoing = true;
        ShowMissonMessage();
        AcceptObj.SetActive(false);
        CommitObj.SetActive(true);
        GiveUpObj.SetActive(true);
    }
   public void OnGiveUpButtonClick()//放弃任务
    {
        MissonIsGoing = false;
        MissonProcessCount = 0;
        AcceptObj.SetActive(true);
        CommitObj.SetActive(false);
        GiveUpObj.SetActive(false);
    }
   public void OnCommitButtonClick()//提交任务
    {
        int missionProcess=0;
        int Pay = 0;
        switch (MissionCount)
        {
            case 0:missionProcess = 10;Pay=1000 ; break;
            case 1: missionProcess = 3; Pay = 300; break;
            case 2: missionProcess = 1; Pay = 100; break;
        }

        if(MissonProcessCount>=missionProcess)
        {
            
            
            Debug.Log("任务完成");
            MissonProcessCount = 0;
            MissonIsGoing = false;
            Inventory._instance.GetCoin(Pay);
            if(MissionCount==1)
            {
                this.grid.MinObject(missionProcess);
            }
            MissionCount++;
            ShowMissonMessage();
            AcceptObj.SetActive(true);
            CommitObj.SetActive(false);
            GiveUpObj.SetActive(false);
        }
        else
        {
            Debug.Log("任务还没完成");
        }
        
    }

    void ShowMissonMessage()
    {
        switch(MissionCount)
        {
            case 0:WolfBabyMission(); break;
            case 1:BuySomeThingMission();break;
            case 2:LookSomeWhereMission();break;
               
        }
      
    }
    void WolfBabyMission()
    {
        if (!MissonIsGoing)
        {

            MissonText.text = "任务:\n击杀10只怪物\n 奖励:1000金币";
        }
        else
        {
            MissonText.text = "任务进度:\n已击杀(" + MissonProcessCount + "/10)";
        }
    }
    void BuySomeThingMission()
    {
        if (!MissonIsGoing)
        {

            MissonText.text = "任务:\n帮我买3瓶生命药水\n 奖励:300金币";
        }
        else
        {
            InventoryGrid[] grids = GameObject.FindObjectsOfType<InventoryGrid>();
            foreach(InventoryGrid grid in grids)
            {
                if(grid.id==1001)
                {
                    this.grid = grid;
                    MissonProcessCount=grid.num;
                    break;
                }
            }
            MissonText.text = "任务进度:\n身上已拥有(" + MissonProcessCount + "/3)";
        }
    }
    void LookSomeWhereMission()
    {
        if (!MissonIsGoing)
        {

            MissonText.text = "任务:\n去船坞侦查一下\n 奖励:100金币";
        }
        else
        {
            MissonText.text = "任务进度:\n侦查进度(" + MissonProcessCount + "/1)";
        }
    }
}
