using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] GameObject pickupPrefab;
    [SerializeField] float minTimeDelay = 1f;
    [SerializeField] float maxTimeDelay = 2f;
    bool spawningPickups = true;
    bool isPlayerInsideSwapner =false;

    IEnumerator SpawnPickup()
    {
        if (spawningPickups == true && isPlayerInsideSwapner ==true)
        {
            yield return new WaitForSeconds(Random.Range(minTimeDelay, maxTimeDelay));
            Instantiate(pickupPrefab, transform.position, transform.rotation);
            Debug.Log("spawned " + pickupPrefab.name);
            spawningPickups = false;
        }

    }

    private void Start()
    {
        Instantiate(pickupPrefab, transform.position, transform.rotation);
    }

    public void SetSpawnTrue()
    {
        spawningPickups = true;
        StartCoroutine(SpawnPickup());
    }

    private void OnTriggerEnter(Collider other)
    {
        isPlayerInsideSwapner = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isPlayerInsideSwapner = false;
    }

    public bool CheckPlayer()
    {
        return isPlayerInsideSwapner;
    }
}
