using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFaceUI : MonoBehaviour {

    public UILabel PlayerName;
    public UISprite Hp;
    public UISprite Mp;
    public UISprite Exp;
    public PlayerInfomation player;

    private void Update()
    {
        Hp.fillAmount = player.Hp_Remain/100;
        Mp.fillAmount = player.Mp_Remain / 100;
        Exp.fillAmount = player.Exp / 100;
        PlayerName.text = "Lv." + player.Level + " " + player.PlayerName;
   
    }
}
