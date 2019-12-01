using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomDetect : MonoBehaviour {


    public static bool isHiding = false;
    public static bool HideBottom = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isHiding == true && HideBottom == true)
        {
            Debug.Log("Player Hiding Bottom");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            isHiding = true;
            HideBottom = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            isHiding = false;
            HideBottom = false;
        }
    }
}
