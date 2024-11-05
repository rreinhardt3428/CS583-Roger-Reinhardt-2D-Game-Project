using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMovement : MonoBehaviour
{
    public float bulletSpeed = 10f;     // Speed of the bullet.

    void Update()
    {
        transform.position += Vector3.left * bulletSpeed * Time.deltaTime;  // Moves enemy bullets towards the left side
        Destroy(gameObject, 3f);    // Destroy bullets after 3 seconds of travel time
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null) // Check if the bullet hits an player
        {
            player.TakeDamage(); // Call the TakeDamage method on the player
            Destroy(gameObject); // Destroy the bullet upon collision
        }
    }
}
