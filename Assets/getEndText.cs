using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getEndText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMesh>().text= PlayerPrefs.GetString("endText");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
