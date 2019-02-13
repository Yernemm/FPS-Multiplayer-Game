using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesScript : MonoBehaviour {

    public bool useDash(Rigidbody rb) {
        Debug.Log("using");
        Camera camera = GameObject.Find("PlayerCamera").GetComponent<Camera>();
        Vector3 initVelocity = rb.velocity;
        initVelocity += camera.transform.forward * 20f;
        Debug.Log(initVelocity);
        rb.velocity = initVelocity;
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