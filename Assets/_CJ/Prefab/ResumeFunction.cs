using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeFunction : MonoBehaviour {

    GameObject plyr;

    // Use this for initialization
    void Start()
    {
        plyr = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PauseGame()
    {
        PauseFunction.isPause = false;
        plyr.GetComponent<Movement>().inMiniGame = false;
        Time.timeScale = 1f;
    }
}
