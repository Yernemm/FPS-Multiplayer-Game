using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletScript : NetworkBehaviour
{
    //This class handles collision detection for bullets.

    [SyncVar]
    public uint shotBy;
    [SerializeField]
    GameObject particles;
    [SerializeField]
    GameObject playerParticles;
    [SyncVar]
    public int damage;

    private GameController gc;

    private void Start()
    {
        gc = GameObject.Find("Game Controller").GetComponent<GameController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Only do collision detection on the server.
        if (!isServer)
            return;
        //Ignore collisions with the player that shot the bullet.
        if (collision.collider.gameObject.tag == "Player")
            if(collision.collider.gameObject.GetComponent<playerController>().playerId == shotBy)
            return;

        if (collision.collider.gameObject.tag == "Player")
        {

            if (collision.collider.gameObject.GetComponent<playerController>().playerId != shotBy)
            {
                //If colliding with a player.
                collision.collider.gameObject.GetComponent<HealthScript>().RpcHit(damage); //Deal damage
                GameObject shooter = gc.getPlayerById(shotBy);
                shooter.GetComponent<HealthScript>().RpcChangeScore(damage); //Give shooter points
                CmdSpawnPlayerParticles(transform.position); //Spawn player impact particles
                if(collision.collider.gameObject.GetComponent<playerController>().currentCharacter.healthCurrent - damage <= 0)
                {
                    //If the bullet will kill the player, give the shooter 1000 points.
                    shooter.GetComponent<HealthScript>().RpcChangeScore(1000);
                }
            }
        }
        else
        {
            //If colliding with something that is not a player, create normal impact particles
            CmdSpawnParticles(transform.position);
        }
       
            //Destroy bullet on collision     
            Destroy(gameObject);
        
    }

    //Commands to spawn particles server-side
    [Command]
    void CmdSpawnParticles(Vector3 postion)
    {
        GameObject p = Instantiate(particles, postion, Quaternion.identity);
        NetworkServer.Spawn(p);
    }

    [Command]
    void CmdSpawnPlayerParticles(Vector3 postion)
    {
        GameObject p = Instantiate(playerParticles, postion, Quaternion.identity);
        NetworkServer.Spawn(p);
    }

}
