﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UIScript : NetworkBehaviour {

    public UnityEngine.UI.Text ammoText;
    public UnityEngine.UI.Text timeLeftText;
    public UnityEngine.UI.Text debugTextUI;
    public UnityEngine.UI.Text ability1Text;
    public UnityEngine.UI.Text ability2Text;
    public UnityEngine.UI.Image ability1Image;
    public UnityEngine.UI.Image ability2Image;
    GameController gameController;

    // Use this for initialization
    void Start () {
        //Setting the values of the above variables.
        ammoText = GameObject.Find("AmmoText").GetComponent<UnityEngine.UI.Text>();
        timeLeftText = GameObject.Find("TimeLeft").GetComponent<UnityEngine.UI.Text>();
        debugTextUI = GameObject.Find("DebugText").GetComponent<UnityEngine.UI.Text>();
        ability1Image = GameObject.Find("Ability1Image").GetComponent<UnityEngine.UI.Image>();
        ability2Image = GameObject.Find("Ability2Image").GetComponent<UnityEngine.UI.Image>();
        ability1Text = GameObject.Find("AbilityText1").GetComponent<UnityEngine.UI.Text>();
        ability2Text = GameObject.Find("AbilityText2").GetComponent<UnityEngine.UI.Text>();
        gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
    }

    public void updateTime(float timeLeft)
    {
        timeLeftText.text = "Time Left: " + Mathf.Floor(timeLeft);
       
    }
    public void updateDebug(string debugText)
    {
        debugTextUI.text = "Debug info: \n" + debugText;
    }
    public void updateAmmo(int ammoCurrent, int ammoMax)
    {
        ammoText.text = ammoCurrent + "/" + ammoMax;
    }

    public void setAbility1Sprite(Sprite sp)
    {
        ability1Image.sprite = sp;
    }

    public void setAbility2Sprite(Sprite sp)
    {
        ability2Image.sprite = sp;
    }

    public void updateAbility1Cooldown(float cooldown)
    {
        ability1Text.text = (Mathf.Ceil(cooldown * 10) / 10).ToString();
    }

    public void updateAbility2Cooldown(float cooldown)
    {
        ability2Text.text = (Mathf.Ceil(cooldown * 10) / 10).ToString();
    }

    // Update is called once per frame
    void Update () {
        //Update the time left on UI
        updateTime((float)gameController.timeLeft);
	}
}
