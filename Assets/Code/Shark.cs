using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    private QuantumManager _quantumMananger;
    public bool dropped = false;

    // Start is called before the first frame update
    void Start()
    {
        _quantumMananger = FindObjectOfType<QuantumManager>();
        StartCoroutine(releaseShark());
    }

    IEnumerator releaseShark()
    {
        yield return new WaitForSeconds(4f);
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
                float difference = 0.03f + (positions[i].y - positions[i + 1].y);
                transform.position = new Vector3(transform.position.x + difference, positions[i].y, 0);
            }
        }
    }

    public void audioChomp()
    {
        var dist = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

        var val = Random.value * 10;
        dist = Mathf.Clamp(dist, 0, 10);
        GetComponent<AudioSource>().volume = 1 - dist / 10;
        if (val < 2)
            GetComponent<AudioSource>().Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.name == "Hamster")
        {
            collision.collider.enabled = false;
            collision.collider.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<AudioSource>().Play();
            Time.timeScale = 0.2f;
            StartCoroutine(killHamster());
        }
    }

    IEnumerator killHamster()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1;
        PlayerPrefs.SetString("endText", "The shark has \neaten the hamster.\nNom nom.");
        GameObject.FindGameObjectWithTag("GameController").GetComponent<levelController>().endLevel();
    }
}
