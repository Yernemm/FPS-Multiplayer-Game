using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public double timeLeft;

	// Use this for initialization
	void Start () {
        timeLeft = 600;
       
	}
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        

    }
}
