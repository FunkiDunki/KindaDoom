using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthbar : MonoBehaviour
{
    public void DisplayHealth(float p)
    {
        transform.localScale = new Vector3(Mathf.Clamp(p, 0.0f, 1.0f), 1.0f, 1.0f);
    }
}
