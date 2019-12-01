using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopRightPropDetectEnemy : MonoBehaviour {

    public GameObject RealPov;
    public GameObject FakePov;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            Debug.Log("ENEMY ENTER");
            RealPov.SetActive(false);
            FakePov.SetActive(true);
        }
        if (collision.transform.CompareTag("Other"))
        {
            RealPov.SetActive(true);
            FakePov.SetActive(false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            RealPov.SetActive(true);
            FakePov.SetActive(false);
        }
        if (collision.transform.CompareTag("Other"))
        {
            RealPov.SetActive(true);
            FakePov.SetActive(false);
        }
    }
}
