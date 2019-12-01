using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    public GameObject sound;
    public void ChangeToScene(int changeSceneTo)
    {
        SceneManager.LoadScene(changeSceneTo);
    }
    private void Start()
    {
        sound = GameObject.Find("SoundManager");
    }
    public void OnMouseDown()
    {
        SoundManager.Instance.ClickSource.Play();
    }
    public void OnMouseOver()
    {
        SoundManager.Instance.MainMenuSource.Play();
    }
}
