using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    Animator animator;
    public Collider colider;
    void Start()
    {
        enabled = true;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void open()
    {
        animator.SetTrigger("open");
        colider.enabled = false;
    }

    public void close()
    {
        animator.SetTrigger("close");
        colider.enabled = true;
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
