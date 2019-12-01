using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectingCollision1 : MonoBehaviour {

    public static bool Connection1 = false;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe"))
        {
            Connection1 = true;
            Debug.Log("Connection 1 = true");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Connection1 = false;
        Debug.Log("Connection 1 = false");
    }


}
