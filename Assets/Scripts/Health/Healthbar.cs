using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    GameObject subject;

    private void Start()
    {
        subject = transform.GetChild(0).gameObject;
    }

    public void Display(float cur, float max)
    {
        subject.transform.localScale = 0.95f * (new Vector3(cur / max, 1, 1));
    }
}
