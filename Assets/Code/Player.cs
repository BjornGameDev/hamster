using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D;
    private QuantumManager _quantumMananger;
    public float helpSpeed = 0.2f;
    public bool dropped = false;
    // Start is called before the first frame update
    void Start()
    {
        _quantumMananger = FindObjectOfType<QuantumManager>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(dropHamster());
    }

    IEnumerator dropHamster()
    {
        yield return new WaitForSeconds(3f);
        _rigidBody2D.simulated = true;
        dropped = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (dropped)
        {
            Vector3[] positions = _quantumMananger.plotPositions;
            if (positions.Length > 0)
            {
                int i = Mathf.RoundToInt(((transform.position.x + 20f) / 40f) * positions.Length);
                float difference = positions[i].y - positions[i + 1].y;
                _rigidBody2D.AddForce(new Vector2((difference * 20) + helpSpeed, 0));
                //transform.position = new Vector3(transform.position.x, positions[i].y, 0);
            }
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.collider.name == "Collider")
        {
            _rigidBody2D.AddForce(new Vector2(0,2),ForceMode2D.Impulse);

        }
    }
}
