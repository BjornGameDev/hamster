using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private QuantumManager _quantumMananger;
    // Start is called before the first frame update
    void Start()
    {
        _quantumMananger = FindObjectOfType<QuantumManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
