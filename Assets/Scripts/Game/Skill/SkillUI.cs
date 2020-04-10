using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUI : MonoBehaviour {

    public static SkillUI _instance;
    public PlayerInfomation.HeroType heroType;
    public SkillItem[] skillItems;
    public int SkillPoint=20;
    public UILabel SkillPointRest;
    // Use this for initialization
    public TweenPosition SkillTween;
    public void Show()
    {
        SkillTween.PlayForward();
    }
    public void Hide()
    {
        SkillTween.PlayReverse();
    }
    private void Awake()
    {
        SkillTween = transform.parent.GetComponent<TweenPosition>();
        _instance = this;
        if (heroType == PlayerInfomation.HeroType.Swordman)
        {
            foreach (SkillItem temp in skillItems)
            {
                string[] num = temp.gameObject.name.Split('-');
                temp.id = 4000 + int.Parse(num[1]);
               
            }
        }
        else if (heroType == PlayerInfomation.HeroType.Magician)
        {
            foreach (SkillItem temp in skillItems)
            {
                string[] num = temp.gameObject.name.Split('-');
                temp.id = 5000 + int.Parse(num[1]);
                Debug.Log(temp.id);
            }
        }
        SkillPointUpdate();
    }
    void Start () {

	}
    public bool SkillPointCosume(int id)
    {
        SkillInfomation info = SkillInfo._instance.GetSkillInfoByID(id);
        if(SkillPoint>=info.NeedPoint)
        {
       
            return true;
        }
        else
        {
            Debug.Log("点数不够");
            return false;
        }
    }
    public void SkillPointUpdate()
    {
        SkillPointRest.text = "SkillPoint:" + SkillPoint.ToString();
    }
	
	// Update is called once per frame
	
    
}
