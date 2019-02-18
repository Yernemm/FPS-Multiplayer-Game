﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMoveScript : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.GetComponent<playerController>().localPlayer)
            return;

        float yRot = Input.GetAxis("Mouse Y") * player.GetComponent<playerController>().camSens;
        float newYPos = transform.localEulerAngles.y + yRot;

        if (newYPos > 90f & newYPos < 180f)
            newYPos = 90f;
        else if (newYPos < 270f & newYPos > 180f)
            newYPos = -90f;

        transform.localEulerAngles = new Vector3(0, newYPos, 0);
    }
}
