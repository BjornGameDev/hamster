using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private QuantumManager _quantumMananger;
    public string handName = "leftHand";
    private Vector3 _initialPosition;
    public float speed = 15;
    private float verticalSpeed = 1;
    public bool handPlaced = false;
    // Start is called before the first frame update
    void Start()
    {
        _initialPosition = transform.position;
        _quantumMananger = FindObjectOfType<QuantumManager>();
        if(handName == "leftHand")
        {
            transform.position = new Vector3(_initialPosition.x - 60, _initialPosition.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(_initialPosition.x + 60, _initialPosition.y, transform.position.z);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(handPlaced)
        {
            if (_quantumMananger.plotPositions.Length > 0)
            {
                 float wellPosition = _quantumMananger.getWellPosition(handName);
                 transform.position = new Vector3(_initialPosition.x + (wellPosition * 20), _initialPosition.y , transform.position.z);
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, _initialPosition, Time.deltaTime);
            if(Vector3.Distance(transform.position,_initialPosition) < 0.1f)
            {
                handPlaced = true;
            }
        }
 
    }
}
