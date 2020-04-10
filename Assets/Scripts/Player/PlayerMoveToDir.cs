using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveToDir : MonoBehaviour {

    private CharacterController cc;
    public float speed;
    private PlayerMove dir;
    private Animation player;
    private PlayFight fight;
    public enum PlayerMoveState
    {
        Move,
        Idle,
        
    }
    public PlayerMoveState playerMove;
    // Use this for initialization
    void Start () {
        
        cc = this.transform.GetComponent<CharacterController>();
        dir = this.transform.GetComponent<PlayerMove>();
        player = this.transform.GetComponent<Animation>();
        fight = this.GetComponent<PlayFight>();
        speed = player.GetComponent<PlayerInfomation>().Speed;
        playerMove = PlayerMoveState.Idle;
    }
	
	// Update is called once per frame
	void Update () {
     
        if(Vector3.Distance(dir.targetPosition, transform.position) > 0.3f)
        {
           
            playerMove = PlayerMoveState.Move;

        }
        else 
        {
            
            playerMove = PlayerMoveState.Idle;
            
        }
        if (fight.state == PlayFight.PlayerAnimState.ControlWalk)
        {
            if (playerMove == PlayerMoveState.Move)
            {
                
                cc.SimpleMove(transform.forward * speed);
                PlayerAnimPlay("Walk");
            }
            else if (playerMove == PlayerMoveState.Idle)
            {

                PlayerAnimPlay("Idle");
            }
        }
        else if (fight.state == PlayFight.PlayerAnimState.NormalAttack)
        {
            if (fight.attack_state == PlayFight.AttackState.Moving)
            {
                
                PlayerAnimPlay("Walk");
            }
        }
     

    }

    void PlayerAnimPlay(string animName)
    {
        player.CrossFade(animName);
    }
}
