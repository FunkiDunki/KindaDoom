using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageScreenEffect : MonoBehaviour
{
    [SerializeField] Image redImg;
    [SerializeField] float flashTime;
    bool isHit = false;
    [SerializeField] float maxAlpha;

    // Update is called once per frame
    void Update()
    {
        if (isHit)
        {
            redImg.color = new Color(redImg.color.r, redImg.color.g, redImg.color.b, maxAlpha);
        }
        else
        {
            redImg.color = new Color(redImg.color.r, redImg.color.g, redImg.color.b, Mathf.Lerp(redImg.color.a, 0.0f, Time.deltaTime / flashTime));
        }
        isHit = false;
    }

    public void GetHit(float ft)
    {
        flashTime = ft;
        isHit = true;
    }
}
