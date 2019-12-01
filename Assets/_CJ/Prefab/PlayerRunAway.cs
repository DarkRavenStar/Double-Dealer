using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRunAway : MonoBehaviour {

    public Animator anim;
    public int JustOnce = 0;
    public GameObject player;
    public static bool oneTime = false;
    public static bool oneMoTime = false;
    float animTimer;

    // Use this for initialization
	void Start () {
        anim = this.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (JustOnce == 0)
        {
            if ( oneTime == false && EnemyDetectionTile.PlayerGetCatch == true)
            {
                oneTime = true;
            }

            if (oneTime == true && oneMoTime == false)
            {
                SmokeBombAnimation();
                oneMoTime = true;
            }

            if(oneMoTime == true)
            {
                animTimer += Time.deltaTime;
                if (animTimer >= 1.5)
                {
                    LoadLoseScreen();
                }
            }
        }
    }

    void LoadLoseScreen()
    {
        SceneManager.LoadScene(4);
        oneMoTime = false;
    }

    void SmokeBombAnimation()
    {
        anim.Play("SmokeBomb");
    }
}
