using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayControl2 : MonoBehaviour {

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMesh>().text = clickControl.playerCode2;
    }
}
