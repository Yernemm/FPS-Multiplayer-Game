using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMoveScript : MonoBehaviour
{
    //This class is on the player's neck.
    //Moving the mouse moves the neck, which also moves the camera.
    [SerializeField]
    GameObject player;
    float newYPos;
    // Start is called before the first frame update
    void Start()
    {
        newYPos = transform.localEulerAngles.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Only run this on the local player.
        if (!player.GetComponent<playerController>().localPlayer)
            return;

        //Moving mouse up and down rotates the neck.
        float yRot = Input.GetAxis("Mouse Y") * player.GetComponent<playerController>().camSens;
        newYPos +=  yRot;

        //Prevent the neck from moving more than 90 degrees up or down.
        if (newYPos > 90f & newYPos < 180f)
            newYPos = 90f;
        else if (newYPos < 270f & newYPos > 180f)
            newYPos = -90f;
        else if (newYPos < -90f)
            newYPos = -90f;

        transform.localEulerAngles = new Vector3(0, newYPos, 0);
    }
}
