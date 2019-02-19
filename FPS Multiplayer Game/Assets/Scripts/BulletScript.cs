using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public uint shotBy;

    private void Start()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player"))
        {
            Physics.IgnoreCollision(g.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
           
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {       
            
            if (other.gameObject.GetComponent<playerController>().playerId != shotBy)
            {
                Debug.Log("Player " + other.gameObject.GetComponent<playerController>().playerId + " has been shot by Player " + shotBy);
            }
        }
    }
}
