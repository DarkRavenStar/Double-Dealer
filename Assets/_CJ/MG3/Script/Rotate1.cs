using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotate1 : MonoBehaviour {

    public Button btn;
    public GameObject sound;
    public void OnMouseDown()
    {
        this.gameObject.transform.Rotate(0, 0, 90);
    }

    private void Start()
    {
        sound = GameObject.Find("SoundManager");
    }
    // Update is called once per frame
    void Update ()
    {

	}

    public void Rotate()
    {
        //this.gameObject.transform.Rotate(0, 0, 90);
        btn.transform.Rotate(0, 0, 90);
        SoundManager.Instance.PipeGameRotateSource.Play();
    }
    
}
