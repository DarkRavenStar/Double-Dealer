using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCodeInUI : MonoBehaviour {

    public GameObject codeDisplay;

	// Use this for initialization
	void Start () {
        //codeDisplay = GameObject.Find("ShowCodeInUi");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Spawn()
    {
        if(EndDetect.isCodeAvailable == true)
        {
            codeDisplay.SetActive(true);
        }
    }

    public void Deactive()
    {
        codeDisplay.SetActive(false);
    }
}
