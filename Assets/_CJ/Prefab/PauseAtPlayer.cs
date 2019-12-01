using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAtPlayer : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PauseFunction.isPause == true)
        {
            Time.timeScale = 0f;
        }
        else if (PauseFunction.isPause == false)
        {
            Time.timeScale = 1f;
        }
    }
}
