using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;         // Bullet prefab to instantiate
    public float bulletSpeed = 10f;         // Speed of the bullet
    public float fireRate = 3f;             // Bullets per second
    public float boostedFireRate = 10f;     // Bullets per second
    public bool shootingEnabled = true;     //Indicate whether player can shoot or not

    public AudioClip shootSound;            // Audio clip for shooting sound
    private AudioSource audioSource;        // Reference to the AudioSource

    private float nextFireTime = 0f;        // Time when the next bullet can be fired
    

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get AudioSource component
    }
    void Update()
    {
        float currentFireRate = fireRate;   // Assigns default fire rate
            
            if (!shootingEnabled)   // Disables firing if shootingEnabled is false
            {
                return;
            }

            if (Input.GetKey(KeyCode.LeftShift))    // Get input for "focus"
            {
                currentFireRate = boostedFireRate; // Increase fire rate when focused
            }

            if (Input.GetKey("z"))  // Check if the "Z" key is held down
        {
            if (Time.time >= nextFireTime)          
            {
                FireBullet();
                    nextFireTime = Time.time + (1f / currentFireRate); // Calculate next fire time
                }
            }
    }

    void FireBullet()
    {
        // Instantiate a bullet at the player's position
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity); // Use Quaternion.identity for rotation

        Rigidbody rb = bullet.GetComponent<Rigidbody>();    // Get the Rigidbody component 

        if (rb != null)
        {
            // Set the velocity to move always along the right direction
            rb.velocity = new Vector3(bulletSpeed, 0f, 0f); // Always moves to the right
        }
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound); // Play the sound effect
        }
    }
}
