using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ProceduralWalk : MonoBehaviour
{
    public List<LegTarget> legTargets;
    public float stepPercent;
    public float stepLength;
    public float stepHeight;
    int legIndex;
    float timeSinceStepped;
    // Start is called before the first frame update
    void Start()
    {
        legIndex = 0;
        timeSinceStepped = 0;

       
    }

    void step()
    {
        timeSinceStepped = 0;
        legIndex += 1;
        legIndex = legIndex % legTargets.Count;

        legTargets[legIndex].startstep(stepLength, stepHeight);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeSinceStepped > stepLength * stepPercent)
        {
            step();
            Debug.Log("step");
        }
        else
        {
            timeSinceStepped += Time.deltaTime;
        }

    }
}
