﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameController : MonoBehaviour {

    public double timeLeft;
    public double time;
    //UIScript uiScript;
   
    
    public string debugText = "";
    public void addDebugText(string t)
    {
        debugText += t + "\n";
    }

	// Use this for initialization
	void Start () {
        //uiScript = GetComponent<UIScript>();
        timeLeft = 600;

        NetworkClient myClient;
        myClient = new NetworkClient();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        myClient.Connect("127.0.0.1", 4444);

    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        timeLeft -= Time.deltaTime;
        //uiScript.updateDebug(debugText);
        //uiScript.updateTime((float)timeLeft);

        
    }
    private void LateUpdate()
    {
        debugText = "";
    }

    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server");
    }
}
