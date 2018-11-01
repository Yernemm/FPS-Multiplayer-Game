using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public double timeLeft;
    public double time;
    public UnityEngine.UI.Text timeLeftText;
    public UnityEngine.UI.Text debugTextUI;
    public string debugText = "";
    public void addDebugText(string t)
    {
        debugText += t + "\n";
    }

	// Use this for initialization
	void Start () {
        timeLeftText = GameObject.Find("TimeLeft").GetComponent<UnityEngine.UI.Text>();
        debugTextUI = GameObject.Find("DebugText").GetComponent<UnityEngine.UI.Text>();

        timeLeft = 600;
       
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        timeLeft -= Time.deltaTime;
        timeLeftText.text = "Time Left: " + Mathf.Floor((float)timeLeft);
        debugTextUI.text = "Debug info: \n" + debugText;

        
    }
    private void LateUpdate()
    {
        debugText = "";
    }
}
