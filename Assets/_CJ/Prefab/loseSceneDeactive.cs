using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loseSceneDeactive : MonoBehaviour {

    public GameObject self;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        self.SetActive(false);
	}
}
