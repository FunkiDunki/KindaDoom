using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GainCellScript : Ability
{
    public override void Init(Image abilityDisplay, TextMeshProUGUI cooldownText, Image coverImage, AbilityInfo info)
    {
        base._Init(abilityDisplay, cooldownText, coverImage, info);    
    }

    public override bool UseDown()
    {
        GameObject.Find("Player").transform.GetComponent<AbilityManager>().GetCell();
        curCooldown = cooldown;
        return true;
    }

    public override bool UseUp()
    {
        return true;
    }
}
