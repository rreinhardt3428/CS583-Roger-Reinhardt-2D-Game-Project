using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float bulletSpeed = 10f;     // Default bullet movement speed
    void Update()
    {
        transform.position += Vector3.right * bulletSpeed * Time.deltaTime;     // Moves player bullets towards the right side 
        Destroy(gameObject, 3f);    // Destroy bullets after 3 seconds of travel time
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        StrongEnemy strongEnemy = other.gameObject.GetComponent<StrongEnemy>();
        if (enemy != null) // Check if the bullet hits an enemy
        {
            enemy.TakeDamage(); // Call the TakeDamage method on the enemy
            Destroy(gameObject); // Destroy the bullet upon collision
        }
        if (strongEnemy != null) // Check if the bullet hits an enemy
        {
            strongEnemy.TakeDamage(); // Call the TakeDamage method on the enemy
            Destroy(gameObject); // Destroy the bullet upon collision
        }
    }
}
