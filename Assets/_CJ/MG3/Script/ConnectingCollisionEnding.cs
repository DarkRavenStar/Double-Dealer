using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectingCollisionEnding : MonoBehaviour
{
    public static bool ConnectionEnd = false;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe"))
        {
            ConnectionEnd = true;
            Debug.Log("ConnectionEnd = true");
        }    
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        ConnectionEnd = false;
        Debug.Log("ConnectionEnd = false");
    }
}
