using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffItem : MonoBehaviour {

    public int id=0;
    private TweenAlpha tween;
    private float CurrentTime;
    private bool isTimeing=false;

	// Use this for initialization
	void Start () {

        
        tween = GetComponent<TweenAlpha>();
        
        this.GetComponent<UISprite>().enabled = false;
    }

    public void SetID(int id,float Bufftime)
    {
        this.id = id;
        SkillInfomation info = SkillInfo._instance.GetSkillInfoByID(id);
        this.GetComponent<UISprite>().enabled = true;
        this.GetComponent<UISprite>().spriteName = info.icon_name;
        this.isTimeing = true;
        CurrentTime = Bufftime;
    }
    void TimeOut()
    {
        CurrentTime = 0;
        isTimeing = false;
        this.id = 0;
        this.GetComponent<UISprite>().enabled = false;
        tween.enabled = false;
    }
   

    // Update is called once per frame
    void Update()
    {
        if(isTimeing)
        {
            CurrentTime -= Time.deltaTime;
            if(CurrentTime<=8f)
            {
                tween.enabled = true;
                if(CurrentTime<=0)
                {
                    TimeOut();

                }
            }
            
        }
    }
}
