using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UIScript : NetworkBehaviour {

    //This class provides the functions which allow other classes to easily update the HUD UI.

    //Declare variables for all of the dynamic UI elements.
    public UnityEngine.UI.Text ammoText;
    public UnityEngine.UI.Text timeLeftText;
    public UnityEngine.UI.Text debugTextUI;
    public UnityEngine.UI.Text ability1Text;
    public UnityEngine.UI.Text ability2Text;
    public UnityEngine.UI.Image ability1Image;
    public UnityEngine.UI.Image ability2Image;
    public UnityEngine.UI.Text healthText;
    public UnityEngine.UI.Text scoreText;
    public UnityEngine.UI.Text topPlayerText;
    GameController gameController;

    // Use this for initialization
    void Start () {
        //Assign all of the UI elements to their object.
        ammoText = GameObject.Find("AmmoText").GetComponent<UnityEngine.UI.Text>();
        timeLeftText = GameObject.Find("TimeLeft").GetComponent<UnityEngine.UI.Text>();
        debugTextUI = GameObject.Find("DebugText").GetComponent<UnityEngine.UI.Text>();
        ability1Image = GameObject.Find("Ability1Image").GetComponent<UnityEngine.UI.Image>();
        ability2Image = GameObject.Find("Ability2Image").GetComponent<UnityEngine.UI.Image>();
        ability1Text = GameObject.Find("AbilityText1").GetComponent<UnityEngine.UI.Text>();
        ability2Text = GameObject.Find("AbilityText2").GetComponent<UnityEngine.UI.Text>();
        gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
        healthText = GameObject.Find("HealthText").GetComponent<UnityEngine.UI.Text>();
        scoreText = GameObject.Find("ScoreText").GetComponent<UnityEngine.UI.Text>();
        topPlayerText = GameObject.Find("TopPlayerText").GetComponent<UnityEngine.UI.Text>();
    }

    //Functions for updating the UI elements.
    //They are called from other classes.
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

    public void updateHealth(int healthCurrent, int healthMax)
    {
        healthText.text = healthCurrent + "/" + healthMax;
    }

    public void setAbility1Sprite(Sprite sp)
    {
        ability1Image.sprite = sp;
    }

    public void setAbility2Sprite(Sprite sp)
    {
        ability2Image.sprite = sp;
    }
    //Ability 1 cooldown time.
    public void updateAbility1Cooldown(float cooldown)
    {
        ability1Text.text = (Mathf.Ceil(cooldown * 10) / 10).ToString();
    }
    //Ability 2 cooldown time.
    public void updateAbility2Cooldown(float cooldown)
    {
        ability2Text.text = (Mathf.Ceil(cooldown * 10) / 10).ToString();
    }

    public void updateScore(int score)
    {
        scoreText.text = "Score: " + score + "/" + gameController.getMaxScore();
    }

    //Update the top player display.
    public void updateTopPlayer()
    {
        var topPlayer = gameController.getTopPlayer();
        topPlayerText.text = "Top Player:\n" 
            + topPlayer.player.GetComponent<CharactersScript>().username + "\n"
            + topPlayer.score;
    }

    // Update is called once per frame
    void Update () {
        //Update the time left on UI
        updateTime((float)gameController.timeLeft);
        updateTopPlayer();

    }
}
