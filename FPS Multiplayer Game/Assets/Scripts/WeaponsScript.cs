﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponsScript : NetworkBehaviour {
    double lastFired = 0;
    double time;
    public GameObject mainBullet;
    public UIScript ui;
    [SerializeField]
    GameObject camera;


    // ================ NORMAL RIFLE ================
    public bool shootRifle(Transform tr, GameObject player)
    {
        double time = GameObject.Find("Game Controller").GetComponent<GameController>().time;
        if (lastFired <= time - rifleWeapon.fireInterval())
        {
            lastFired = time;
            Debug.Log("before shoot " + player.GetComponent<playerController>().playerId);
            CmdServerSpawnRifleBullet(
                player.GetComponent<playerController>().gunSpawnPosition.position, 
                player.GetComponent<playerController>().gunSpawnPosition.rotation, 
                player.GetComponent<playerController>().playerId
                );
            camera.GetComponent<Animator>().Play("CameraRecoil", 0, 0);
            return true;
        }
        else
            return false;
        
    }

    [Command]
    void CmdServerSpawnRifleBullet(Vector3 pos, Quaternion rot, uint shooter)
    {
        Debug.Log("during shoot " + shooter);
        GameObject bullet = Instantiate(mainBullet, pos,rot);
        bullet.GetComponent<BulletScript>().shotBy = shooter;
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 75f;
        NetworkServer.Spawn(bullet);     
        
    }

    public bool reloadRifle()
    {
        rifleWeapon.ammoCurrent = rifleWeapon.ammoMax;
        return true;
        
    }
    public Weapon rifleWeapon = new Weapon(30)
    {
        name = "Rifle",
        fireRate = 7,
        reloadTime = 2
    };

    //===========================================================

    public Weapon debugHitscan = new Weapon(60)
    {
        name = "Hitscan Gun",
        fireRate = 10
    };
    private readonly object debugGun;

    void Start()
    {
        rifleWeapon.shootCode = shootRifle;
        rifleWeapon.reload = reloadRifle;
        time = GameObject.Find("Game Controller").GetComponent<GameController>().time;
        ui = GetComponent<UIScript>();
    }


}



public class Weapon
{
   
    public string name { get; set; }
    public int ammoMax { get; set; }
    public int ammoCurrent { get; set; }
    public delegate bool shootDelegate(Transform tr, GameObject player);
    public shootDelegate shootCode { get; set; }
    public void shoot(Transform tr, GameObject player)
    {
        if (ammoCurrent > 0)
        {
            if (shootCode(tr, player))
            {
                ammoCurrent--;
            };
        }
    }
    public delegate bool reloadDelegate();
    public reloadDelegate reload;
    public double reloadTime;
    public double fireRate;
    public Weapon(int ammoMaxInput)
    {
        ammoMax = ammoMaxInput;
        ammoCurrent = ammoMax;
    }
    public double fireInterval()
    {
        return 1 / fireRate;
    }
   
}
