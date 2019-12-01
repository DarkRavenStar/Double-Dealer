using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class clickControl : MonoBehaviour {
    public Text num;
    public GameObject sound;
    public static string correctCode="7";
    public static string correctCode2 = "5";
    public static string correctCode3 = "9";
    public static string correctCode4 = "3";
    public static string playerCode ="";
    public static string playerCode2 = "";
    public static string playerCode3 = "";
    public static string playerCode4 = "";
    public static int totalDigits = 0;
    private void Start()
    {
        sound = GameObject.Find("SoundManager");
       // Debug.Log(totalDigits);
    }
    public void OnMouseDown()
    {
        SoundManager.Instance.ClickSource.Play();
        if (playerCode == "" && totalDigits == 0)
        {
            playerCode += gameObject.name;
            totalDigits++;
            num.text += playerCode;
}
        else if (playerCode2 == "" && totalDigits == 1)
        {
            playerCode2 += gameObject.name;
            totalDigits++;
            num.text += playerCode2;
        }
        else if (playerCode3 == "" && totalDigits ==2)
        {
            playerCode3 += gameObject.name;
            totalDigits++;
            num.text += playerCode3;
        }
        else if (playerCode4 == "" &&totalDigits ==3)
        {
            playerCode4 += gameObject.name;
            totalDigits++;
            num.text += playerCode4;
        }

    }
    private void Update()
    {
        //Debug.Log(totalDigits);
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
