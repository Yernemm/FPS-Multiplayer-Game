using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameController : NetworkBehaviour {


    //SyncVar synchronises the state of the variable between the server and the clients.
    [SyncVar]
    public double timeLeft;
    [SyncVar]
    public double time;
    
   
    
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

    //Searches all game object to find a matching player.
    public GameObject getPlayerById(uint id)
    {
        GameObject[] players = FindObjectsOfType<GameObject>();
        foreach(GameObject player in players)
        {
            if (player.tag == "Player")
            {
                //Game object is a player
                if(player.GetComponent<playerController>().playerId == id)
                {
                    //ID matched
                    return player;
                }
            }
        }
        return null;
    }
}
