﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftDetect : MonoBehaviour {

    public static bool isHiding = false;
    public static bool HideLeft = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isHiding == true && HideLeft == true)
        {
            Debug.Log("Player Hiding Left");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            isHiding = true;
            HideLeft = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            isHiding = false;
            HideLeft = false;
        }
    }
}
