using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anyKey : MonoBehaviour
{
    public bool PressAnyKey;
    public string loadableScreen;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(activator());
    }
    float startTimer = 0;
    bool canchange = false;
    IEnumerator activator()
    {
        yield return new WaitForEndOfFrame();
        startTimer += Time.deltaTime;
        if (startTimer >= 2)
        {
            canchange = true;
        }
        else
        {
            if(startTimer > 1)
            {
                Color col = GetComponent<UnityEngine.UI.Image>().color;
                col.a -= Time.deltaTime;
                GetComponent<UnityEngine.UI.Image>().color = col;
            }
            StartCoroutine(activator());
        }
        
}
    private void Update()
    {
        if(canchange && PressAnyKey && Input.anyKeyDown)
        {
            change();
        }
    }
    IEnumerator changeScene()
    {
        yield return new WaitForEndOfFrame();
        Color col = GetComponent<UnityEngine.UI.Image>().color;
        col.a += Time.deltaTime;
        GetComponent<UnityEngine.UI.Image>().color = col;
        if (col.a>=1){
            UnityEngine.SceneManagement.SceneManager.LoadScene(loadableScreen);
        }
        else
        {
            StartCoroutine(changeScene());
        }
    }

    // Update is called once per frame
public void change()
    {

            StartCoroutine(changeScene());

    }
}
