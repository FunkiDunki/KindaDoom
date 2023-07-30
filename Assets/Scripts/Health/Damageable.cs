using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Damageable : MonoBehaviour
{
    public UnityEvent DeathEvent;
    public float maxHealth { get; protected set; }
    public float curHealth { get; protected set; }

    public void _Init(float maxHealth)
    {
        this.maxHealth = maxHealth;
        this.curHealth = maxHealth;
    }

    public virtual void Damage(float dmg)
    {
        curHealth -= dmg;
        if (curHealth <= 0)
        {
            OnDeath();
        }
    }


    public abstract void DisplayHealth();
    public virtual void OnDeath()
    {
        DeathEvent.Invoke();
    }
}
