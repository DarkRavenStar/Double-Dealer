using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayControl : MonoBehaviour {

	// Update is called once per frame
	void Update() {
        GetComponent<TextMesh>().text = clickControl.playerCode;
	}
}
