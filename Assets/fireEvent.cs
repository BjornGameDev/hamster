using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireEvent : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent animEvent;

    public void fireAnimEvent()
    {
        animEvent.Invoke();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
