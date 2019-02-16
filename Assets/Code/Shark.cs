using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
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
            float difference = 0.03f + (positions[i].y - positions[i + 1].y);
            transform.position = new Vector3(transform.position.x+difference, positions[i].y, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.name == "Hamster")
        {
            PlayerPrefs.SetString("endText", "The shark has \neaten the hamster.\nNom nom.");
        }
        GameObject.FindGameObjectWithTag("GameController").GetComponent<levelController>().endLevel();
    }
}
