using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {

    //Player rigid body will be set in Unity.
    public Rigidbody player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Look at the player every frame.
        transform.LookAt(player.transform);
	}
}
