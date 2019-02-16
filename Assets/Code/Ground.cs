using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    private QuantumManager _quantumMananger;
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _quantumMananger = FindObjectOfType<QuantumManager>();
        _player = FindObjectOfType<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] positions = _quantumMananger.plotPositions;
        if (positions.Length > 0)
        {
            //Place collider x under player.
            transform.position = new Vector3(_player.transform.position.x, transform.position.y, 0);
            int i = Mathf.RoundToInt(((transform.position.x + 20f) / 40f) * positions.Length);
            transform.position = new Vector3(transform.position.x, positions[i].y, 0);
        }
    }
}
