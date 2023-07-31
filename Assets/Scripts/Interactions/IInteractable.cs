using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public string GetInteractionName();
    public void Interact();
    public bool InteractActive()
    {
        return true;
    }
}
