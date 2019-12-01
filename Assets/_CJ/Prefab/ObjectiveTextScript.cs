using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTextScript : MonoBehaviour {

    public GameObject obj;
    bool canShow = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        canShow = EndDetect.isCodeAvailable;

		if(canShow == true)
        {
            obj.SetActive(true);
        }
        else
        {
            obj.SetActive(false);
        }
	}
}
