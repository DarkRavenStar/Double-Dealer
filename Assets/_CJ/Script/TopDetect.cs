using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDetect : MonoBehaviour {

    public static bool isHiding = false;
    public static bool HideTop = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isHiding == true && HideTop == true)
        {
            Debug.Log("Player Hiding Top");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            isHiding = true;
            HideTop = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            isHiding = false;
            HideTop = false;
        }
    }
}
