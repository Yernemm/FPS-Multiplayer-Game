using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadAnimation : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    
    public void animationFinished()
    {
        Debug.Log("Reload Done");
        player.GetComponent<playerController>().reloadFinished();
    }
}
