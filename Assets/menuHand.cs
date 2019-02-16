using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuHand : MonoBehaviour
{
    public anyKey startLevel;
    public string axisName;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        startLevel.change();
    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis(axisName) * Time.deltaTime, 0, 0);
    }
}
