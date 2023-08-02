using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegTarget : MonoBehaviour
{
    Vector3 targetPos;
    Vector3 lastPos;
    bool steping;
    float stepStart;
    float stepTime;
    float stepHeight;

    public GameObject rayCaster;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!steping){ transform.position = targetPos; return; }

        float percent = (Time.time - stepStart) / stepTime;
        if(percent > 1f) { steping = false; return; }


        Vector3 pathPoint = (targetPos - lastPos)*percent + lastPos;

        float heightFactor = (.5f - Mathf.Abs(percent - .5f)) * 2;

        Vector3 diff = targetPos - lastPos;

        pathPoint.y += stepHeight * heightFactor * diff.magnitude;

        transform.position = pathPoint;




    }
    public void setTargetPos()
    {
        Ray ray = new Ray(rayCaster.transform.position + (targetPos - lastPos)* .5f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 newPos = hit.point;
            lastPos = targetPos;
            targetPos = newPos;
        }
    }
    public void startstep(float time, float height)
    {
        setTargetPos();
        steping = true;
        stepTime = time;
        stepStart = Time.time;
        stepHeight = height;
    }
}
