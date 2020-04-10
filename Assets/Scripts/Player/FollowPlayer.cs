using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
  
    private GameObject player;
    private Vector3 offsetPosition;
    private float distance = 0;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag(Tags.player);
        offsetPosition = transform.position - player.transform.position;
        
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = offsetPosition + player.transform.position;
        //视角左右转动
        RotateView();
        //ScollView 视角拉进拉远
        ScollView();
       


    }
    void ScollView()
    {
        distance = offsetPosition.magnitude;
        distance -= distance * Input.GetAxis("Mouse ScrollWheel");
        distance = Mathf.Clamp(distance, 2, 15);
        offsetPosition = distance * offsetPosition.normalized;
    }
    void RotateView()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            float angleX=Input.GetAxis("Mouse X");
            transform.RotateAround(player.transform.position,player.transform.up,angleX);

            Vector3 originalPosition = transform.position;//先将位置信息和旋转信息保存起来 在旋转之前保存
            Quaternion originalRotation = transform.rotation;

            float angleY = Input.GetAxis("Mouse Y");
            transform.RotateAround(player.transform.position,transform.right,-angleY);//影响的属性有两个 一个position 一个rotation

            float x = transform.eulerAngles.x;
            if(x<10||x>70)
            {
                transform.position = originalPosition;//如果超出范围则赋予原来的值
                transform.rotation = originalRotation; 
            }
            
        }
        offsetPosition = transform.position - player.transform.position; //rotation改变 Vector3类型的offsetPosition也改变 偏移量重新校正
        //由于这里重置了偏移量 所以ScollView调用顺序应该放在后面


    }
}
