using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CharactersScript : NetworkBehaviour
{
    //This class provides the behaviour for each character object.

    WeaponsScript wp;
    AbilitiesScript ab;
    NetworkStartPosition[] spawns;
    [SerializeField]
    GameObject deathParticles;
    [SerializeField]
    GameObject spawnParticles;
    [SyncVar]
    public string username;

    GameController gameController;
    [SerializeField]
    playerController playerController;

    public Character debug = new Character(200)
    {
        name = "Debug Character",
        moveSpeed = 15,
        jumpForce = 6,
        mass = 60,
    };

    private void Start()
    {
        gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
        spawns = FindObjectsOfType<NetworkStartPosition>();
        wp = GetComponent<WeaponsScript>();
        ab = GetComponent<AbilitiesScript>();
        debug.weapon = wp.rifleWeapon;
        debug.ability1 = ab.dash;
        debug.ability2 = ab.dash;

        setGeneralDelegates(debug);

        //Handle setting name.
        if (isLocalPlayer)
            CmdSetName(GameObject.Find("Network Manager").GetComponent<playerName>().name);
        Debug.Log("My name is " + username);

        updatePublicScore(0);
    }

    void setGeneralDelegates(Character character)
    {
        character.respawnPosition = respawnPosition;
        character.deathParticles = createDeathParticles;
        character.updatePublicScore = updatePublicScore;
    }

    private void respawnPosition()
    {
        transform.position = spawns[Random.Range(0, spawns.Length)].transform.position;
        createSpawnParticles();
    }

    private void createDeathParticles()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
    }

    private void createSpawnParticles()
    {
        CmdcreateSpawnParticles(transform.position);
    }

    [Command]
    void CmdcreateSpawnParticles(Vector3 postion)
    {
        GameObject p = Instantiate(spawnParticles, postion, Quaternion.identity);
        NetworkServer.Spawn(p);
    }

    //Set the name with server command to update it for all clients.
    [Command]
    void CmdSetName(string name)
    {
        username = name;
    }

    void updatePublicScore(int score)
    {
        gameController.CmdUpdatePublicScore(playerController.playerId, score);
    }


}

public class Character
{
    //This class acts as a template for creating different characters that a player can play as.
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
    public delegate void updatePublicScoreDelegate(int score);
    public updatePublicScoreDelegate updatePublicScore;
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
        updatePublicScore(score);
    }
    public Character(int maxHealth)
    {
        healthMax = maxHealth;
        healthCurrent = healthMax;
        score = 0;
    }
}
