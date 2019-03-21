using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //Get animator component.
    [SerializeField]
    Animator anim;

    //Get all animations.
    const string animIdle = "Idle";
    const string animWalk = "Walk";
    const string animWalkBack = "WalkBack";
    const string animWalkRight = "WalkRight";
    const string animWalkLeft = "WalkLeft";
    const string animJump = "Jump";
    const string animInAir = "InAir";


    [SerializeField]
    GameObject cam; //Main camera.
    [SerializeField]
    GameObject jumpBox; //Jump trigger box on the player.
    // Update is called once per frame
    void Update()
    {
        //Only run the code on the local player.
        if (!GetComponent<playerController>().localPlayer)
            return;
        //Flag boolean for detecting if a player is moving.
        bool isMoving = false;

        //1 = moving forward
        //0 = not moving forward or back
        //-1 = moving back
        int forward = 0;

        //1 = moving right
        //0 = not right or left
        //-1 = moving left
        int right = 0;

        //If the movement keys are currently pressed down, add the appropriate value to the detecting variables.
        forward += Input.GetKey("w") ? 1 : 0; //e.g. if W pressed, add 1 to forward, if not, add 0
        forward += Input.GetKey("s") ? -1 : 0;
        right += Input.GetKey("a") ? -1 : 0;
        right += Input.GetKey("d") ? 1 : 0;

        //if any movement or jump key is currently pressed, set the moving flag to true.
        if (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d") || Input.GetKey("space"))
        {
            isMoving = true;
        }



        //These calls play the movement animations if the player is moving.
        //Further explained in the procedure explanation.
        animateFromDirection(forward, right, 1, 0, animWalk); //E.g. if moving forward, play walk animation.
        animateFromDirection(forward, right, -1, 0, animWalkBack);

        //If not currently jumping and not moving, play idle animation.
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName(animJump))
        animateFromDirection(forward, right, 0, 0, animIdle);

        animateFromDirection(forward, right, 1, -1, animWalkLeft);
        animateFromDirection(forward, right, 0, -1, animWalkLeft);
        animateFromDirection(forward, right, -1, -1, animWalkLeft);

        animateFromDirection(forward, right, 1, 1, animWalkRight);
        animateFromDirection(forward, right, 0, 1, animWalkRight);
        animateFromDirection(forward, right, -1, 1, animWalkRight);

        //Play jump animation if space is pressed.
        animate("space", animJump);

        //If player is in the air, play the in-air animation.
        if (!jumpBox.GetComponent<JumpColliderScript>().canJump
            & !anim.GetCurrentAnimatorStateInfo(0).IsName(animJump) 
            & !anim.GetCurrentAnimatorStateInfo(0).IsName(animInAir))
            anim.Play(animInAir, 0, 0);
       
    }

    //Play the "animation" animation if the "keyPress" key is pressed.
    bool animate(string keyPress, string animation)
    {
        //If key is pressed down and not currently playing the given animation.
        if (Input.GetKeyDown(keyPress) & !anim.GetCurrentAnimatorStateInfo(0).IsName(animation))
        {
            anim.Play(animation, 0, 0); //Play the input animation.
            return true;
        }
        else
            return false;
    }

    //Takes the current "forward" and "right" values. 
    //Compares them to the "f" and "r" values required for the animation.
    //If these match, play the "animation" animation.
    bool animateFromDirection(int forward, int right, int f, int r, string animation)
    {
        //If requirements for animation met and not currently animating, not jumping, and on the ground.
        if (forward == f & right == r 
            & !anim.GetCurrentAnimatorStateInfo(0).IsName(animation) 
            & !anim.GetCurrentAnimatorStateInfo(0).IsName(animJump) 
            & jumpBox.GetComponent<JumpColliderScript>().canJump)
        {
            anim.Play(animation, 0, 0); //Play animation.
            return true;
        }
        else
            return false;
    }
}
