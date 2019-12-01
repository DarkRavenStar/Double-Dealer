using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour {
    ulong vidFrameCount;

    float counter = 1f;
    bool triggeredDial = false;

    VideoPlayer Vid;
    public GameObject Player;
    public GameObject Tutorial;

    void Start () {
        Vid = GetComponent<VideoPlayer>();
        vidFrameCount = Vid.frameCount;
		Player = GameObject.Find ("Player");
        Player.GetComponent<Movement>().MovementOnPlayer = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Vid.frame > (long) vidFrameCount - 60 && counter >= 0.1f) {
            Vid.targetCameraAlpha = counter;
            counter -= 0.4f * Time.deltaTime;
        } else if(counter <= 0.1f){
            Player.GetComponent<Movement>().MovementOnPlayer = true;
            Tutorial.GetComponent<DialogueTrigger>().TriggerDialogue();
            gameObject.SetActive(false);
        }
	}
}
