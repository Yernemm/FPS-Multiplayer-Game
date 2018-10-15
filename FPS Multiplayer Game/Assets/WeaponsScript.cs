using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsScript : MonoBehaviour {
    public GameObject debugBullet;

    public bool shootGun(Transform tr)
    {
        //Draw line here for debug
        Debug.Log("It has been shot");
        //Vector3 lookAt = new Vector3()
        Debug.DrawLine(tr.position, new Vector3(0, 0, 0), Color.red, 1000,true);
        GameObject bullet = Instantiate(debugBullet, GameObject.Find("Main Camera").GetComponent<Transform>().transform);
        bullet.transform.parent = null;
        bullet.GetComponent<Rigidbody>().velocity = GameObject.Find("Main Camera").GetComponent<Transform>().transform.forward * 150f;
        //Debug.Log(Camera.main.transform.forward.normalized);
        bullet.transform.parent = null;
        

        return true;
    }

    public Weapon debugGun = new Weapon()
    {
        name = "Debug Gun",
        ammoMax = 20
    };

    void Start()
    {
        debugGun.shoot = shootGun;
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
    public Weapon()
    {
        ammoCurrent = ammoMax;
    }
}
