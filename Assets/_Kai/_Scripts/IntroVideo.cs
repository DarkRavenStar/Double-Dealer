using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroVideo : MonoBehaviour {

    ulong vidFrameCount;
    public GameObject sound;
    VideoPlayer Vid;

    void Start() {
        Vid = GetComponent<VideoPlayer>();
        vidFrameCount = Vid.frameCount;
        Vid.loopPointReached += CheckOver;
        sound = GameObject.Find("SoundManager");
    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp) {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SoundManager.Instance.musicSource.Play();
    }

    public void SkipVideo() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        SoundManager.Instance.musicSource.Play();
    }
}
