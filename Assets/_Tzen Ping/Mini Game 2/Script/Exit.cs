using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour {
    public GameObject plyr;
    public GameObject minigame;
    public GameObject sound;
    // Use this for initialization
    void Awake () {
        plyr = GameObject.Find("Player");
    }
    private void Start()
    {
        sound = GameObject.Find("SoundManager");
    }
    // Update is called once per frame
    void Update () {
        
    }
    public void OnMouseDown()
    {
        Debug.Log("OpenMiniGame");
        SoundManager.Instance.ClickSource.Play();
		EnterControl.isEnter = false;
        clickControl.totalDigits = 0;
        clickControl.playerCode = "";
        clickControl.playerCode2 = "";
        clickControl.playerCode3 = "";
        clickControl.playerCode4 = "";
		plyr.GetComponent<Movement>().inMiniGame = false;
		minigame.transform.GetChild(0).gameObject.SetActive(false);
    }
}
