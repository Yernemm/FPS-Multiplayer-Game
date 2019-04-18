using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MainMenuScript : NetworkBehaviour {

    //This class handles the I/O for the main menu and the logic for connecting to a match.

    //Get all the input fields.
    public Button btnHost;
    public Button btnJoin;
    public InputField inpIp;
    public InputField inpPort;
    public InputField inpName;
    public UnityEngine.UI.Text inputValidationText;

    [SerializeField]
    NetworkManager net; //Network manager object.

	// Use this for initialization
	void Start () {
        net = NetworkManager.singleton;
        Cursor.lockState = CursorLockMode.None;
        //Add listener events to the buttons.
        btnHost.onClick.AddListener(btnHostClick);
        btnJoin.onClick.AddListener(btnJoinClick);
	}
	
    //Handle the host button.
    void btnHostClick()
    {
        if (inputValidation())
        {
            updateNetConfig();
            Debug.Log("Host Clicked");
            net.StartHost();
        }
    }
    //Handle the join button.
    void btnJoinClick()
    {
        if (inputValidation())
        {
            updateNetConfig();
            Debug.Log("Join Clicked");
            net.StartClient();
        }
    }
    //Procedure for updating the network configuration with the entered
    //address, port and username.
    void updateNetConfig()
    {
        net.networkAddress = inpIp.text; //Change IP address to the text box value.
        net.matchPort = int.Parse(inpPort.text); //Parse input port.
        net.GetComponent<playerName>().name = inpName.text; //Set the username in the playerName script.
    }

    //Validate the input username string.
    bool validateUsernameString(string username)
    {
        bool valid = true;
        if (username.Length < 1)
            valid = false;
        if (username.Length > 20)
            valid = false;
        return valid;
    }

    //Validate the input boxes.
    bool inputValidation()
    {
        if (validateUsernameString(inpName.text))
            return true;
        else
        {
            inputValidationText.text = "Invalid username";
            return false;
        }
    }
}
