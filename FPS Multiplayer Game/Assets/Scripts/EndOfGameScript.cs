using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfGameScript : MonoBehaviour
{
    //This class handles the end of game learderboard pop-up.

    //Get the UI elements.
    [SerializeField]
    UnityEngine.UI.Text leaderboard;

    [SerializeField]
    GameObject mainMenuCanvas;

    [SerializeField]
    UnityEngine.UI.Button OkButton;

    //Game Info object contains a game ended boolean and a leaderboard string.
    EndOfGameInfo gameInfo;

    // Start is called before the first frame update
    void Start()
    {
        //Test whether the match ended or if the player left early.
        gameInfo = GameObject.Find("Network Manager").GetComponent<EndOfGameInfo>();
        if (gameInfo.gameEnded)
        {
            //Match just ended
            OkButton.onClick.AddListener(okButtonClicked);
            //Show screen
            gameObject.SetActive(true);
            mainMenuCanvas.SetActive(false);
            leaderboard.text = gameInfo.leaderboardText;

            gameInfo.gameEnded = false;
        }
        else
        {
            //Main menu opened but not after a match.
            //Show main menu, hide this screen.
            mainMenuCanvas.SetActive(true);
            gameObject.SetActive(false);
            
        }
    }

    //Ok button closes the pop-up.
    void okButtonClicked()
    {
        gameInfo.gameEnded = false;
        mainMenuCanvas.SetActive(true);
        gameObject.SetActive(false);
    }

}
