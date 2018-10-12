using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesScript : MonoBehaviour {

}

public class Ability
{
    public string name { get; set; }
    public float cooldownMax { get; set; }
    public float cooldownCurrent { get; set; }
    public bool offCooldown { get; set; }
    public delegate bool useDelegate(Rigidbody rb);
    public useDelegate use;
}