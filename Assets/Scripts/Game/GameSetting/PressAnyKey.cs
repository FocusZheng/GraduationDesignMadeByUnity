using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyKey : MonoBehaviour {

    private bool isAnyKeyDowm = false;
    private GameObject ButtonContainer;
    private void Start()
    {
        ButtonContainer = this.transform.parent.Find("ButtonContain").gameObject;
    }
    private void Update()
    {
        if(isAnyKeyDowm==false)
        {
            if(Input.anyKey)
            {
                ButtonContainer.SetActive(true);
                this.transform.gameObject.SetActive(false);
                isAnyKeyDowm = true;
            }
        }
    }
}
