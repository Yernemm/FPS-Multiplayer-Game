using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    const string animIdle = "Idle";
    const string animWalk = "Walk";
    const string animWalkBack = "WalkBack";
    const string animWalkRight = "WalkRight";
    const string animWalkLeft = "WalkLeft";
    const string animJump = "Jump";
    const string animInAir = "InAir";

    [SerializeField]
    GameObject cam;
    [SerializeField]
    GameObject jumpBox;
    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<playerController>().localPlayer)
            return;
        bool isMoving = false;
        int forward = 0;
        int right = 0;

        forward += Input.GetKey("w") ? 1 : 0;
        forward += Input.GetKey("s") ? -1 : 0;
        right += Input.GetKey("a") ? -1 : 0;
        right += Input.GetKey("d") ? 1 : 0;

        if (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d") || Input.GetKey("space"))
        {
            isMoving = true;
        }




        animateFromDirection(forward, right, 1, 0, animWalk);
        animateFromDirection(forward, right, -1, 0, animWalkBack);

        if(!anim.GetCurrentAnimatorStateInfo(0).IsName(animJump))
        animateFromDirection(forward, right, 0, 0, animIdle);

        animateFromDirection(forward, right, 1, -1, animWalkLeft);
        animateFromDirection(forward, right, 0, -1, animWalkLeft);
        animateFromDirection(forward, right, -1, -1, animWalkLeft);

        animateFromDirection(forward, right, 1, 1, animWalkRight);
        animateFromDirection(forward, right, 0, 1, animWalkRight);
        animateFromDirection(forward, right, -1, 1, animWalkRight);

        animate("space", animJump);

        if (!jumpBox.GetComponent<JumpColliderScript>().canJump & !anim.GetCurrentAnimatorStateInfo(0).IsName(animJump) & !anim.GetCurrentAnimatorStateInfo(0).IsName(animInAir))
            anim.Play(animInAir, 0, 0);
       
    }

    bool animate(string keyPress, string animation)
    {

        if (Input.GetKeyDown(keyPress) & !anim.GetCurrentAnimatorStateInfo(0).IsName(animation))
        {
            anim.Play(animation, 0, 0);
            return true;
        }
        else
            return false;
    }

    bool animateFromDirection(int forward, int right, int f, int r, string animation)
    {

        if (forward == f & right == r & !anim.GetCurrentAnimatorStateInfo(0).IsName(animation) & !anim.GetCurrentAnimatorStateInfo(0).IsName(animJump) & jumpBox.GetComponent<JumpColliderScript>().canJump)
        {
            anim.Play(animation, 0, 0);
            return true;
        }
        else
            return false;
    }
}
