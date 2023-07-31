using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoomExitDoor : MonoBehaviour, IInteractable
{
    public DoorScript doorScript;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void open()
    {
        doorScript.open();
    }

    public void close()
    {
        doorScript.close();
    }

    public string GetInteractionName()
    {
        return "Enter next area";
    }

    public void Interact()
    {
        doorScript.open();

    }
    
    public void OnTriggerEnter(Collider other)
    {
        if(other.transform.TryGetComponent(out PlayerController C))
        {
            doorScript.close();
        }
    }
}
