using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAnimManager : MonoBehaviour {

    Animator anim;

	void Start ()
    {
        anim = GetComponent<Animator>();
	}
	
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.I))
        {
            anim.SetInteger("State", 0);
        }
        if(Input.GetKeyUp(KeyCode.I))
        {
            anim.SetInteger("State", -1);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            anim.SetInteger("State", 2);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            anim.SetInteger("State", 3);
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            anim.SetInteger("State", 4);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            anim.SetInteger("State", 5);
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            anim.SetInteger("State", 6);
        }
    }
}
