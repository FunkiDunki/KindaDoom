using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SpawnRoomDoor : MonoBehaviour, IInteractable
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
        return "No going back now";
    }

    public void Interact()
    {
        
    }
}
