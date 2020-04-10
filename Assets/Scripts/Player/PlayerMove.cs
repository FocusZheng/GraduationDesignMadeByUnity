using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public GameObject Click_Effcet;
    private bool isMouse = false;
    [HideInInspector]
    public Vector3 targetPosition;
    



    // Use this for initialization
    void Start () {
        targetPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        
		if(Input.GetMouseButtonDown(1) && UICamera.isOverUI == false)//检测是否鼠标点击时 有UI存在  &&UICamera.hoveredObject==null ????????
        {
            GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayFight>().state = PlayFight.PlayerAnimState.ControlWalk;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitinfo;
            bool iscollider = Physics.Raycast(ray,out hitinfo);
            if(iscollider&&hitinfo.collider.tag==Tags.ground)
            {
                GameObject.Instantiate(Click_Effcet, hitinfo.point, Quaternion.identity);
                this.transform.LookAt(new Vector3 (hitinfo.point.x,transform.position.y,hitinfo.point.z));
                targetPosition = new Vector3(hitinfo.point.x, hitinfo.point.y+0.1f, hitinfo.point.z);
               
                isMouse = true;
            }
        }
     

     
	}
}
