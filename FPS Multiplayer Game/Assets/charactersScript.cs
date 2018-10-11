using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charactersScript : MonoBehaviour {

    public Character soldier = new Character()
    {
        name = "Soldier",
        moveSpeed = 10,
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
