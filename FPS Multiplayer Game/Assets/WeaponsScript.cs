using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsScript : MonoBehaviour {
    double lastFired = 0;
    double time;
    public GameObject debugBullet;



    public bool shootDebugGun(Transform tr)
    {
        //Draw line here for debug
       // Debug.Log("It has been shot");
        //Vector3 lookAt = new Vector3()
        //Debug.DrawLine(tr.position, new Vector3(0, 0, 0), Color.red, 1000, true);

        // Debug.Log("TIME: " + GameObject.Find("Game Controller").GetComponent<GameController>().time);
        double time = GameObject.Find("Game Controller").GetComponent<GameController>().time;
        if (lastFired <= time - debugGun.fireInterval()) {
            lastFired = time;
            //GameObject bullet = Instantiate(debugBullet, GameObject.Find("Main Camera").GetComponent<Transform>().transform);

            Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            Vector3 spawnPos = new Vector3(Screen.width / 2, Screen.height /2 , 0);
            Transform spawnLocation = camera.transform;
            spawnLocation.position = camera.transform.position ;
            GameObject bullet = Instantiate(debugBullet);
            bullet.transform.position = spawnLocation.position;


            Debug.Log(Screen.width + " " + Screen.height + " " + camera.transform.position + " " + spawnLocation.position + " " + bullet.transform.position);
        bullet.transform.parent = null;
        bullet.GetComponent<Rigidbody>().velocity = GameObject.Find("Main Camera").GetComponent<Transform>().transform.forward * 50f;
        //Debug.Log(Camera.main.transform.forward.normalized);
        bullet.transform.parent = null;
    }

        return true;
    }

    public Weapon debugGun = new Weapon()
    {
        name = "Debug Gun",
        ammoMax = 20,
        fireRate = 5
    };

    void Start()
    {
        debugGun.shoot = shootDebugGun;
        time = GameObject.Find("Game Controller").GetComponent<GameController>().time;

    }


}



public class Weapon
{
    public string name { get; set; }
    public int ammoMax { get; set; }
    public int ammoCurrent { get; set; }
    public delegate bool shootDelegate(Transform tr);
    public shootDelegate shoot { get; set; }
    public delegate bool reloadDelegate();
    public reloadDelegate reload;
    public double fireRate;
    public Weapon()
    {
        ammoCurrent = ammoMax;
    }
    public double fireInterval()
    {
        return 1 / fireRate;
    }
}
