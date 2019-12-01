using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour {

	public enum VOLUME_TYPE
    {
        NULL = 0,
        SFX,
        BGM
    }

    public VOLUME_TYPE volumeType;
    // Use this for initialization
	void Start () {
        //soundManager = GameObject.FindGameObjectWithTag("SoundManager");
        if (volumeType == VOLUME_TYPE.SFX)
        {
            GetComponent<Slider>().value = SoundManager.sfxSliderValue;
            SoundManager.Instance.VolumeSFX = this.gameObject.GetComponent<Slider>();
        }

        if (volumeType == VOLUME_TYPE.BGM)
        {
            GetComponent<Slider>().value = SoundManager.bgmSliderValue;
            SoundManager.Instance.VolumeBGM = this.gameObject.GetComponent<Slider>();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
