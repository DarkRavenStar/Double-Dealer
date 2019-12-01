using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToMainMenu : MonoBehaviour {

    GameObject plyr;

	// Use this for initialization
	void Start () {
        plyr = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeScene()
    {
        PauseFunction.isPause = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
        plyr.GetComponent<Movement>().inMiniGame = false;
    }
}
