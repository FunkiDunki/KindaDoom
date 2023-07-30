using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AutoWeaponInfo", menuName = "ScriptableObjects/AutoWeaponInfo")]
public class AutoWeaponInfo : ScriptableObject
{
    public enum FireMethod
    {
        HITSCAN,
        PROJECTILE
    }

    public FireMethod fireMethod;
    public int shotdmg;
    public float fireRate;
    public float focalLength;
    public float maxSpread;
    public float minSpread;
    public float spreadGain;
    public float spreadLoss;
    public int maxAmmo;
    public TrailRenderer fireTrail;
    public int numBullets;
    public int shotCost;
}
