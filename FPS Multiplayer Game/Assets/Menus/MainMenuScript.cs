using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

    public Button btnHost;
    public Button btnJoin;
    public InputField inpIp;
    public InputField inpPort;

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
        Debug.Log("Host Clicked");
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    void btnJoinClick()
    {
        Debug.Log("Join Clicked");
    }
}
