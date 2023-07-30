using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public abstract class Weapon : MonoBehaviour
{
    public int maxAmmo { get; protected set; }
    [SerializeField] public int curAmmo { get; protected set; }
    public float maxDelay { get; protected set; }
    public float curDelay { get; protected set; }
    [SerializeField] public GameObject shootPoint;
    protected TextMeshProUGUI ammoText;
    protected bool shotLock;
    [SerializeField] protected Animator anim;
    public event Action OnAnimationFinish;
    protected PlayerController owner;

    public virtual void _Init(int maxAmmo, float maxDelay, TextMeshProUGUI aText)
    {
        this.maxAmmo = maxAmmo;
        this.maxDelay = maxDelay;
        this.curAmmo = maxAmmo;
        this.curDelay = 0.0f;
        ammoText = aText;
        shotLock = false;
    }

    public void SetOwner(PlayerController o)
    {
        owner = o;
    }

    public void SheathFinishEvent()
    {
        OnAnimationFinish?.Invoke();
        OnAnimationFinish = null;
    }

    public void StartSwap()
    {
        anim.SetTrigger("Swap");
    }

    public void StartUnsheath()
    {
        anim.SetTrigger("Unsheath");
    }

    public abstract void Init(TextMeshProUGUI aText);

    protected virtual void Update()
    {
        curDelay -= Time.deltaTime;
        DisplayAmmo();
    }

    protected void UseAmmo(int ammount)
    {
        curAmmo -= ammount;
        if (curAmmo < 0)
        {
            curAmmo = 0;
        }
    }

    public void SetShotLock(bool mode)
    {
        shotLock = mode;
    }

    public void ReloadAmmo(int ammount)
    {
        curAmmo += ammount;
    }

    public void ReloadAmmo()
    {
        curAmmo = maxAmmo;
    }

    public virtual void FinishReload()
    {
        ReloadAmmo();
    }

    public virtual void UseDown()
    {
        return;
    }
    public virtual void UseUp()
    {
        return;
    }
    public virtual void UseMiddle()
    {
        return;
    }
    public virtual void UseRightDown() { return; }
    public virtual void UseRightUp() { return; }

    public virtual void UseReload()
    {
        //play animation (the ammo should be reloaded in the animation behaviour script)
        anim.SetTrigger("Reload");
    }

    public virtual void DisplayAmmo()
    {
        ammoText.text = curAmmo.ToString() + " / " + maxAmmo.ToString();
    }
}
