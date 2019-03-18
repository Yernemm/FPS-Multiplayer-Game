using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletScript : NetworkBehaviour
{
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
        if (!isServer)
            return;

        //Debug.Log("Bullet collided with " + collision.collider);
        if (collision.collider.gameObject.tag == "Player")
            if(collision.collider.gameObject.GetComponent<playerController>().playerId == shotBy)
            return;

        if (collision.collider.gameObject.tag == "Player")
        {

            if (collision.collider.gameObject.GetComponent<playerController>().playerId != shotBy)
            {
                //Debug.Log("Player " + collision.collider.gameObject.GetComponent<playerController>().playerId + " has been shot by Player " + shotBy);
                //collision.collider.gameObject.GetComponent<playerController>().damage(damage);
                collision.collider.gameObject.GetComponent<HealthScript>().RpcHit(damage);
                GameObject shooter = gc.getPlayerById(shotBy);
                shooter.GetComponent<HealthScript>().RpcChangeScore(damage);
                CmdSpawnPlayerParticles(transform.position);
            }
        }
        else
        {
            CmdSpawnParticles(transform.position);
        }
       
            
            Destroy(gameObject);
        
    }

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
