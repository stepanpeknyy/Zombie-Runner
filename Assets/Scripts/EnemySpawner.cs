using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float minTimeDelay=1f;
    [SerializeField] float maxTimeDelay = 2f;
    bool spawning = true;

    IEnumerator Start()
    {
        while (spawning)
        {
            yield return new WaitForSeconds(Random.Range(minTimeDelay, maxTimeDelay));
            Instantiate(enemyPrefab, transform.position, transform.rotation);
        }

    }
}
