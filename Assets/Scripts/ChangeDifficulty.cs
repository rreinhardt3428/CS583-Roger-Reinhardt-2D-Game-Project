using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ChangeDifficulty : MonoBehaviour
{
    public float difficultyIncreaseInterval = 30f;      // Time interval to increase difficulty.
    public EnemySpawner enemySpawner;                   // Reference to the EnemySpawner script
    public GameObject enemy;                            // Reference to the basic enemy prefab.
    public GameObject strongEnemy;                      // Reference to the strong enemy prefab.

    private float timer = 0f;   // Timer to track the elapsed time since the last difficulty increase.
    private bool strongEnemiesEnabled = false;  // Flag to check if strong enemies have been enabled.
    void Update()                                    
    {
        timer += Time.deltaTime;    // Increment the timer by the time that has passed since the last frame.

        if (timer > difficultyIncreaseInterval && !strongEnemiesEnabled)    // Check if the timer is past difficulty increase interval and strong enemies are not spawning
        {
            strongEnemiesEnabled = true;    // Begin spawning strong enemies
            enemySpawner.EnableStrongEnemies();     // Call method on enemySpawner to enable spawning of strong enemies.
        }
    }
}