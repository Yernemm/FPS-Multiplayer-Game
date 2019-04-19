using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class nameTagScript : MonoBehaviour
{
    //This class displays the player name above a player's head.
    [SerializeField]
    TextMeshPro tagtext;
    [SerializeField]
    GameObject player;

    // Update is called once per frame
    void Update()
    {
        //Hide the name tag if local player.
        if (!player.GetComponent<playerController>().localPlayer)
            tagtext.text = player.GetComponent<CharactersScript>().username;
        else
            tagtext.text = "";

        //Rotate the text to always face the camera.
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }
}
