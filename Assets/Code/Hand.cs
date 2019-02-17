﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private QuantumManager _quantumMananger;
    public string handName = "leftHand";
    private Vector3 _initialPosition;
    public float speed = 15;
    private float verticalSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        _initialPosition = transform.position;
        _quantumMananger = FindObjectOfType<QuantumManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_quantumMananger.plotPositions.Length > 0)
        {
            float wellPosition = _quantumMananger.getWellPosition(handName);
            transform.position = new Vector3(_initialPosition.x + (wellPosition * 50), _initialPosition.y , transform.position.z);

        }
    }
}
