using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AbilitiesScript : NetworkBehaviour {

    //This class provides the behaviour for the abilities.

    [SerializeField]
    GameObject cameraObject;

    [SerializeField]
    Sprite dashSprite;

    

    //Function for using the dash ability.
    //return true if used.
    public bool useDash(Rigidbody rb) {
        Debug.Log("using");
        Camera camera = cameraObject.GetComponent<Camera>();
        Vector3 initVelocity = rb.velocity;
        initVelocity += camera.transform.forward * 20f; //dash forward
        Debug.Log(initVelocity);
        rb.velocity = initVelocity;
        //Play camera zoom animation.
        Animator anim = cameraObject.GetComponent<Animator>();
        anim.Play("CameraDashAbility", 0, 0);
        return true;
    }
    //Define the dash ability
    public Ability dash = new Ability()
    {
        name = "Dash",
        cooldownMax = 5,
       
    };
    //set the basic properties for dash.
   void Start()
    {
        dash.useDel = useDash;
        dash.sprite = dashSprite;         
    }
    //Update the cooldown time for dash.
    private void Update()
    {
        tickCooldown(dash, Time.deltaTime); 
    }
    //Recursive procedure for the cooldown timer.
    void tickCooldown(Ability ab, float timeChange)
    {
        //Subtract the time since the previous frame every frame.
        if (ab.cooldownCurrent > 0) {
            ab.cooldownCurrent -= timeChange;
            //If the resultant cooldown is below 0, set the cooldown to 0.
            if (ab.cooldownCurrent < 0)
            {
                ab.cooldownCurrent = 0;
                //At this point the cooldown is 0. Call itself again to fall into the else block below.
                tickCooldown(ab, 0);
            }
        }
        else
        {
        //If the cooldown is 0, set the off cooldown boolean to true.
            if (!ab.offCooldown)
                ab.offCooldown = true;
        }
    }
}



public class Ability
{
    //This class defines an ability.
    //Each character can have 2 abilities.
    public string name { get; set; }
    public float cooldownMax { get; set; }
    public float cooldownCurrent { get; set; }
    public bool offCooldown { get; set; }
    public delegate bool useDelegate(Rigidbody rb);
    public useDelegate useDel;
    public bool use(Rigidbody rb)
    {
        //Put the ability on cooldown when it is used.
        if (offCooldown)
        {
            useDel(rb);
            cooldownCurrent = cooldownMax;
            offCooldown = false;
            return true;
        }
        else
        {
            return false;
        }
    }
    //The image sprite for the ability.
    public Sprite sprite;
    public Ability()
    {
        //The ability is off cooldown when it is first instantiated.
        cooldownCurrent = 0;
        offCooldown = true;
    }
}