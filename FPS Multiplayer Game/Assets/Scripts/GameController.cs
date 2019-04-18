using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameController : NetworkBehaviour {
    
    //This class provides the basic funtionality used to run the match, including keeping the score, time and end of game procedure.

    //SyncVar synchronises the state of the variable between the server and the clients.
    [SyncVar]
    public double timeLeft;
    [SyncVar]
    public double time;
    //Dictionary pairs player IDs with player's score.
    //Synced over network.
    
    public Dictionary<uint, int> playerScores;

    //Score needed to win the match.
    public const int maxScore = 40000;
    //Functions allow other scripts to get the value of the max score.
    public int getMaxScore()
    {
        return maxScore;
    }
   
    //Text displayed on the screen if debug mode is enabled.
    //Displays additional information, not in the final game.
    public string debugText = "";
    public void addDebugText(string t)
    {
        debugText += t + "\n";
    }

	// Use this for initialization
	void Start () {
        timeLeft = 600; //The match is 10 minutes long at most.
        //Generic code for setting up the networked match.
        NetworkClient myClient;
        myClient = new NetworkClient();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        myClient.Connect("127.0.0.1", 4444);
        //Initialise dictionary.
        //Dictionary used to keep a scoreboard of the player scores.
        playerScores = new Dictionary<uint, int>();

    }
	
	// Update is called once per frame
	void Update () {
        //Time variables updated to keep the match timer.
        //Time.deltaTime is the time elapsed since the previous instance this was called.
        time += Time.deltaTime;
        timeLeft -= Time.deltaTime;
        //Only run the code below on the server.
        if (isServer)
        {
            //If the time runs out, end the game.
            if (timeLeft <= 0)
                CmdEndGame();
        }
        
    }

    //LateUpdate is run after all of the update functions on all scripts are finished.
    //Debug text is reset.
    private void LateUpdate()
    {
        debugText = "";
    }

    //Display a debug console message when the player connects to a server.
    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server");
    }

    //Function which returns a player object based on their unique player ID.
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

    //Debug function for testing the win conditions.
    [ClientRpc]
    void RpcDebugWin(string winner)
    {
        Debug.Log("Winner is " + winner);
    }

    //This function is called when the time ends or a player reaches the max score.
    //It ends the match for all players and sends out the leaderboard values.
    [Command]
    void CmdEndGame()
    {
        string leaderboad = generateLeaderboard();
        RpcEndTheGameProcedure(leaderboad);
    }

    //When the client receives the end match signal, it runs the end of match procedure.
    [ClientRpc]
    void RpcEndTheGameProcedure(string leaderboardText)
    {
        //Display the leaderboard screen and disconnect the player from the match.
        var gameInfo = GameObject.Find("Network Manager").GetComponent<EndOfGameInfo>();
        gameInfo.leaderboardText = leaderboardText;
        gameInfo.gameEnded = true;
        NetworkManager.singleton.StopClient();
    }

    //Function generates the leaderboard text based on the player scores.
    //Uses similar logic to getTopPlayer() but instead of returning the player, it sorts all the players.
    string generateLeaderboard()
    {
        Dictionary<uint, int> playersCopy = playerScores;
        string leaderText = "";
        int counter = 1;
        //While loop sorts and ranks the players by their score from highest to lowest.
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

