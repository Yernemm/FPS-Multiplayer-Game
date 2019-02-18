using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpColliderScript : MonoBehaviour
{
    public bool canJump = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Floor")
            canJump = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Floor")
            canJump = false;

    }
}
