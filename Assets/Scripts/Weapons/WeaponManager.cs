using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] List<Weapon> weaps;
    int curWeapon;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] GameObject weaponHolder;
    // Start is called before the first frame update
    void Start()
    {
        weaps = new List<Weapon>(weaponHolder.GetComponentsInChildren<Weapon>());
        foreach (Weapon weapon in weaps)
        {
            weapon.Init(ammoText);
            weapon.SetOwner(GetComponent<PlayerController>());
        }
        weaps[0].gameObject.SetActive(true);
        for (int i = 1; i < weaps.Count; i++)
        {
            weaps[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            weaps[curWeapon].UseDown();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            weaps[curWeapon].UseUp();
        }
        if (Input.GetKeyDown("r"))
        {
            weaps[curWeapon].UseReload();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            weaps[curWeapon].UseRightDown();
        }
        if (Input.GetButtonUp("Fire2"))
        {
            weaps[curWeapon].UseRightUp();
        }
        if (Input.GetButtonDown("Swap"))
        {
            StartSwapWeapon();
        }
    }

    public void SetShotLock(bool mode)
    {
        weaps[curWeapon].SetShotLock(mode);
    }

    void StartSwapWeapon()
    {
        weaps[curWeapon].OnAnimationFinish += WeaponSwapNext;
        weaps[curWeapon].StartSwap();
    }

    void WeaponSwapNext()
    {
        weaps[curWeapon].gameObject.SetActive(false);
        curWeapon = (++curWeapon) < weaps.Count ? curWeapon : 0;
        weaps[curWeapon].gameObject.SetActive(true);
        weaps[curWeapon].StartUnsheath();
    }
}
