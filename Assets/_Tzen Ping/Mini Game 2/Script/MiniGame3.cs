using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame3 : MonoBehaviour {

    public GameObject obj;
    public GameObject plyr;
    // Use this for initialization
    void Start()
    {
        obj.SetActive(false);
    }

    void Update()
    {

    }

    public void OnMouseDown()
    {
        obj.SetActive(false);
        plyr.GetComponent<Movement>().inMiniGame = false;
    }

}
