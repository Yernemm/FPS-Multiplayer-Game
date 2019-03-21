using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfGameScript : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Text leaderboard;

    [SerializeField]
    GameObject mainMenuCanvas;

    [SerializeField]
    UnityEngine.UI.Button OkButton;

    EndOfGameInfo gameInfo;

    // Start is called before the first frame update
    void Start()
    {
        gameInfo = GameObject.Find("Network Manager").GetComponent<EndOfGameInfo>();
        if (gameInfo.gameEnded)
        {
            //game just ended
            OkButton.onClick.AddListener(okButtonClicked);

            gameObject.SetActive(true);
            mainMenuCanvas.SetActive(false);
            leaderboard.text = gameInfo.leaderboardText;

            gameInfo.gameEnded = false;
        }
        else
        {
            //main menu opened but not after a match
            mainMenuCanvas.SetActive(true);
            gameObject.SetActive(false);
            
        }
    }

    void okButtonClicked()
    {
        gameInfo.gameEnded = false;
        mainMenuCanvas.SetActive(true);
        gameObject.SetActive(false);
    }

}
