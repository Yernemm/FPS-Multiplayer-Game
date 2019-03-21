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
    //Dictionary pairs player IDs with player's score.
    //Synced over network.
    
    public Dictionary<uint, int> playerScores;

    public const int maxScore = 100;
    public int getMaxScore()
    {
        return maxScore;
    }
   
    
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
        //Initialise dictionary.
        playerScores = new Dictionary<uint, int>();

    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        timeLeft -= Time.deltaTime;
        if (isServer)
        {
            if (timeLeft <= 0)
                CmdEndGame();
        }
        
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
    //Struct allows the function to return multiple values.
    public struct getTopPlayerReturn
    {
        public GameObject player;
        public uint id;
        public int score;
    }
    //Get the player with the highest score and return their ID, object and score.
    public getTopPlayerReturn getTopPlayer()
    {
        getTopPlayerReturn output = new getTopPlayerReturn();
        uint id = 0;
        //Lowest signed int value in case all players have a negative score.
        int highest = -2147483648; 
        //Iterate through all players in dictionary.
        foreach(KeyValuePair<uint, int> score in playerScores)
        {
            //Catch highest score.
            if(score.Value > highest)
            {
                highest = score.Value;
                id = score.Key;
            }
        }
        output.id = id;
        output.score = highest;
        output.player = getPlayerById(id);
        return output;
    }

    //Command is always run on server.
    //Called from clients, telling the server to update the player's public score.
    [Command]
    public void CmdUpdatePublicScore(uint id, int score)
    {
        playerScores[id] = score;
        RpcUpdateLocalScores(id, score);
        CmdCheckForWinners();
    }
    //Rpc is broadcast from server to all clients.
    //Update the local dictionaries for each client.
    [ClientRpc]
    public void RpcUpdateLocalScores(uint id, int score)
    {
        playerScores[id] = score;
    }

    //Command ensures this is done by the server.
    [Command]
    void CmdCheckForWinners()
    {
        foreach (KeyValuePair<uint, int> score in playerScores)
        {
            if(score.Value >= maxScore)
            {
                //Winner found
                //Run end of game procedure
                RpcDebugWin(getPlayerById(score.Key).GetComponent<CharactersScript>().username);
                CmdEndGame();

            }

        }
    }

    [ClientRpc]
    void RpcDebugWin(string winner)
    {
        Debug.Log("Winner is " + winner);
    }

    [Command]
    void CmdEndGame()
    {
        string leaderboad = generateLeaderboard();
        RpcEndTheGameProcedure(leaderboad);
    }

    [ClientRpc]
    void RpcEndTheGameProcedure(string leaderboardText)
    {
        var gameInfo = GameObject.Find("Network Manager").GetComponent<EndOfGameInfo>();
        gameInfo.leaderboardText = leaderboardText;
        gameInfo.gameEnded = true;
        NetworkManager.singleton.StopClient();
    }

    string generateLeaderboard()
    {
        Dictionary<uint, int> playersCopy = playerScores;
        string leaderText = "";
        int counter = 1;
        while (playersCopy.Count > 0) {
            int maxScore = -2147483648;
            uint maxId = 0;
            foreach (KeyValuePair<uint, int> score in playersCopy)
            {
                if(score.Value > maxScore)
                {
                    maxScore = score.Value;
                    maxId = score.Key;
                }
            }
            playersCopy.Remove(maxId);
            leaderText += counter + ") " 
                + getPlayerById(maxId).GetComponent<CharactersScript>().username 
                + " : " + maxScore + "\n";
            counter++;
        }

        return leaderText;
    }
}

