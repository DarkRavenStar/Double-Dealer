using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectingCollision0 : MonoBehaviour {


    public static bool Connection0 = false;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe"))
        {
            Connection0 = true;
            Debug.Log("Connection 0 = true");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Connection0 = false;
        Debug.Log("Connection 0 = false"); 
    }
}
