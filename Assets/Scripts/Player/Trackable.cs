using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trackable : MonoBehaviour
{
    public static List<Trackable> trackables = new List<Trackable>();

    // Start is called before the first frame update
    private void OnLevelWasLoaded(int level)
    {
        trackables.Clear();
    }
    void Start()
    {
        trackables.Add(this);
    }

    public static Trackable[] GetAllTrackables()
    {
        return trackables.ToArray();
    }
}
