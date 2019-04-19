using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HealthScript : NetworkBehaviour
{
    //This class keeps track of player health.
    
    [SerializeField]
    Collider bodyCollider;
    [SerializeField]
    Collider headCollider;
    [SerializeField]
    playerController pl;

    //Collision detection for development purposes.
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with " + collision.collider.gameObject.name + "  Tag: " + collision.collider.gameObject.tag);

    }

    //Procedure forced to run client-side.
    //Collisions are detected server-side but the health must be updated locally.
    [ClientRpc]
    public void RpcHit(int damageToDeal)
    {
        //Deal damage when a bullet hits a player.
        Debug.Log(GetComponent<playerController>().playerId + ") Collision with bullet with " + damageToDeal);
        pl.currentCharacter.damage(damageToDeal);
     
        
    }



    //Update score if a shot lands
    [ClientRpc]
    public void RpcChangeScore(int score)
    {
        GetComponent<playerController>().currentCharacter.changeScore(score);
    }


    //Collision detection for development purposes.
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            Debug.Log("Trigger with bullet");
        }
    }


}
