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
    [SyncVar]
    public int damage;

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
            }
        }
       
            CmdSpawnParticles(transform.position);
            Destroy(gameObject);
        
    }

    [Command]
    void CmdSpawnParticles(Vector3 postion)
    {
        GameObject p = Instantiate(particles, postion, Quaternion.identity);
        NetworkServer.Spawn(p);
    }

}
