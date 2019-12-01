using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveSettingPage : MonoBehaviour {

    public GameObject volumeImg;
    public GameObject volumeSlider;
    public GameObject brightnessImg;
    public GameObject brightnessSlider;
    public GameObject returnButton;

	// Use this for initialization
	void Start () {
        volumeImg.SetActive(false);
        volumeSlider.SetActive(false);
        brightnessImg.SetActive(false);
        brightnessSlider.SetActive(false);
        returnButton.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
