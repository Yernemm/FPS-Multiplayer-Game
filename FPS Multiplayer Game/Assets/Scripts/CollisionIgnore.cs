using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionIgnore : MonoBehaviour {

    //Any object which has this script will ignore collisions with any other object with a specified tag.

    public string Tag; //The tag to ignore.

    private void Start()
    {
        foreach(GameObject g in GameObject.FindGameObjectsWithTag(Tag))
        {
            //Ignore collisions with all exisitng objects with the input tag.
            Physics.IgnoreCollision(g.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If about to collide with a tagged object, don't.
        if (collision.gameObject.tag == Tag)
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }
}
