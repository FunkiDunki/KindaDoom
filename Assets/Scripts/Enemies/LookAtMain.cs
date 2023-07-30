using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class LookAtMain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ConstraintSource source = new ConstraintSource();
        source.sourceTransform = Camera.main.transform;
        source.weight = 1.0f;
        GetComponent<LookAtConstraint>().AddSource(source);
    }
}
