using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class playerName : NetworkBehaviour
{
    [SyncVar]
    public string name;
}
