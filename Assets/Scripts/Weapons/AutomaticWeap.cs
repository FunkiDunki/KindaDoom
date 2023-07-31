using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AutomaticWeap : Weapon
{
    bool held;
    [SerializeField] AutoWeaponInfo info;
    float curSpread;

    // Start is called before the first frame update
    void Start()
    {
        held = false;
    }

    public override void Init(TextMeshProUGUI aText)
    {
        _Init(info.maxAmmo, 1.0f / info.fireRate, aText);
    }
    public override void UseDown()
    {
        held = true;
    }
    public override void UseUp()
    {
        held = false;
    }
    public override void UseRightDown()
    {
        anim.SetTrigger("Scope");
    }
    public override void UseRightUp()
    {
        anim.SetTrigger("UnScope");
    }



    public void HitScanFire()
    {
        RaycastHit hit;
        Vector3 firedDirection;//relative to camera
        if (WeaponAssists.HitScanWithSpread(this, curSpread, out hit, out firedDirection))
        {
            Damageable damageable;
            if (hit.transform.TryGetComponent(out damageable))
            {
                if (hit.collider.CompareTag("critical"))
                {
                    damageable.Damage(info.shotdmg * 1.5f);
                }
                else
                {
                    damageable.Damage(info.shotdmg);
                }
            }
        }

        GameObject trail = Instantiate(info.fireTrail, shootPoint.transform.position, Quaternion.identity);
        Vector3 hitPos = Camera.main.transform.position + firedDirection * info.focalLength;
        trail.transform.LookAt(hitPos);
        Destroy(trail, 1.0f);
        

    }

    public virtual void Fire()
    {
        if (info.fireMethod == AutoWeaponInfo.FireMethod.HITSCAN)
        {
            for (int i = 0; i < info.numBullets; i++)
            {
                HitScanFire();
            }
            UseAmmo(info.shotCost);

            curDelay = maxDelay;
            anim.SetTrigger("Shoot");
            curSpread += info.spreadGain;
            curSpread = Mathf.Min(curSpread, info.maxSpread);
        }
    }

    protected override void Update()
    {
        base.Update();
        if (held && curDelay <= 0.0f && curAmmo > 0 && !shotLock && anim.GetCurrentAnimatorStateInfo(0).IsTag("cancelable"))
        {
            Fire();
        }

        if (curDelay <= 0.0f)
        {
            curSpread -= Time.deltaTime * info.spreadLoss;
            curSpread = Mathf.Max(curSpread, info.minSpread);
        }

    }
}
