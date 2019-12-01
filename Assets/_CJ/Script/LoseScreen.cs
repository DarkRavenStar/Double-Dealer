using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class LoseScreen : MonoBehaviour {
    public GameObject sound;
    private void Start()
    {
        sound = GameObject.Find("SoundManager");
    }
    private void Awake()
    {
        PauseFunction.isPause = false;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        SoundManager.Instance.musicSource.Stop();
    }
}
