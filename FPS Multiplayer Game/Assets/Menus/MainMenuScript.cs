using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MainMenuScript : NetworkBehaviour {

    public Button btnHost;
    public Button btnJoin;
    public InputField inpIp;
    public InputField inpPort;
    public InputField inpName;
    [SerializeField]
    NetworkManager net;

	// Use this for initialization
	void Start () {
        btnHost.onClick.AddListener(btnHostClick);
        btnJoin.onClick.AddListener(btnJoinClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void btnHostClick()
    {
        updateNetConfig();
        Debug.Log("Host Clicked");
        net.StartHost();
    }

    void btnJoinClick()
    {
        updateNetConfig();
        Debug.Log("Join Clicked");
        net.StartClient();
    }

    void updateNetConfig()
    {
        net.networkAddress = inpIp.text;
        net.matchPort = int.Parse(inpPort.text);
        net.GetComponent<playerName>().name = inpName.text;
    }
}
