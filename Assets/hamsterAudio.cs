using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hamsterAudio : MonoBehaviour
{
    public void playRandomHamsterAudio()
    {
        if (Random.value < 0.2f)
        {
            GetComponent<AudioSource>().Play();
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
