using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class nameTagScript : MonoBehaviour
{

    TextMeshPro tagtext;


    // Start is called before the first frame update
    void Start()
    {
        tagtext = GetComponent<TextMeshPro>();
        tagtext.text = GetComponent<playerController>().currentCharacter.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
