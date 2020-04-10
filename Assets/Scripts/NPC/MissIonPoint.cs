using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissIonPoint : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag==Tags.player)
        {
            if(NPCMisson._instance.MissonIsGoing==true&&NPCMisson._instance.MissionCount==2)
            {
                NPCMisson._instance.MissonProcessCount++;
                Debug.Log("探索任务完成");
            }
        }
    }
}
