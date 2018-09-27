using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
    //Public float which controls the move speed.
    //It is public so it can be edited from Unity itself.
    public float moveSpeed; 
    //A rigid body is the component on which physics calculations can be done.
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        //Create a copy of the player's velocity vector.
        Vector3 velocity = rb.velocity;

        //If the movement keys are pressed, add the move speed to the appropriate
        //direction in the vector.
        if (Input.GetKey("w"))
        {
            //Time.deltaTime is the time since the previous frame.
            //This is multiplied here to prevent the player from moving faster at higher framerates.
            //It ensures a constant move speed regardless of framerate.
            velocity.z -= moveSpeed * Time.deltaTime;

        }

        //The player's velocity is set to the final calculated velocity.
        rb.velocity = velocity;
        }
}
