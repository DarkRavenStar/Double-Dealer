using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseFunction : MonoBehaviour {

    public static bool isPause = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
    }

    public void PauseGame()
    {
        isPause = true;
    }
}
