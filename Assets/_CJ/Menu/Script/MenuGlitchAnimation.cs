using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuGlitchAnimation : MonoBehaviour {

    public Animator anim;
    int randNum;
    int interval = 4;
    int nextTime = 1;

	// Use this for initialization
	void Start () {
        anim = this.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if(Time.time >= nextTime)
        {
            RandomGlitch();

            nextTime += interval;
        }
    }

    void Glitch1()
    {
        anim.Play("Arrange1");
    }

    void Glitch2()
    {
        anim.Play("Arrange2");
    }

    void Glitch3()
    {
        anim.Play("Arrange3");
    }

    void RandomGlitch()
    {
        randNum = Random.Range(1, 3 + 1);
        Debug.Log(randNum);
        if(randNum == 1)
        {
            Glitch1();
        }
        else if(randNum == 2)
        {
            Glitch2();
        }
        else if(randNum == 3)
        {
            Glitch3();
        }
    }

}
