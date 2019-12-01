using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitAnimation : MonoBehaviour {

    public Animator anim;
    int randNum;
    int interval = 3;
    int nextTime = 1;

    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextTime)
        {
            RandomFade();

            nextTime += interval;
        }
    }

    void Fade1()
    {
        anim.Play("Exit1");
    }

    void Fade2()
    {
        anim.Play("Exit2");
    }

    void RandomFade()
    {
        randNum = Random.Range(1, 2 + 1);
        Debug.Log(randNum);
        if (randNum == 1)
        {
            Fade1();
        }
        else if (randNum == 2)
        {
            Fade2();
        }
    }

}
