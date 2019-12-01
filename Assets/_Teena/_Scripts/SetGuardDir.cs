using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGuardDir : MonoBehaviour {

    EnemyAnimManager anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<EnemyAnimManager>();
	}
	
	// Update is called once per frame
	void Update () {
        anim.forward = false;
        anim.backward = false;
        anim.right = true;
	}
}
