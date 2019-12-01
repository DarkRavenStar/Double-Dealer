using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckWinCondition : MonoBehaviour
{

    public Transform plyr;
    public GameObject obj;
    public GameObject pipe1;
    public GameObject pipe2;
    public GameObject pipe3;
    public GameObject pipe4;

    public Text display;

    public bool isEnd = false;

    void Start()
    {
        obj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

       // obj.transform.position = player.transform.position;
        // if connection1234 = true debug
        if (ConnectingCollisionEnding.ConnectionEnd == true && ConnectingCollision0.Connection0 == true)
        {
            if (ConnectingCollision1.Connection1 == true && ConnectingCollision2.Connection2 == true
                && ConnectingCollision3.Connection3 == true && ConnectingCollision4.Connection4 == true)
            {
                isEnd = true;
            }
            else if (ConnectingCollision1.Connection1 == false && ConnectingCollision2.Connection2 == true
                && ConnectingCollision3.Connection3 == true && ConnectingCollision4.Connection4 == true)
            {
                isEnd = false;
            }
            else if (ConnectingCollision1.Connection1 == true && ConnectingCollision2.Connection2 == false
                && ConnectingCollision3.Connection3 == true && ConnectingCollision4.Connection4 == true)
            {
                isEnd = false;
            }
            else if (ConnectingCollision1.Connection1 == true && ConnectingCollision2.Connection2 == true
                && ConnectingCollision3.Connection3 == false && ConnectingCollision4.Connection4 == true)
            {
                isEnd = false;
            }
            else if (ConnectingCollision1.Connection1 == true && ConnectingCollision2.Connection2 == true
                && ConnectingCollision3.Connection3 == true && ConnectingCollision4.Connection4 == false)
            {
                isEnd = false;
            }
        }
        else if (ConnectingCollisionEnding.ConnectionEnd == false && ConnectingCollision0.Connection0 == true)
        {
            isEnd = false;
        }
        else if (ConnectingCollisionEnding.ConnectionEnd == true && ConnectingCollision0.Connection0 == false)
        {
            isEnd = false;
        }
        else if (ConnectingCollisionEnding.ConnectionEnd == false && ConnectingCollision0.Connection0 == false)
        {
            isEnd = false;
        }

        if (isEnd == true)
        {
            display.text = "Password are 1234";
            plyr.GetComponent<Movement>().inMiniGame = false;
            obj.SetActive(false);

            pipe1.transform.Rotate(0, 0, 90);
            ConnectingCollision1.Connection1 = false;

            pipe2.transform.Rotate(0, 0, 90);
            ConnectingCollision2.Connection2 = false;

            pipe3.transform.Rotate(0, 0, 90);
            ConnectingCollision3.Connection3 = false;

            pipe4.transform.Rotate(0, 0, 90);
            ConnectingCollision4.Connection4 = false;

            ConnectingCollisionEnding.ConnectionEnd = false;
            ConnectingCollision0.Connection0 = false;

            Invoke("clearDisplay", 4);

            isEnd = false;
        }
    }

    void clearDisplay()
    {
        display.text = "";
    }

}