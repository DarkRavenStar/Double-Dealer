using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pipe1_0 : MonoBehaviour {

    public Button btn;
    public static float NotConnectOpacity = 0.25f;
    public static float ConnecttedOpacity = 1f;
    public static bool Pipe1_0isConnect = false;

    private bool canConnect = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Pipe1_1.Pipe1_1isConnect == true)
        {
            canConnect = true;
        }
        else if (Pipe1_1.Pipe1_1isConnect == false)
        {
            canConnect = false;
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (canConnect == true)
        {
            if (collision.gameObject.CompareTag("Pipe(1,1)"))
            {
                Pipe1_0isConnect = true;
                Color tempColor = btn.image.color;
                tempColor.a = ConnecttedOpacity;
                btn.image.color = tempColor;
            }
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe(1,1)"))
        {
            Pipe1_0isConnect = false;
            Color tempColor = btn.image.color;
            tempColor.a = NotConnectOpacity;
            btn.image.color = tempColor;
        }
    }

}
