using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfKill : MonoBehaviour {

    //Any object that this script is placed on will destroy itself after a set amount of time.

    public float lifeTime;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, lifeTime);
    }

}
