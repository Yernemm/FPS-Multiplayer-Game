using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpColliderScript : MonoBehaviour
{
    //This class is for collision detection between the character's feet and the floor for jumping.
    public bool canJump = false;

    //When touched a floor, the player can jump.
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Floor")
            canJump = true;
    }
    //When no longer touching floor, the player is in air so cannot jump again.
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Floor")
            canJump = false;

    }
}
