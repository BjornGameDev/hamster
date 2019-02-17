using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D;
    private QuantumManager _quantumMananger;
    public float helpSpeed = 0.2f;
    public bool dropped = false;
    private float bounce;
    private float _help = 0;
    private BoxCollider2D collider;
    public PhysicsMaterial2D bounceMaterial;
    public PhysicsMaterial2D solidMaterial;

    // Start is called before the first frame update
    void Start()
    {
        _quantumMananger = FindObjectOfType<QuantumManager>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(dropHamster());
        collider = GetComponent<BoxCollider2D>();
        collider.sharedMaterial = solidMaterial;

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
                _rigidBody2D.AddForce(new Vector2((difference * 20) + _help, 0));
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        collider.sharedMaterial = bounceMaterial;
        _help = helpSpeed;
        if (collision.collider.name == "Collider") {
            _rigidBody2D.AddForce(new Vector2(0,10f));

        }
    }
}
