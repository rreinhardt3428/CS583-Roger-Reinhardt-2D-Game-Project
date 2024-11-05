using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;   // Reference to the enemy prefab
    public GameObject strongEnemyPrefab;   // Reference to the enemy prefab
    public float enemySpawnInterval = 2f; // Time between spawns
    public float spawnStartTime = 3f;

    private bool spawnStrongEnemies = false;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(spawnStartTime);

        while (true)
        {
            GameObject enemyToSpawn = spawnStrongEnemies && Random.value > 0.5f ? strongEnemyPrefab : enemyPrefab;
            Instantiate(enemyToSpawn, new Vector3(10f, 0, 0), Quaternion.identity);
            yield return new WaitForSeconds(enemySpawnInterval);
        }
    }

    public void updateEnemyPrefab(GameObject newPrefab) 
    {
        enemyPrefab = newPrefab;
    }

    public void EnableStrongEnemies()
    {
        spawnStrongEnemies = true;
    }

}

