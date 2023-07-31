using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardChest : MonoBehaviour, IInteractable
{
    bool open;
    Animator animator;

    public bool InteractActive()
    {
        return !open;
    }


    public string GetInteractionName()
    {
        return "Open";
    }

    public void Interact()
    {
        animator.SetTrigger("open");
        open = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
