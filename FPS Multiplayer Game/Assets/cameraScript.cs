using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {

    //Player rigid body will be set in Unity.
    public Rigidbody player;
    Vector3 posOffset;
    

	// Use this for initialization
	void Start () {
        posOffset = new Vector3(0, 1, 0);
	}
	
	// Update is called once per frame
	void Update () {
        //Look at the player every frame.
        //transform.LookAt(player.transform);
        
    }

    private void LateUpdate()
    {
        transform.position = player.position + posOffset;
        transform.rotation = player.rotation;
    }
}
