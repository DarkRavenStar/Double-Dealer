using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageOpacity : MonoBehaviour {

    public Image img;
    public static float BrightnessAlphaValue = 0.25f;

    // Use this for initialization
    void Start()
    {
        img = this.GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Color tempColor = img.color;
        tempColor.a = BrightnessAlphaValue;
        img.color = tempColor;
    }
}
