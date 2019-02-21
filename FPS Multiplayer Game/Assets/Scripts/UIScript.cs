using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UIScript : NetworkBehaviour {

    public UnityEngine.UI.Text ammoText;
    public UnityEngine.UI.Text timeLeftText;
    public UnityEngine.UI.Text debugTextUI;
    GameController gameController;

    // Use this for initialization
    void Start () {
        //Setting the values of the above variables.
        ammoText = GameObject.Find("AmmoText").GetComponent<UnityEngine.UI.Text>();
        timeLeftText = GameObject.Find("TimeLeft").GetComponent<UnityEngine.UI.Text>();
        debugTextUI = GameObject.Find("DebugText").GetComponent<UnityEngine.UI.Text>();
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
	
	// Update is called once per frame
	void Update () {
        //Update the time left on UI
        updateTime((float)gameController.timeLeft);
	}
}
