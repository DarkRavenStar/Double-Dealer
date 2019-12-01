using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour {
    GameObject plyr;
    GameObject pipe;

	// Use this for initialization
	void Start () {
        plyr = GameObject.Find("Player");
        pipe = GameObject.Find("PipeBG");
    }
	
	// Update is called once per frame
	public void SetActiveFalse() {
        pipe.SetActive(false);
        plyr.GetComponent<Movement>().inMiniGame = false;
    }
}
