using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {

    //This class handles camera movement.
    
    //Player rigid body will be set in Unity.
    [SerializeField]
    GameObject player;
    UIScript ui;    

	// Use this for initialization
	void Start () {
        ui = player.GetComponent<UIScript>();
        //Lock the cursor.
        //This makes the cursor invisible and locks it to the centre of the screen.
       Cursor.lockState = CursorLockMode.Locked;  
    }

	// Update is called once per frame
	void Update () {
        //If left mouse button clicked, shoot with the waeapon and update the ammo counter.
        if (Input.GetMouseButton(0))
        {
            Character ch = player.GetComponent<playerController>().currentCharacter;
            ch.weapon.shoot(transform, player);
            ui.updateAmmo(ch.weapon.ammoCurrent, ch.weapon.ammoMax);
            
        }
    }
}
