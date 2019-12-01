using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayControl4 : MonoBehaviour {

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMesh>().text = clickControl.playerCode4;
    }
}
