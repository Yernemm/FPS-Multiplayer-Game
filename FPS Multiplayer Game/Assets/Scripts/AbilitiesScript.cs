using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AbilitiesScript : NetworkBehaviour {

    [SerializeField]
    GameObject cameraObject;

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
        cooldownMax = 5
       
    };

   void Start()
    {
        dash.use = useDash;
       
        
    }
}



public class Ability
{
    public string name { get; set; }
    public float cooldownMax { get; set; }
    public float cooldownCurrent { get; set; }
    public bool offCooldown { get; set; }
    public delegate bool useDelegate(Rigidbody rb);
    public useDelegate use;
    public Ability()
    {
        cooldownCurrent = 0;
        offCooldown = true;
    }
}