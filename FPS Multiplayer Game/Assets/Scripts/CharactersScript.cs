using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CharactersScript : NetworkBehaviour
{

    WeaponsScript wp;
    AbilitiesScript ab;
    NetworkStartPosition[] spawns;
    [SerializeField]
    GameObject deathParticles;

    public Character debug = new Character(200)
    {
        name = "Debug Character",
        moveSpeed = 15,
        jumpForce = 6,
        mass = 60,
    };

    private void Start()
    {
        spawns = FindObjectsOfType<NetworkStartPosition>();
        wp = GetComponent<WeaponsScript>();
        ab = GetComponent<AbilitiesScript>();
        debug.weapon = wp.rifleWeapon;
        debug.ability1 = ab.dash;
        debug.ability2 = ab.dash;
        debug.respawnPosition = respawnPosition;
        debug.deathParticles = createDeathParticles;
    }


    private void respawnPosition()
    {
        transform.position = spawns[Random.Range(0, spawns.Length)].transform.position;
    }

    private void createDeathParticles()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
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
    public int score { get; set; }
    public delegate void respawnPostionDelegate();
    public respawnPostionDelegate respawnPosition;
    public delegate void deathParticlesDelegate();
    public deathParticlesDelegate deathParticles;
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
        changeScore(-200);
        deathParticles();
        respawn();
        return true;
    }
    public void respawn()
    {
        respawnPosition();
        healthCurrent = healthMax;
    }
    public void kill()
    {
        damage(healthCurrent);
    }
    public void changeScore(int amount)
    {
        score += amount;
    }
    public Character(int maxHealth)
    {
        healthMax = maxHealth;
        healthCurrent = healthMax;
        score = 0;
    }
}
