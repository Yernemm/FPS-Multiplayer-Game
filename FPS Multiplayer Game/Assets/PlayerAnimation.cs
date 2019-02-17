using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    [SerializeField]
    string walkAnim = "Walk";
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            anim.Play(walkAnim, 0, 0);
        }
    }
}
