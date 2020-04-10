using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickGrid : MonoBehaviour {

    public int id;
    public UILabel QuickNum;//按键号
    public int num;
    public KeyCode key;
    private SkillInfomation skillinfo = null;
    private float ColdTimeValue;
    private UISprite ColdTime;
    private float QuickGridTimer = 0;
    public bool isColding;
    private UILabel ColdTimeText;
    private float CurrentTime;
    public enum QuickGridType
    {
        Drug,
        Skill,
    }
    public QuickGridType quickType=QuickGridType.Drug;
    private void Awake()
    {
        
        num = int.Parse(QuickNum.text);
    }
    private void Start()
    {
        isColding = false;

    }
    
    public void GetColdTime(float coldtime)
    {
         ColdTime = GetComponentInChildren<QuickItem>().transform.Find("ColdTime").GetComponent<UISprite>();
        ColdTimeText= GetComponentInChildren<QuickItem>().transform.Find("ColdTimeValue").GetComponent<UILabel>();
        ColdTimeValue = coldtime;
        
    }
       
    void ColdTimeUpdate()
    {
        ColdTime.fillAmount = 1-((ColdTimeValue-CurrentTime  ) / ColdTimeValue);
        ColdTimeText.text = ((int)CurrentTime).ToString();
     
    }
    public void StartColding()
    {
        isColding = true;
        CurrentTime = ColdTimeValue;
    }
    void Update()
    {
        if(isColding==true)
        {
            
            ColdTimeUpdate();
            CurrentTime -= Time.deltaTime;
            if (CurrentTime <= 0)
            {
                CurrentTime = 0;
                isColding = false;
                ColdTimeText.text = null;
            }
           
        }
        if (Input.GetKeyDown(key)) //如果按下快捷栏快捷键
        {
               
            if (quickType==QuickGridType.Drug&&this.id!=0&&isColding==false)
            {
                InventoryGrid[] grids = FindObjectsOfType<InventoryGrid>();

                foreach(InventoryGrid grid in grids)
                {
                    if(grid.id==this.id)
                    {
                      
                        grid.MinObject();
                        if(grid.num==0)
                        {
                            Destroy(this.GetComponentInChildren<QuickItem>().gameObject);
                            this.id = 0;
                            Debug.Log("药品使用完了");
                            break;
                        }
                        //DrugIsExist();
                        Debug.Log("使用成功");
                       
                    }
                }
            }
            else if(quickType==QuickGridType.Skill&&this.id!=0&&isColding==false)
            {
                PlayFight._instance.UseSkill(this.id);
            


            }
        }
    }


    

}
