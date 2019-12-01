using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MiniGame2 : MonoBehaviour {

    public GameObject obj;
    public Text disply;
    public Text num;
    public GameObject plyr;
    // Use this for initialization
    void Start () {
        obj.SetActive(false);
	}

    void Update()
    {

    }

    public void OnMouseDown()
    {
        obj.SetActive(false);
        clickControl.totalDigits = 0;
        num.text = "";
        disply.text = "";
        plyr.GetComponent<Movement>().inMiniGame = false;
    }

}
