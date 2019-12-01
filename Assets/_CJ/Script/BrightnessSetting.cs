using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessSetting : MonoBehaviour {

    public Image img;
    public Slider BrightnessSlider;
    private float OpacityAlphaValue;

    // Use this for initialization
    void Start()
    {
        BrightnessSlider.minValue = 0f;
        BrightnessSlider.maxValue = 0.5f;
        OpacityAlphaValue = ImageOpacity.BrightnessAlphaValue;
        BrightnessSlider.value = OpacityAlphaValue;
    }

    // Update is called once per frame
    void Update()
    {
        OpacityAlphaValue = BrightnessSlider.value;
        Color tempColor = img.color;
        tempColor.a = OpacityAlphaValue;
        img.color = tempColor;
        ImageOpacity.BrightnessAlphaValue = OpacityAlphaValue;
    }
}
