using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButtonisInInventory : MonoBehaviour {

    GameObject plyr;

    // Use this for initialization
    void Start()
    {
        plyr = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DisableMovement()
    {
        plyr.GetComponent<Movement>().inMiniGame = true;
    }
}
