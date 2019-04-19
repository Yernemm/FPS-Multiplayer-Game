using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponsScript : NetworkBehaviour {

    //This class provides the behaviour for each weapon object.

    double lastFired = 0;
    double time;
    public GameObject mainBullet;
    public UIScript ui;
    [SerializeField]
    GameObject camera;
    [SerializeField]
    GameObject reloadAnimationObject;


    // ================ NORMAL RIFLE ================
    public bool shootRifle(Transform tr, GameObject player)
    {
        //Prevent the player from shooting again unless a certain amount of time passed to create a constant fire rate.
        double time = GameObject.Find("Game Controller").GetComponent<GameController>().time;
        if (lastFired <= time - rifleWeapon.fireInterval())
        {
            lastFired = time;
            Debug.Log("before shoot " + player.GetComponent<playerController>().playerId);
            CmdServerSpawnRifleBullet(
                player.GetComponent<playerController>().gunSpawnPosition.position, 
                player.GetComponent<playerController>().gunSpawnPosition.rotation, 
                player.GetComponent<playerController>().playerId,
                rifleWeapon.damage
                ); //Spawn bullet with set velocity and postitin when shooting.
            //Play the recoil camera animation if not currently playing the dash animation 
            //to prevent the animations from clashing.
            if(!camera.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CameraDashAbility"))
                camera.GetComponent<Animator>().Play("CameraRecoil", 0, 0);

            return true;
        }
        else
            return false;
        
    }
    //Spawn bullet server-side so it is synchronised to clients.
    [Command]
    void CmdServerSpawnRifleBullet(Vector3 pos, Quaternion rot, uint shooter, int damage)
    {
        Debug.Log("during shoot " + shooter);
        GameObject bullet = Instantiate(mainBullet, pos,rot);
        bullet.GetComponent<BulletScript>().shotBy = shooter; //set bullet properties.
        bullet.GetComponent<BulletScript>().damage = damage;
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 75f; //set bvelocity.
        NetworkServer.Spawn(bullet);     
        
    }
    //reload procedure
    public bool reloadRifle()
    {
        //Do not reload if currently reloading or if player has full ammo.
        if (rifleWeapon.isReloading || rifleWeapon.ammoCurrent == rifleWeapon.ammoMax)
            return false;
        else
        {
            //Play the reload animation and then set the ammo to max.
            Animator anim = reloadAnimationObject.GetComponent<Animator>();
            anim.SetFloat("Speed", (float)(1 / rifleWeapon.reloadTime));
            anim.Play("Reload", 0, 0);
            rifleWeapon.isReloading = true;
            rifleWeapon.ammoCurrent = rifleWeapon.ammoMax;
            return true;
        }
    }
    //Instantiate the main rifle weapon.
    public Weapon rifleWeapon = new Weapon(30)
    {
        name = "Rifle",
        fireRate = 7,
        reloadTime = 1,
        damage = 20
    };

    //===========================================================

    //Instantiate a debug weapon. This one is used for testing.
    public Weapon debugHitscan = new Weapon(60)
    {
        name = "Hitscan Gun",
        fireRate = 10
    };
    private readonly object debugGun;

    void Start()
    {
        rifleWeapon.shootCode = shootRifle; //Set the delegates for the rifle weapon.
        rifleWeapon.reload = reloadRifle;
        time = GameObject.Find("Game Controller").GetComponent<GameController>().time;
        ui = GetComponent<UIScript>();
    }




}



public class Weapon
{
    //This is a tempalte class for creating different weapons.
    //Each character object has one weapon object.
    public string name { get; set; }
    public int ammoMax { get; set; }
    public int ammoCurrent { get; set; }
    public delegate bool shootDelegate(Transform tr, GameObject player);
    public shootDelegate shootCode { get; set; }
    public void shoot(Transform tr, GameObject player)
    {
        if (ammoCurrent > 0 && !isReloading)
        {
            if (shootCode(tr, player))
            {
                //Shooting always subtracts 1 from ammo count.
                ammoCurrent--;
            };
        }
        else if (ammoCurrent <= 0 && !isReloading)
            reload(); //If a player tries to shoot with no ammo, automatically reload.
    }
    public delegate bool reloadDelegate();
    public reloadDelegate reload;
    public double reloadTime;
    public double fireRate;
    public Weapon(int ammoMaxInput)
    {
        //Set the basic properties when instantiating a weapon.
        ammoMax = ammoMaxInput;
        ammoCurrent = ammoMax;
        isReloading = false;
    }
    public double fireInterval()
    {
        //The interval between each shot is the inverse of the rate at which it shoots.
        return 1 / fireRate;
    }
    public int damage { get; set; }
    public bool isReloading { get; set; }

}
