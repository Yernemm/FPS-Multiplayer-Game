using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField]
    Collider bodyCollider;
    [SerializeField]
    Collider headCollider;
    [SerializeField]
    playerController pl;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with " + collision.collider.gameObject.name + "  Tag: " + collision.collider.gameObject.tag);
        if(collision.collider.gameObject.tag == "Bullet")
        {
            Debug.Log("Collision with bullet");
            int damageToDeal = collision.collider.GetComponent<BulletScript>().damage;
            pl.currentCharacter.damage(damageToDeal);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            Debug.Log("Trigger with bullet");
            pl.currentCharacter.damage(other.GetComponent<BulletScript>().damage);
        }
    }


}
