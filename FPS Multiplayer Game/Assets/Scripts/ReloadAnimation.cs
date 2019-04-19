using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadAnimation : MonoBehaviour
{
    //This class plays the reload animation on the player model.
    //It uses a different class from other animations because it has to animate two separate 
    //so it has to be placed on a different object.
    [SerializeField]
    GameObject player;
    
    public void animationFinished()
    {
        Debug.Log("Reload Done");
        player.GetComponent<playerController>().reloadFinished();
    }
}
