using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characters : MonoBehaviour {

    public Character soldier = new Character()
    {
        name = "Soldier",
        moveSpeed = 12,
        jumpForce = 12,
        mass = 12
    };

 

}

public class Character
{
    public string name { get; set; }
    public float moveSpeed { get; set; }
    public float jumpForce { get; set; }
    public float mass { get; set; }


}
