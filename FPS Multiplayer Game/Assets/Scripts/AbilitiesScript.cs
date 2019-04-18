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

    


    public bool useDash(Rigidbody rb) {
        Debug.Log("using");
        Camera camera = cameraObject.GetComponent<Camera>();
        Vector3 initVelocity = rb.velocity;
        initVelocity += camera.transform.forward * 20f;
        Debug.Log(initVelocity);
        rb.velocity = initVelocity;

        Animator anim = cameraObject.GetComponent<Animator>();
        anim.Play("CameraDashAbility", 0, 0);
        return false;
    }

    public Ability dash = new Ability()
    {
        name = "Dash",
        cooldownMax = 5,
       
    };

   void Start()
    {
        dash.useDel = useDash;
        dash.sprite = dashSprite;
       
        
    }

    private void Update()
    {
        tickCooldown(dash, Time.deltaTime); 
    }

    void tickCooldown(Ability ab, float timeChange)
    {
        //Subtract the time since the previous frame every frame.
        if (ab.cooldownCurrent > 0) {
            ab.cooldownCurrent -= timeChange;
            //If the resultant cooldown is below 0, set the cooldown to 0.
            if (ab.cooldownCurrent < 0)
            {
                ab.cooldownCurrent = 0;
                //At this point the cooldown is 0. Call itself to fall into the else block below.
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
    public string name { get; set; }
    public float cooldownMax { get; set; }
    public float cooldownCurrent { get; set; }
    public bool offCooldown { get; set; }
    public delegate bool useDelegate(Rigidbody rb);
    public useDelegate useDel;
    public bool use(Rigidbody rb)
    {
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
    public Sprite sprite;
    public Ability()
    {
        cooldownCurrent = 0;
        offCooldown = true;
    }
}