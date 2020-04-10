using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterChoice : MonoBehaviour {

    public GameObject[] HeroList;
    private GameObject[] HeroInstance;
    private int ListIndex=0;
    // Use this for initialization
    void Start() {
        HeroInstance = new GameObject[HeroList.Length];
        for (int i = 0; i < HeroList.Length; i++)
        {
            HeroInstance[i] = Instantiate(HeroList[i], transform.position, transform.rotation) as GameObject;
            if (i != 0)
            {
                HeroInstance[i].SetActive(false);
            }
        }
     

    }

    // Update is called once per frame
    void Update() {

    }
    public void NextButton()
    {
        ListIndex++;
        //if(ListIndex>HeroInstance.Length-1)
        //{
        //    ListIndex =0;
        //}
        ListIndex %= HeroInstance.Length;//ListIndex=3 HeroInstance.Length=4 (3+1)%4=0
        ShowHero(ListIndex);


    }
    public void PrevButton()
    {
        ListIndex--;

        if (ListIndex < 0)
        {
            ListIndex = HeroInstance.Length - 1;
        }
        ShowHero(ListIndex);


    }
    void ShowHero(int index)
    {
        for(int i=0;i<HeroList.Length;i++)
        {
            HeroInstance[i].SetActive(false);
            if(i==index)
            {
                HeroInstance[index].SetActive(true);
            }
            
        }
       
        
    }
}



