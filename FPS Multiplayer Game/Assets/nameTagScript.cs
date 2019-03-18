using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class nameTagScript : MonoBehaviour
{
    [SerializeField]
    TextMeshPro tagtext;
    [SerializeField]
    GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        //tagtext = GetComponent<TextMeshPro>();
        tagtext.text = player.GetComponent<CharactersScript>().username;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
