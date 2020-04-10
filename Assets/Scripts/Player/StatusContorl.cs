using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusContorl : MonoBehaviour {

    public static StatusContorl _intance;
    private UILabel AD;
    private UILabel Speed;
    private UILabel Defenese;
    private UILabel RestPointText;
    private PlayerInfomation Playinfo;
    private bool isSave = false;
    private int EquipMentAttack=0;
    private int EquipMentDefenese=0;
    private int EquipMentSpeed=0;
    public TweenPosition StatusTween;

    [HideInInspector]
    public float Total_AD;
    public float Total_Speed;
    public float Total_Defenese;

    // Use this for initialization
    void Awake () {
        _intance = this;
        StatusTween = this.GetComponent<TweenPosition>();
        AD = transform.Find("AD").GetComponent<UILabel>();
        Speed = transform.Find("Speed").GetComponent<UILabel>();
        Defenese = transform.Find("Defenese").GetComponent<UILabel>();
        RestPointText = transform.Find("restpointText").GetComponent<UILabel>();
        Playinfo = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerInfomation>();
        UpdateProperty();
        UpdateShow();
       


    }
	
	// Update is called once per frame
	void Update () {
     
	}
  
    void TotalProperty()
    {
        Total_AD = Playinfo.Ad + Playinfo.Ad_Plus + EquipMentAttack;
        Total_Defenese = Playinfo.Defenese + Playinfo.Defenese_Plus + EquipMentDefenese;
        Total_Speed = Playinfo.Speed + Playinfo.Speed_Plus + EquipMentSpeed;
    }
    public void ShowStatus()
    {
        StatusTween.enabled = true;
        StatusTween.PlayForward();
    }
    public void HideStatus()
    {
        StatusTween.PlayReverse();
    }
    public void UpdateShow()
    {
        AD.text = Playinfo.Ad + "+" + "[00ffff]"+Playinfo.Ad_Plus+"[-]"+"+"+"[0000ff]"+ EquipMentAttack+"[-]";      
        Speed.text = Playinfo.Speed + "+" + "[00ffff]"+Playinfo.Speed_Plus+ "[-]"+"+" + "[0000ff]" + EquipMentSpeed+"[-]";
        Defenese.text = Playinfo.Defenese + "+" + "[00ffff]"+ Playinfo.Defenese_Plus+ "[-]"+"+" + "[0000ff]" + EquipMentDefenese+"[-]";
        RestPointText.text = Playinfo.Point_Rest.ToString() ;
        TotalProperty();


    }
    public void EquipChangeUpdateShow()
    {
        AD.text = "(" + (Playinfo.Ad + Playinfo.Ad_Plus).ToString() + "+" + "[0000ff]" + EquipMentAttack + "[-]" + ")" + "(" + "[800080]" + (Playinfo.Ad + Playinfo.Ad_Plus + EquipMentAttack).ToString() + "[-]" + ")";
        Speed.text = "(" + (Playinfo.Speed + Playinfo.Speed_Plus).ToString() + "+" + "[0000ff]" + EquipMentSpeed + "[-]" + ")" + "(" + "[800080]" + (Playinfo.Speed + Playinfo.Speed_Plus + EquipMentSpeed).ToString() + "[-]" + ")";
        Defenese.text = "(" + (Playinfo.Defenese + Playinfo.Defenese_Plus).ToString() + "+" + "[0000ff]" + EquipMentDefenese + "[-]" + ")" + "(" + "[800080]" + (Playinfo.Defenese + Playinfo.Defenese_Plus + EquipMentDefenese).ToString() + "[-]" + ")";
        TotalProperty();
    }

  
    public void UpdateProperty()
    {
        this.EquipMentAttack = 0; //更新重新加一遍
        this.EquipMentDefenese = 0;
        this.EquipMentSpeed = 0;
        EquipGrid[] EquipMentList = GameObject.FindObjectsOfType<EquipGrid>();
        foreach (EquipGrid item in EquipMentList)
        {
            if(item.id!=0)
            {
                Objectinfomation equipinfo = ObjectInfo._instance.GetInfoByID(item.id);
                EquipMentAttack += equipinfo.attack;
                EquipMentDefenese += equipinfo.defenese;
                EquipMentSpeed += equipinfo.speed;
            }
         
        }
        

    
        
    }

    public void OnADPlusClick()
    {
        bool success = Playinfo.AddPoint();
        if(success)
        {
            Playinfo.Ad_Plus++;
            UpdateShow();
            isSave = false;
        }
    }

    public void OnADMinClick()
    {
        if(Playinfo.Ad_Plus > 0)
        {
            bool success = Playinfo.MinPoint();
            if (success)
            {

                Playinfo.Ad_Plus--;
                UpdateShow();
                isSave = false;
            }
        }
    
    }
    public void OnSpeedPlusClick()
    {
        bool success = Playinfo.AddPoint();
        if (success)
        {
            Playinfo.Speed_Plus++;
            UpdateShow();
            isSave = false;
        }
    }
    public void OnSpeedMinClick()
    {
        if(Playinfo.Speed_Plus > 0)
        {
            bool success = Playinfo.MinPoint();
            if (success )
            {
                Playinfo.Speed_Plus--;
                UpdateShow();
                isSave = false;
            }
        }
        
        
    }
    public void OnDefenesePlusClick()
    {
        bool success = Playinfo.AddPoint();
        if (success)
        {
            Playinfo.Defenese_Plus++;
            UpdateShow();
            isSave = false;
        }
    }
    public void OnDefeneseMinClick()
    {
        if(Playinfo.Defenese_Plus > 0)
        {
            bool success = Playinfo.MinPoint();
            if (success)
            {
                Playinfo.Defenese_Plus--;
                UpdateShow();
                isSave = false;
            }
        }
       

    }
    public void OnSaveClick()
    {
        isSave = true;
        Playinfo.Ad += Playinfo.Ad_Plus;
        Playinfo.Speed += Playinfo.Speed_Plus;
        Playinfo.Defenese += Playinfo.Defenese_Plus;
        Playinfo.Ad_Plus = 0;
        Playinfo.Speed_Plus = 0;
        Playinfo.Defenese_Plus = 0;
        AD.text = "("+(Playinfo.Ad + Playinfo.Ad_Plus).ToString()+"+"+"[0000ff]"+EquipMentAttack+"[-]"+")"+"("+ "[800080]"+(Playinfo.Ad+Playinfo.Ad_Plus+EquipMentAttack).ToString()+"[-]"+")";
        Speed.text = "("+(Playinfo.Speed + Playinfo.Speed_Plus).ToString()+"+"+"[0000ff]"+EquipMentSpeed+"[-]"+")" + "(" + "[800080]" + (Playinfo.Speed + Playinfo.Speed_Plus + EquipMentSpeed).ToString() + "[-]" + ")";
        Defenese.text = "("+(Playinfo.Defenese+Playinfo.Defenese_Plus).ToString()+"+"+"[0000ff]"+EquipMentDefenese+"[-]"+")" + "(" + "[800080]" + (Playinfo.Defenese + Playinfo.Defenese_Plus + EquipMentDefenese).ToString() + "[-]" + ")";
       

    }
    public void OnResetClick()
    {
        if(!isSave)
        {
            float RestPoint = Playinfo.Ad_Plus + Playinfo.Speed_Plus + Playinfo.Defenese_Plus;
            Playinfo.Ad_Plus = 0;
            Playinfo.Speed_Plus = 0;
            Playinfo.Defenese_Plus = 0;
            Playinfo.Point_Rest += RestPoint;
            UpdateShow();
        }
       
    }


}
