using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private QuantumManager _quantumMananger;
    public string handName = "leftHand";
    private Vector3 _initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        _initialPosition = transform.position;
        _quantumMananger = FindObjectOfType<QuantumManager>();
    }

    

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(_initialPosition.x + (_quantumMananger.getWellPosition(handName)*10), _initialPosition.y, 0);
    }
}
