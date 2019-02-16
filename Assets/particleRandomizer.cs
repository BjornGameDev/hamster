using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleRandomizer : MonoBehaviour
{
    public LineRenderer lr;

    public int sizeIndex;
    public int speedIndex;
    public int emissionIndex;
    public int rindex;
    public int gindex;
    public int bindex;
    ParticleSystem.EmissionModule emod;
    ParticleSystem.MainModule main;
    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem parts = GetComponent<ParticleSystem>();
        emod = parts.emission;
        main = parts.main;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (lr.positionCount<1) 
            return;


        main.startSize = float.Parse(Mathf.Abs(lr.GetPosition(sizeIndex).y).ToString().Split(',')[0]);
        main.startSpeed = float.Parse(Mathf.Abs(lr.GetPosition(speedIndex).y).ToString().Split(',')[0]);
        emod.rateOverTimeMultiplier = float.Parse(Mathf.Abs(lr.GetPosition(emissionIndex).y).ToString().Split(',')[0]);
    }
}
