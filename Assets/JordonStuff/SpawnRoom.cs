using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum RewardType
{
    None,
    Loot,
    Skill,
    Upgrade
}




public class SpawnRoom : MonoBehaviour
{
    public SpawnRoomDoor door;
    public static SpawnRoom instance;

    public GameObject lootReward, skillreward, upgradeReward;
    public GameObject roomLocation, rewardLocation;
    public GameObject currentReward;
    public GameObject currentRoom;

    public List<GameObject> roomPrefabs;

    private void Start()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
        int roomIndex = Random.Range(0, roomPrefabs.Count);
        currentRoom = Instantiate(roomPrefabs[roomIndex], roomLocation.transform.position, roomLocation.transform.rotation);

        currentReward = Instantiate(lootReward, rewardLocation.transform.position, rewardLocation.transform.rotation);

    }


    public void newRoom()
    {
        door.close();
        Destroy(currentRoom);

        int roomIndex = Random.Range(0, roomPrefabs.Count);
        currentRoom = Instantiate(roomPrefabs[roomIndex], roomLocation.transform.position, roomLocation.transform.rotation);
    }

    public void setReward(GameObject reward)
    {
        if(currentReward != null)
        {
            Destroy(currentReward);
            currentReward = null;
        }
        currentReward = Instantiate(reward, rewardLocation.transform.position, rewardLocation.transform.rotation);
              

    }
}
