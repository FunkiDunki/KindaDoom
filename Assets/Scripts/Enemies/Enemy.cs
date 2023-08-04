using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : Damageable
{
    [SerializeField] float healthMax;
    Healthbar healthbar;

    //TRACKING STUFFS
    protected GameObject target; //tracking this object (agro)
    protected Vector3 target_pos;
    public float agro_distance;//how close before you are noticed;
    public float agro_loss; //how far before you are forgotten;
    NavMeshAgent nm;


    private bool navActive;

    public bool NavActive
    {
        get => navActive; protected set
        {
            nm.enabled = value;
            navActive = value;
        }
    }

    public override void DisplayHealth()
    {
        healthbar.Display(base.curHealth, base.maxHealth);
    }

    public override void OnDeath()
    {
        base.OnDeath();
        curHealth = maxHealth;
        print("Killed enemy");
        Destroy(gameObject);

    }

    // Start is called before the first frame update
    protected void _Init()
    {
        _Init(healthMax);
        healthbar = GetComponentInChildren<Healthbar>();
        nm = GetComponent<NavMeshAgent>();
        target_pos = transform.position;
        NavActive = true;
    }

    protected void _Update()
    {
        DisplayHealth();
        if (navActive)
        {
            Observe();
            TrackTarget();
        }
    }

    private void Observe()
    {
        if (target != null)//we already have a target
        {
            target_pos = target.transform.position;
            if (Vector3.Distance(target_pos, transform.position) > agro_loss)
            {
                //we lost the target
                target = null;
                return;
            }
            else
            {
                //keep tracking the target
                return;
            }

        }

        //look for new targets:
        /*else
        {
            Trackable[] trackables = Trackable.GetAllTrackables();
            for (int i = 0; i < trackables.Length; i++)
            {
                if (Vector3.Distance(trackables[i].transform.position, transform.position) < agro_distance)
                {
                    //this one is close enough:
                    target = trackables[i].gameObject;
                    target_pos = target.transform.position;
                    return;
                }
            }
            //may return here if nothing is close enough to track!
        }*/

    }

    private void TrackTarget()
    {
        if (target_pos == null)
        {
            return;//guard clause
        }
        //nm.SetDestination(target_pos);
        nm.SetDestination(target_pos);
    }
}
