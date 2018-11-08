using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour {

    public UnityEngine.UI.Text ammoText;
    public UnityEngine.UI.Text timeLeftText;
    public UnityEngine.UI.Text debugTextUI;

    // Use this for initialization
    void Start () {
        ammoText = GameObject.Find("AmmoText").GetComponent<UnityEngine.UI.Text>();
        timeLeftText = GameObject.Find("TimeLeft").GetComponent<UnityEngine.UI.Text>();
        debugTextUI = GameObject.Find("DebugText").GetComponent<UnityEngine.UI.Text>();
    }

    public void updateTime(float timeLeft)
    {
        timeLeftText.text = "Time Left: " + Mathf.Floor((float)timeLeft);
       
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
		
	}
}
