using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public double timeLeft;
    public UnityEngine.UI.Text timeLeftText;

	// Use this for initialization
	void Start () {
        timeLeftText = GameObject.Find("TimeLeft").GetComponent<UnityEngine.UI.Text>();
        timeLeft = 600;
       
	}
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        timeLeftText.text = "Time Left: " + Mathf.Floor((float)timeLeft);
        

    }
}
