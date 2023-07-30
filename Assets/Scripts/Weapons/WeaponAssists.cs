using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WeaponAssists
{
    public static bool HitScanWithSpread(Weapon weapon, float theta, out RaycastHit hit, out Vector3 firedDirection)
    {
        Vector3 relativeDir = Vector3.forward;
        Vector2 delta = Random.insideUnitCircle * theta / 45.0f;
        relativeDir.x += delta.x;
        relativeDir.y += delta.y;
        firedDirection = Camera.main.transform.TransformDirection(relativeDir);

        //first, try to find object hit
        if (Physics.Raycast(Camera.main.transform.position, firedDirection, out hit))
        {
            return true;
        }
        return false;
    }

    public static IEnumerator SpawnFireTrail(TrailRenderer trail, Vector3 hit)
    {
        float time = 0;
        Vector3 startPosition = trail.transform.position;

        while (time < Vector3.Distance(startPosition, hit))
        {
            trail.transform.position = Vector3.Lerp(startPosition, hit, time / Vector3.Distance(startPosition, hit));
            time += Time.deltaTime / trail.time;

            yield return null;
        }

        MonoBehaviour.Destroy(trail.gameObject, trail.time);
    }
}
