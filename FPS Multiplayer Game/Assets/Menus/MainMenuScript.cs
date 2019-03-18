using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MainMenuScript : NetworkBehaviour {
    //Get all the input fields.
    public Button btnHost;
    public Button btnJoin;
    public InputField inpIp;
    public InputField inpPort;
    public InputField inpName;
    [SerializeField]
    NetworkManager net; //Network manager object.

	// Use this for initialization
	void Start () {
        //Add listener events to the buttons.
        btnHost.onClick.AddListener(btnHostClick);
        btnJoin.onClick.AddListener(btnJoinClick);
	}
	
    //Handle the host button.
    void btnHostClick()
    {
        updateNetConfig();
        Debug.Log("Host Clicked");
        net.StartHost();
    }
    //Handle the join button.
    void btnJoinClick()
    {
        updateNetConfig();
        Debug.Log("Join Clicked");
        net.StartClient();
    }
    //Procedure for updating the network configuration with the entered
    //address, port and username.
    void updateNetConfig()
    {
        net.networkAddress = inpIp.text; //Change IP address to the text box value.
        net.matchPort = int.Parse(inpPort.text); //Parse input port.
        net.GetComponent<playerName>().name = inpName.text; //Set the username in the playerName script.
    }
}
