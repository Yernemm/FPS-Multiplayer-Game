using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class playerController : NetworkBehaviour {
    //This class handles basic player input and basic movement.

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
    bool debugMode = false;

    UIScript ui;

    public uint playerId;

    [SerializeField]
    float health;

    bool iconsInitiated;



    // Use this for initialization
    void Start () {

        ui = GetComponent<UIScript>();
        rb = GetComponent<Rigidbody>();
        gameController = GameObject.Find("Game Controller");
        chars = GetComponent<CharactersScript>();

        currentCharacter = chars.debug;
        moveSpeed = currentCharacter.moveSpeed;

        //Cannot instantiate the icons at the start because it creates a race condition.
        //The icons are loaded from a different class and can only be instantiated once the other class is loaded.
        //To solve this, a boolean is used to instantiate the icons on the first frame in the update procedure which runs after start.
        iconsInitiated = false;


        playerId = GetComponent<NetworkIdentity>().netId.Value; //Give the player a unique ID based on their network object ID.
        if (!isLocalPlayer)
        {
            //Disable the player camera if this is not the local player.
            playerCamera.SetActive(false);
            return;
        }
        
        //Set localPlayer to isLocalPlayer so other scripts can access this property.
        localPlayer = isLocalPlayer;
     
 
   
	}

	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
            return;

        //Initiate ability icons once.
        if (!iconsInitiated)
        {
            ui.setAbility1Sprite(currentCharacter.ability1.sprite);
            ui.setAbility2Sprite(currentCharacter.ability2.sprite);
            iconsInitiated = true;
        }

        ui.updateAbility1Cooldown(currentCharacter.ability1.cooldownCurrent);
        ui.updateAbility2Cooldown(currentCharacter.ability2.cooldownCurrent);

        //Movement below

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
            z += moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
            z -= moveSpeed * Time.deltaTime;
        if (Input.GetKey("d"))
            x += moveSpeed * Time.deltaTime;
        if (Input.GetKey("a"))
            x -= moveSpeed * Time.deltaTime;
        
        //Calculate a translation vector to where the player is moved next.
        Vector3 translation = new Vector3(x, 0, z);
        //Move the player by the calculated vector.

        //ClampMagnitude is used to ensure that the magnitude of the movement when adding two
        //direction vectors remains constant.
        transform.Translate(Vector3.ClampMagnitude(translation, moveSpeed * Time.deltaTime));
        //Change from a local direction vector to a world direction vector.
        Vector3 worldDirection = transform.TransformDirection(new Vector3(x,0,z ));


        //Abilities
        //Only use ability if it is not on cooldown.
        if (Input.GetKeyDown("q") && currentCharacter.ability1.offCooldown)
        {
            currentCharacter.ability1.use(rb);
        }

        if (Input.GetKeyDown("e") && currentCharacter.ability2.offCooldown)
            currentCharacter.ability2.use(rb);

        //Reload
        if (Input.GetKeyDown("r"))
            currentCharacter.weapon.reload();


        //Jump
        float jumpV = 0;
        if(Input.GetKeyDown("space"))
        {
            if (jumpTrigger.GetComponent<JumpColliderScript>().canJump)
            {
                jumpV = currentCharacter.jumpForce;
            }
            
        }

        //Mouse movement rotates player left and right.
        float xRot = Input.GetAxis("Mouse X") * camSens;
        rb.transform.eulerAngles += new Vector3(0,xRot,0);


        Vector3 planeV = Vector3.ClampMagnitude(new Vector3(velocity.x, 0, velocity.z), moveSpeed);
        //The player's velocity is set to the final calculated velocity.
        rb.velocity = new Vector3(worldDirection.x + rb.velocity.x, rb.velocity.y + jumpV, worldDirection.z + rb.velocity.z);

        //Info and cheats for development purposes.
        //debugMode is disabled for the final release.
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


        //Update UI
        ui.updateAmmo(currentCharacter.weapon.ammoCurrent, currentCharacter.weapon.ammoMax);
        ui.updateHealth(currentCharacter.healthCurrent, currentCharacter.healthMax);
        ui.updateScore(currentCharacter.score);

        //Check if player below minimum height. If so, kill player.
        if (transform.position.y < -20)
            GetComponent<HealthScript>().RpcHit(currentCharacter.healthCurrent);

        //Leave game with esc key.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isServer)
            {
                NetworkManager.singleton.StopHost();
            }
            else
            {
                NetworkManager.singleton.StopClient();
            }
        }
        }

    //FixedUpdate always runs at the same interval, regardless of framerate.
    private void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;
        //Add a drag force to simulate air resistance.
        //Multiply the player's velocity by 0.95 each interval.
        float dragMultiplier = 0.95f;
        Vector3 velocity = rb.velocity;
        velocity.x *= dragMultiplier;
        velocity.z *= dragMultiplier;
        rb.velocity = velocity;
    }
    //Damage has been moved to the characters script.
    public void damage(int amount)
    {
        currentCharacter.damage(amount);
    }

    public void initiateHealth()
    {

    }
    //Set the isReloading property of the player to false.
    public void reloadFinished()
    {
        currentCharacter.weapon.isReloading = false;
    }


}
