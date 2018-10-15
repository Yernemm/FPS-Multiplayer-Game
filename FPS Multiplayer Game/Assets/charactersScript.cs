using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersScript : MonoBehaviour {

    WeaponsScript wp;

    public Character soldier = new Character()
    {
        name = "Soldier",
        moveSpeed = 10,
        jumpForce = 3,
        mass = 120
    };

    private void Start()
    {
        wp = GetComponent<WeaponsScript>();

        soldier.weapon = wp.mainGun;
    }



}

public class Character
{
    public string name { get; set; }
    public float moveSpeed { get; set; }
    public float jumpForce { get; set; }
    public float mass { get; set; }
    public Weapon weapon { get; set; }
    public Ability ability1 { get; set; }
    public Ability ability2 { get; set; }

}
