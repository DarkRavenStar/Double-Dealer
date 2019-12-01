using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetControl : MonoBehaviour {
    public Text num;
    public Text num1;
    public GameObject sound;

    private void Start()
    {
        sound = GameObject.Find("SoundManager");
    }

    public void OnMouseDown()
    {
        clickControl.totalDigits = 0;
        clickControl.playerCode ="";
        clickControl.playerCode2 = "";
        clickControl.playerCode3 = "";
        clickControl.playerCode4 =  "";
        num.text = "";
        num1.text = "";
        SoundManager.Instance.ClickSource.Play();
    }
    void OnMouseOver()
    {
        GetComponent<SpriteRenderer>().color = new Color32(254, 152, 203, 255);
    }
    void OnMouseExit()
    {

        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
    }
}
