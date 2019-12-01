using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectingCollision2 : MonoBehaviour {

    public static bool Connection2 = false;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe"))
        {
            Connection2 = true;
            Debug.Log("Connection 2 = true");
        }
            
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Connection2 = false;
        Debug.Log("Connection 2 = false");
    }


}
