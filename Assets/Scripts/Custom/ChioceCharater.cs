using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChioceCharater : MonoBehaviour {

    private UIInput uiput;
	// Use this for initialization
	void Start () {
        uiput = GameObject.Find("Input").GetComponent<UIInput>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OKClick()
    {
        PlayerPrefs.SetString("Name", uiput.value);
        SceneManager.LoadScene("GameStart");
    }
}
