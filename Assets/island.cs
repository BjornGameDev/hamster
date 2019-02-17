using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class island : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Hamster")
        {
            PlayerPrefs.SetString("endText", "The hamster has\nreached the island");
            GameObject.FindGameObjectWithTag("GameController").GetComponent<levelController>().endLevel();
            GetComponent<AudioSource>().Play();
        }
        else if (collision.name == "Shark")
        {
            PlayerPrefs.SetString("endText", "The shark has\nreached the island");
            GameObject.FindGameObjectWithTag("GameController").GetComponent<levelController>().endLevel();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
