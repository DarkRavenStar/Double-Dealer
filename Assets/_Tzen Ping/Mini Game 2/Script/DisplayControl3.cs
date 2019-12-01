using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayControl3 : MonoBehaviour {

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMesh>().text = clickControl.playerCode3;
    }
}
