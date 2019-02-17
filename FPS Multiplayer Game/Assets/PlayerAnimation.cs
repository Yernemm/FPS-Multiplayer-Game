using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    [SerializeField]
    string animIdle = "Idle";
    [SerializeField]
    string animWalk = "Walk";
    [SerializeField]
    GameObject cam;
    [SerializeField]
    GameObject head;
    // Update is called once per frame
    void Update()
    {
        bool isMoving = false;


        if (Input.GetKeyDown("w"))
        {
            anim.Play(animWalk, 0, 0);
        }

        if (Input.GetKey("w"))
        {
            isMoving = true;
        }

        if (!isMoving & anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != animIdle)
        {
            anim.Play(animIdle, 0, 0);
        }
    }
}
