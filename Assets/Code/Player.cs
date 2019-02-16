﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D;
    private QuantumManager _quantumMananger;
    // Start is called before the first frame update
    void Start()
    {
        _quantumMananger = FindObjectOfType<QuantumManager>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] positions = _quantumMananger.plotPositions;
        if (positions.Length > 0)
        {
            int i = Mathf.RoundToInt(((transform.position.x + 20f) / 40f) * positions.Length);
            float difference = positions[i].y - positions[i + 1].y;
            _rigidBody2D.AddForce(new Vector2(difference*20,0));
            //transform.position = new Vector3(transform.position.x, positions[i].y, 0);
        }
    }
}
