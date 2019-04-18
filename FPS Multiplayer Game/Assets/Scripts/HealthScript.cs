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


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with " + collision.collider.gameObject.name + "  Tag: " + collision.collider.gameObject.tag);
        if(collision.collider.gameObject.tag == "Bullet" &&
            GetComponent<playerController>().playerId != collision.collider.gameObject.GetComponent<BulletScript>().shotBy)
        {

            
            
        }
    }

    //Procedure forced to run client-side.
    [ClientRpc]
    public void RpcHit(int damageToDeal)
    {
        Debug.Log(GetComponent<playerController>().playerId + ") Collision with bullet with " + damageToDeal);
        pl.currentCharacter.damage(damageToDeal);
     
        
    }



    //Update score if a shot lands
    [ClientRpc]
    public void RpcChangeScore(int score)
    {
        GetComponent<playerController>().currentCharacter.changeScore(score);
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            Debug.Log("Trigger with bullet");
            //pl.currentCharacter.damage(other.GetComponent<BulletScript>().damage);
        }
    }


}
