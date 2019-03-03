﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class playerController : NetworkBehaviour {
    //Public float which controls the move speed.
    //It is public so it can be edited from Unity itself.
    float moveSpeed; 
    //A rigid body is the component on which physics calculations can be done.
    Rigidbody rb;
    GameObject gameController;
    [SerializeField]
    GameObject playerCamera;

    [SerializeField]
    Collider jumpTrigger;

    public Transform gunSpawnPosition;


    CharactersScript chars;
    public Character currentCharacter;
    public float camSens;
    public bool localPlayer;
    bool debugMode = true;

    UIScript ui;

    public uint playerId;

    [SerializeField]
    float health;

    bool iconsInitiated;


    // Use this for initialization
    void Start () {
        playerId = GetComponent<NetworkIdentity>().netId.Value;
        if (!isLocalPlayer)
        {
            playerCamera.SetActive(false);
            return;
        }
        
        //Set localPlayer to isLocalPlayer so other scripts can access this property.
        localPlayer = isLocalPlayer;
     
        ui = GetComponent<UIScript>();
        rb = GetComponent<Rigidbody>();
        gameController = GameObject.Find("Game Controller");
        chars = GetComponent<CharactersScript>();

        currentCharacter = chars.debug;
        moveSpeed = currentCharacter.moveSpeed;
        Debug.Log(moveSpeed);

        iconsInitiated = false;
   
	}

	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
            return;

        if (!iconsInitiated)
        {
            ui.setAbility1Sprite(currentCharacter.ability1.sprite);
            ui.setAbility2Sprite(currentCharacter.ability2.sprite);
            iconsInitiated = true;
        }

        ui.updateAbility1Cooldown(currentCharacter.ability1.cooldownCurrent);
        ui.updateAbility2Cooldown(currentCharacter.ability2.cooldownCurrent);

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
            //Debug.Log("MovingForward");
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
        Vector3 worldDirection = transform.TransformDirection(new Vector3(x,0,z ));


        if (Input.GetKeyDown("q") && currentCharacter.ability1.offCooldown)
        {
            Debug.Log("before using");
            Debug.Log(currentCharacter.name);
            currentCharacter.ability1.use(rb);
        }

        if (Input.GetKeyDown("e") && currentCharacter.ability2.offCooldown)
            currentCharacter.ability2.use(rb);

        if (Input.GetKeyDown("r"))
            currentCharacter.weapon.reload();

        float jumpV = 0;
        if(Input.GetKeyDown("space"))
        {
            // Debug.DrawRay(GetComponent<Collider>().transform.position, Vector3.down, Color.red, 30);
            // if (Physics.Raycast(GetComponent<Collider>().transform.position, Vector3.down, (GetComponent<Collider>().bounds.size.y / 2) + 0.05f))
            if (jumpTrigger.GetComponent<JumpColliderScript>().canJump)
            {
                Debug.Log("Jump " + currentCharacter.jumpForce);
                jumpV = currentCharacter.jumpForce;
            }
            
        }

        float xRot = Input.GetAxis("Mouse X") * camSens;

        rb.transform.eulerAngles += new Vector3(0,xRot,0);


        Vector3 planeV = Vector3.ClampMagnitude(new Vector3(velocity.x, 0, velocity.z), moveSpeed);
        //The player's velocity is set to the final calculated velocity.
        rb.velocity = new Vector3(worldDirection.x + rb.velocity.x, rb.velocity.y + jumpV, worldDirection.z + rb.velocity.z);

        if (debugMode)
        {
            if (Input.GetKeyDown("p"))
            {
                rb.velocity = new Vector3(0, 0, 0);
                rb.position = new Vector3(0, 2, 0);
            }
            float sp = rb.velocity.magnitude;
            if (sp < 0.01)
                sp = 0;
            gameController.GetComponent<GameController>().addDebugText("Speed: " + sp);
        }


        ui.updateAmmo(currentCharacter.weapon.ammoCurrent, currentCharacter.weapon.ammoMax);

        ui.updateHealth(currentCharacter.healthCurrent, currentCharacter.healthMax);
        }

    private void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;
        float dragMultiplier = 0.95f;
        Vector3 velocity = rb.velocity;
        velocity.x *= dragMultiplier;
        velocity.z *= dragMultiplier;
        rb.velocity = velocity;
    }

    public void damage(float amount)
    {
        health -= amount;
    }

    public void initiateHealth()
    {

    }

    public void reloadFinished()
    {
        currentCharacter.weapon.isReloading = false;
    }


}
