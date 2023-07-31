using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoySpawnRoom : MonoBehaviour
{
    public GameObject rewardLocation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void spawnReward(GameObject reward)
    {
        GameObject g = Instantiate(reward, rewardLocation.transform);

    }
}
