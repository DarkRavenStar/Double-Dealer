using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectingCollision3 : MonoBehaviour {

    public static bool Connection3 = false;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe"))
        {
            Connection3 = true;
            Debug.Log("Connection 3 = true");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Connection3 = false;
        Debug.Log("Connection 3 = false");
    }


}
