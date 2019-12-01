using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkipTutorial : MonoBehaviour {
    public Button btn;
    public GameObject sound;

    private void Start()
    {
        sound = GameObject.Find("SoundManager");

    }

    public void Skip() {
        GameObject.Find("Player").GetComponent<Movement>().MovementOnPlayer = true;
		GameObject.Find("Player").GetComponent<Transform> ().position = new Vector3 (100f, 100f, 0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnMouseDown()
    {
        SoundManager.Instance.OpenMenuSource.Play();
    }
}
