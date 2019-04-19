using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class playerName : NetworkBehaviour
{
    //This class holds the player name.
    //It is a separate class from other properties because it has to be passed from the main menu to the game.
    //The other player properties are set during the game.
    [SyncVar]
    public string name;
}
