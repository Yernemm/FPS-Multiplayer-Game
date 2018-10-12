﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsScript : MonoBehaviour {

    public bool shootGun(Rigidbody rb)
    {
        Debug.Log("It has been shot");
        return true;
    }

    public Weapon mainGun = new Weapon()
    {
        name = "Gun",
        ammoMax = 20
    };

    void Start()
    {
        mainGun.shoot = shootGun;
    }


}



public class Weapon
{
    public string name { get; set; }
    public int ammoMax { get; set; }
    public int ammoCurrent { get; set; }
    public delegate bool shootDelegate(Rigidbody rb);
    public shootDelegate shoot { get; set; }
    public delegate bool reloadDelegate();
    public reloadDelegate reload;
    public Weapon()
    {
        ammoCurrent = ammoMax;
    }
}