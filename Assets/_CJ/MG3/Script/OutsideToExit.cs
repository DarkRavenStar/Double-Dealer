using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideToExit : MonoBehaviour {

    public Transform player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        this.gameObject.SetActive(false);
        player.GetComponent<Movement>().inMiniGame = false;
    }
}
