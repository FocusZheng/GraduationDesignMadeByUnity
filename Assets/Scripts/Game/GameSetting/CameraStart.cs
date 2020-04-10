using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStart : MonoBehaviour {

    private float speed = 1f;
    private float endPosZ = 103;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.localPosition = new Vector3(transform.position.x, transform.position.y, Mathf.Lerp(transform.position.z, endPosZ, speed * Time.deltaTime));
        //if(Mathf.Abs(transform.position.z-endPosZ)>0.02f)
        //transform.Translate(Vector3.forward * speed*Time.deltaTime*10);
    }
}
