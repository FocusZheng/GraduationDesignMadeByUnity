using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTool : MonoBehaviour {

    public static SkillTool _instance;
    private UIWidget SkillToolShip;
    private UILabel SkillText;
    private float TargetAlpha=0;
    public bool isHover=false;
    private float smoothing = 1;
    private Vector2 Offset = new Vector2(20, -20);
    public Camera UIrootCam;
    
    private void Awake()
    {
        _instance = this;
        SkillToolShip = this.GetComponent<UIWidget>();
        SkillText = this.GetComponentInChildren<UILabel>();
    }
    private void Update()
    {

        
       if(isHover)
        {
            if(SkillToolShip.alpha!=1)
            {
                SkillToolShip.alpha = Mathf.Lerp(SkillToolShip.alpha, 1, smoothing * Time.deltaTime);
                if(Mathf.Abs(SkillToolShip.alpha-1)<0.01f)
                {
                    SkillToolShip.alpha = 1;
                }                             
            }
            Vector2 positon;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find("UI Root").GetComponent<UIRoot>().transform as RectTransform, Input.mousePosition, UIrootCam, out positon);
            this.transform.localPosition = positon + Offset;
        }
       else
        {
            if(SkillToolShip.alpha!=0)
            {
                SkillToolShip.alpha = Mathf.Lerp(SkillToolShip.alpha, TargetAlpha, smoothing * Time.deltaTime);
                if(Mathf.Abs(SkillToolShip.alpha-0)<0.01f)
                {
                    SkillToolShip.alpha = 0;
                }
            }
        }
            
    }
    public void Show(string text)
    {
        isHover = true;
        SkillText.text = text;
    }
    public void Hide()
    {
        isHover = false;
        
    }
    

}
