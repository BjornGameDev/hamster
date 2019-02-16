using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
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
        Vector3[] positions = _quantumMananger.plotPositions;
        if (positions.Length > 0)
        {
            int i = Mathf.RoundToInt(((transform.position.x + 20f) / 40f) * positions.Length);
            transform.position = new Vector3(transform.position.x, positions[i].y, 0);



        }
    }
}
