using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public double timeLeft;
    public double time;
    UIScript uiScript;
   
    
    public string debugText = "";
    public void addDebugText(string t)
    {
        debugText += t + "\n";
    }

	// Use this for initialization
	void Start () {
        uiScript = GetComponent<UIScript>();
        timeLeft = 600;
       
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        timeLeft -= Time.deltaTime;
        uiScript.updateDebug(debugText);
        uiScript.updateTime((float)timeLeft);

        
    }
    private void LateUpdate()
    {
        debugText = "";
    }
}
