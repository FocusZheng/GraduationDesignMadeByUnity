using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void UIonClickShow()
    {
        transform.gameObject.SetActive(true);

    }
    public void UIonClickHide()
    {
        transform.gameObject.SetActive(false);
    }

}
