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

    private void LateUpdate()
    {
        Debug.Log(head.transform.localRotation);
        Debug.Log(cam.transform.localRotation);
        // Transform headPos = cam.transform;
        //head.transform.localRotation = new Quaternion(cam.transform.localRotation.x, head.transform.localRotation.y, head.transform.localRotation.z, head.transform.localRotation.w);
        //head.transform.eulerAngles.Set(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);
       // head.transform.rotation = headPos.rotation;
    }
}
