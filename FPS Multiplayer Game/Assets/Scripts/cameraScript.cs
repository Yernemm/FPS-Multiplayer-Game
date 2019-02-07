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
        ui = GameObject.Find("Game Controller").GetComponent<UIScript>();
        //Cursor.lockState = CursorLockMode.Locked;

        initPlayerCamera();
   
        posOffset = new Vector3(0, 1, 0);
       foreach(Transform g in player.transform)
        {
            g.GetComponent<MeshRenderer>().enabled = false;
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
        //Look at the player every frame.
        //transform.LookAt(player.transform);
        float yRot = Input.GetAxis("Mouse Y") * player.GetComponent<playerController>().camSens;
       // if (yRot > 0)
           // Debug.Log(yRot);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, player.transform.eulerAngles.y, player.transform.eulerAngles.z);
        transform.eulerAngles -= new Vector3(yRot,0, 0);


        if (Input.GetMouseButton(0))
        {
            Character ch = player.GetComponent<playerController>().currentCharacter;
            ch.weapon.shoot(transform);
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
