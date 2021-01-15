using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawnerDelete : MonoBehaviour
{
    [SerializeField] GameObject pickupPrefab;
    [SerializeField] float minTimeDelay = 1f;
    [SerializeField] float maxTimeDelay = 2f;
    bool spawning = true;

    IEnumerator Start()
    {
        while (spawning)
        {
            yield return new WaitForSeconds(Random.Range(minTimeDelay, maxTimeDelay));
            Instantiate(pickupPrefab, transform.position, transform.rotation);
        }

    }
}
