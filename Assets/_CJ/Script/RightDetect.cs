using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightDetect : MonoBehaviour {

    public static bool isHiding = false;
    public static bool HideRight = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isHiding == true && HideRight == true)
        {
            Debug.Log("Player Hiding Right");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            isHiding = true;
            HideRight = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            isHiding = false;
            HideRight = false;
        }
    }
}
