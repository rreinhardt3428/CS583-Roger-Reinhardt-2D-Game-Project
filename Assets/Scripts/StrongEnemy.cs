using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class StrongEnemy : MonoBehaviour
{
    public ScoreCounter scoreCounter;       // Reference to the ScoreCounter

    public int health = 8;                  // Health of the enemy (5 hits to destroy)

    public GameObject bulletPrefab;         // Reference to the bullet prefab
    public Transform firePoint;             // Position from where bullets will be fired
    public float bulletSpeed = 5f;          // Speed of the bullets
    public float fireRate = 2f;             // Rate of fire in bullets per second
    private float nextFireTime = 0f;        // Time for the next shot

    private SpriteRenderer spriteRenderer;  // Reference to the SpriteRenderer
    private Color originalColor;            // Original sprite color 
    public Sprite deathSprite;              // Sprite to display upon death
    public Color hitColor = Color.white;    // Default hit color (actually turns red)
    public float flashDuration = 0.1f;      // Duration of flashing upon taking damage

    public AudioClip shootSound;            // Audio clip for shooting sound
    public AudioClip deathSound;            // Audio clip for death sound
    private AudioSource audioSource;        // Reference to the AudioSource

    private bool isOnScreen = false;        // Indicate if enemy is onscreen
    private bool hasDied = false;           // Indicate whether enemy has died

    void Start()
    {
        // Find and store the SpriteRenderer from a child object named "Sprite"
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;   // Store original sprite color

        audioSource = GetComponent<AudioSource>();  // Get AudioSource component from enemy 

        // Find the ScoreCounter GameObject and ScoreCounter component
        GameObject scoreGO = GameObject.Find("ScoreCounter");   
        scoreCounter = scoreGO.GetComponent<ScoreCounter>();
    }
    void Update()
    {
        if (spriteRenderer.isVisible && !isOnScreen)    // Checks if sprite is currently visible and is not onscreen
        {
            isOnScreen = true;  // Mark enemy as onscreen 
        }

        if (isOnScreen && Time.time >= nextFireTime)    // If enemy is onscreen and current time is past next fire time, begin shooting
        {
            StartCoroutine(ShootBullet());  // Start shooting bullets
            nextFireTime = Time.time + 1f / fireRate;   // Schedule the next fire time
        }
        if (transform.position.x < -10f)    // Check if enemy is offscreen
        {
            Destroy(gameObject);    // Destroy enemy if it is
        }
    }

    IEnumerator ShootBullet()
    {
        for (int i = 0; i < 3; i++)     // For loop fires three bullets in quick succession
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);  // Instantiate a bullet at the fire point
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();  // Get rigidbody component for bullet
            if (bulletRb != null)
            {
                bulletRb.velocity = transform.forward * bulletSpeed;    // Shoot bullet forward
            }
            if (audioSource != null && shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);    // Play the sound effect
            }

            yield return new WaitForSeconds(0.1f);  // Time between shooting bullets
        }
    }

    // Method to handle taking damage
    public void TakeDamage()
    {
        if (health > 0)
        {
            StartCoroutine(Flash());    // Coroutine to flash taking damage
            health -= 1;    // Decrease health by 1
        }

        else if (health <= 0 && !hasDied)   // If enemy health is 0 or less and enemy hasn't died
        {
            hasDied = true;     // Mark enemy as dead
            if (deathSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(deathSound);    // Play the sound effect 
            }
            spriteRenderer.sprite = deathSprite;    // Change sprite upon death
            StartCoroutine(DestroyObject());    // Destroy enemy when health reaches 0
            scoreCounter.score += 500;  // Increase score by 500 upon kill
        }
    }

    private IEnumerator Flash()
    {
        spriteRenderer.color = hitColor;    // Change sprite color upon hit
        yield return new WaitForSeconds(flashDuration);     // Display color for flash duration
        spriteRenderer.color = originalColor;   // Change sprite back to original color
    }
    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(0.2f);  // Wait for the specified delay
        Destroy(gameObject);
    }
}
