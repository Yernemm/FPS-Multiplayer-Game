using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionIgnore : MonoBehaviour {
    public string Tag;

    private void Start()
    {
        foreach(GameObject g in GameObject.FindGameObjectsWithTag(Tag))
        {
            Physics.IgnoreCollision(g.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.collider.name);
        if (collision.gameObject.tag == Tag)
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }
}
