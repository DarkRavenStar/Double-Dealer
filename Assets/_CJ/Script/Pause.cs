using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    bool isPause = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        isPause = PauseFunction.isPause;
        if(isPause == true)
        {
            Time.timeScale = 0f;
        }
        else if(isPause == false)
        {
            Time.timeScale = 1f;
        }
	}
}
