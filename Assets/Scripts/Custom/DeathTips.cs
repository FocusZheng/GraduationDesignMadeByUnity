using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTips : MonoBehaviour {

    public static DeathTips _instance;
    public GameObject player;
    public Transform RebornPoint;
	// Use this for initialization
	void Awake () {
        _instance = this;
	}
	
	// Update is called once per frame
	
    public void RebornButton()
    {
        this.gameObject.SetActive(false);
        this.GetComponent<TweenPosition>().PlayReverse();
        player.GetComponent<PlayFight>().state = PlayFight.PlayerAnimState.ControlWalk;
        player.GetComponent<PlayerMoveToDir>().enabled = true;
        player.GetComponent<PlayerMove>().enabled = true;
        player.transform.position = RebornPoint.position;
        player.GetComponent<PlayerInfomation>().Hp_Remain = player.GetComponent<PlayerInfomation>().Hp / 2;
    }
  
}
