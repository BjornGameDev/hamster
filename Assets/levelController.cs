using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelController : MonoBehaviour
{
    public void endLevel()
    {
        //StartCoroutine(changeScene());
    }
    IEnumerator changeScene()
    {
        yield return new WaitForEndOfFrame();
        Color col = GetComponent<UnityEngine.UI.Image>().color;
        col.a += Time.deltaTime;
        GetComponent<UnityEngine.UI.Image>().color = col;
        if (col.a >= 1)
        {
           // UnityEngine.SceneManagement.SceneManager.LoadScene("endGame");
        }
        else
        {
          //  StartCoroutine(changeScene());
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
