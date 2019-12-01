using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComicFinish : MonoBehaviour {
    public Animator anim;
    public GameObject Fadeout;

    float Timer;

    // Use this for initialization
    void Start () {
        anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Camera_Intro")) {
            Timer += Time.deltaTime * 2;
            Fadeout.GetComponent<SpriteRenderer>().color = new Color(0,0,0, Timer);
        }

        if (Timer >= 1) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
