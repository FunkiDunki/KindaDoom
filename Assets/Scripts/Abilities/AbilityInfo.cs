using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Info", menuName = "ScriptableObjects/AbilityInfo")]
public class AbilityInfo : ScriptableObject
{
    public float cooldown;
    public Sprite sprite;
    public int cost;
}
