using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CharactersScript : NetworkBehaviour
{

    WeaponsScript wp;
    AbilitiesScript ab;

    public Character debug = new Character(200)
    {
        name = "Debug Character",
        moveSpeed = 15,
        jumpForce = 6,
        mass = 60,
    };

    private void Start()
    {
        wp = GetComponent<WeaponsScript>();
        ab = GetComponent<AbilitiesScript>();
        debug.weapon = wp.rifleWeapon;
        debug.ability1 = ab.dash;
        debug.ability2 = ab.dash;
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
    public int healthMax { get; set; }
    public int healthCurrent { get; set; }
    public bool damage(int healthAmount)
    {
        healthCurrent -= healthAmount;
        
        if (healthCurrent <= 0)
        {
            die();
            return true;
        }
        else
            return false;
    }
    public bool die()
    {
        //TO-DO
        return false;
    }
    public Character(int maxHealth)
    {
        healthMax = maxHealth;
        healthCurrent = healthMax;
    }
}
