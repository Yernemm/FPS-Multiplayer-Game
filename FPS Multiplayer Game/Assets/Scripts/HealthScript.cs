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
        Debug.Log("Collision detected with " + collision.collider.gameObject.name);
        if(collision.collider.gameObject.tag == "Bullet")
        {
            Debug.Log("Collision with bullet");
            pl.currentCharacter.damage(collision.collider.GetComponent<BulletScript>().damage);
        }
    }


}
