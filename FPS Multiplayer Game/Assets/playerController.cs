﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
    //Public float which controls the move speed.
    //It is public so it can be edited from Unity itself.
    float moveSpeed; 
    //A rigid body is the component on which physics calculations can be done.
    Rigidbody rb;
    GameObject gameController;
    CharactersScript chars;
    public Character currentCharacter;
    public float camSens;
    
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        gameController = GameObject.Find("Game Controller");
        chars = gameController.GetComponent<CharactersScript>();

        currentCharacter = chars.debug;
        moveSpeed = currentCharacter.moveSpeed;
        Debug.Log(moveSpeed);
	}
	
	// Update is called once per frame
	void Update () {

        //Create a copy of the player's velocity vector.
        Vector3 velocity = rb.velocity;

        //If the movement keys are pressed, add the move speed to the appropriate
        //direction in the vector.

        float x = 0;
        float z = 0;

        if (Input.GetKey("w"))
        {
            //Time.deltaTime is the time since the previous frame.
            //This is multiplied here to prevent the player from moving faster at higher framerates.
            //It ensures a constant move speed regardless of framerate.
            //velocity.z -= moveSpeed * Time.deltaTime;
            Debug.Log("MovingForward");
            z += moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
            z -= moveSpeed * Time.deltaTime;
        if (Input.GetKey("d"))
            x += moveSpeed * Time.deltaTime;
        if (Input.GetKey("a"))
            x -= moveSpeed * Time.deltaTime;

        Vector3 translation = new Vector3(x, 0, z);
        transform.Translate(Vector3.ClampMagnitude(translation, moveSpeed * Time.deltaTime));



        if (Input.GetKeyDown("q") && currentCharacter.ability1.offCooldown)
            currentCharacter.ability1.use(rb);

        if (Input.GetKeyDown("e") && currentCharacter.ability2.offCooldown)
            currentCharacter.ability2.use(rb);

        if (Input.GetKeyDown("r"))
            currentCharacter.weapon.reload();

        float jumpV = 0;
        if(Input.GetKeyDown("space"))
        {
            if (Physics.Raycast(rb.transform.position, Vector3.down, (GetComponent<Collider>().bounds.size.y / 2) + 0.1f))
                jumpV = currentCharacter.jumpForce;
            
        }

        float xRot = Input.GetAxis("Mouse X") * camSens;

        rb.transform.eulerAngles += new Vector3(0,xRot,0);


        Vector3 planeV = Vector3.ClampMagnitude(new Vector3(velocity.x, 0, velocity.z), moveSpeed * Time.deltaTime);
        //The player's velocity is set to the final calculated velocity.
        rb.velocity = new Vector3(planeV.x, rb.velocity.y + jumpV, planeV.z);
        }
}
