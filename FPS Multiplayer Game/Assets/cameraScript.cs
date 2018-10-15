using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {

    
    //Player rigid body will be set in Unity.
   // public Rigidbody player;
    Vector3 posOffset;
    GameObject player;
    

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        posOffset = new Vector3(0, 1, 0);
      //  GameObject.Find("Player").GetComponent<MeshRenderer>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        //Look at the player every frame.
        //transform.LookAt(player.transform);
        float yRot = Input.GetAxis("Mouse Y") * GameObject.Find("Player").GetComponent<playerController>().camSens;
        if (yRot > 0)
            Debug.Log(yRot);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, player.transform.eulerAngles.y, player.transform.eulerAngles.z);
        transform.eulerAngles -= new Vector3(yRot,0, 0);


        if (Input.GetMouseButton(0))
            player.GetComponent<playerController>().currentCharacter.weapon.shoot(transform);

    }

    private void LateUpdate()
    {
        transform.position = player.GetComponent<Rigidbody>().position + posOffset;
        //transform.eulerAngles = player.transform.eulerAngles;
    }
}
