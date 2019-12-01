using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveCreditScene : MonoBehaviour {
    public GameObject A1;
    public GameObject A2;
    public GameObject A3;
    public GameObject A4;
    public GameObject A5;
    public GameObject A6;
    public GameObject A7;
    public GameObject A8;
    public GameObject CreditTitle;

	// Use this for initialization
	void Start () {

        A1.SetActive(false);
        A2.SetActive(false);
        A3.SetActive(false);
        A4.SetActive(false);
        A5.SetActive(false);
        A6.SetActive(false);
        A7.SetActive(false);
        A8.SetActive(false);
        CreditTitle.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
