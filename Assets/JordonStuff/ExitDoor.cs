using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Animations;


public class ExitDoor : MonoBehaviour, IInteractable
{
    public DoorScript doorScript;
    public GameObject decoySpawnroom;
    public RewardType rewardType;
    public GameObject reward;


    static bool openable = true;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.TryGetComponent(out PlayerController player))
        {
            Vector3 relPos = player.transform.position - transform.position;
            player.transform.position = SpawnRoom.instance.door.transform.position + relPos;
            SpawnRoom.instance.newRoom();
            openable = true;


        }

    }

    public bool InteractActive()
    {
        return openable;
    }

    public string GetInteractionName()
    {
        return "Open door for " + rewardType.ToString() + " reward";
    }

    public void Interact()
    {
        doorScript.open();
        SpawnRoom.instance.door.open();
        decoySpawnroom.SetActive(true);
        decoySpawnroom.GetComponent<DecoySpawnRoom>().spawnReward(reward);
        SpawnRoom.instance.setReward(reward);
        openable = false;

    }



}
