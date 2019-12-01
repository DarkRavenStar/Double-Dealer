using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectingCollision4 : MonoBehaviour {

    public static bool Connection4 = false;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe"))
        {
            Connection4 = true;
            Debug.Log("Connection 4 = true");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Connection4 = false;
        Debug.Log("Connection 4 = true");
    }


}
