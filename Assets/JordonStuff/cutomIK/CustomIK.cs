using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CustomIK : MonoBehaviour
{
    public Transform root, mid,tip, target;
    Quaternion rootStart, midstart;
    // Start is called before the first frame update
    void Start()
    {
        rootStart = root.localRotation;
        midstart = mid.localRotation;
    }
    // Update is called once per frame
    void Update()
    {
        root.localRotation = rootStart;
        mid.localRotation = midstart;


        Vector3 diff = -root.position + target.position;
        float poleRotation =  Vector3.SignedAngle(-root.up,  diff - root.forward * Vector3.Dot(diff, root.forward), root.forward);
        root.Rotate(root.forward, poleRotation);


        float a = (root.position - mid.position).magnitude;
        float b = (mid.position - tip.position).magnitude;
        float c = diff.magnitude;

        float C = -CosAngle(a, c, b);


        float towwardsAngle = Vector3.SignedAngle(root.forward, diff.normalized, root.right);
        Debug.Log(root.right);
        root.Rotate(root.right, towwardsAngle + C,Space.World);

        Vector3 finaldiff = mid.transform.position - target.position;

        mid.Rotate(-mid.right, 180 -Vector3.SignedAngle(mid.forward, finaldiff.normalized, mid.right), Space.World);


    }
    float CosAngle(float a, float b, float c)
    {
        if (!float.IsNaN(Mathf.Acos((-(c * c) + (a * a) + (b * b)) / (-2 * a * b)) * Mathf.Rad2Deg))
        {
            return Mathf.Acos((-(c * c) + (a * a) + (b * b)) / (2 * a * b)) * Mathf.Rad2Deg;
        }
        else
        {
            return 1;
        }
    }
}
