using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {

    
    //Player rigid body will be set in Unity.
   // public Rigidbody player;
    Vector3 posOffset;
    [SerializeField]
    GameObject player;
    UIScript ui;    

	// Use this for initialization
	void Start () {
        ui = player.GetComponent<UIScript>();
       // Cursor.lockState = CursorLockMode.Locked;

        initPlayerCamera();
   
        posOffset = new Vector3(0, 1, 0);

        //Make local player invisible because camera is inside local player.
        //Prevents model obstructing view.
        if (!player.GetComponent<playerController>().localPlayer)
        {
            foreach (Transform g in player.transform)
            {

               // g.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
    public void initPlayerCamera()
    {
       // GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
       // foreach (GameObject p in players)
       // {
       //     if (p.GetComponent<playerController>().localPlayer)
        //    {
       //         player = p;
       //    }
       // } 
    }
	
	// Update is called once per frame
	void Update () {



        if (Input.GetMouseButton(0))
        {
            Character ch = player.GetComponent<playerController>().currentCharacter;
            ch.weapon.shoot(transform, player);
            ui.updateAmmo(ch.weapon.ammoCurrent, ch.weapon.ammoMax);
            
        }
    }

    private void LateUpdate()
    {
        //this thing should work once fixed
        //transform.position = player.GetComponent<Rigidbody>().position + posOffset;


        //transform.eulerAngles = player.transform.eulerAngles;
    }
}
