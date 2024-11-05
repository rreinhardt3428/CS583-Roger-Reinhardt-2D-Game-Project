using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 8f;            // Base movement speed
    public int lives = 3;                   // Number of starting lives

    private Game game;                      // Reference to the Game script
    private MeshRenderer meshRenderer;      // Reference to the MeshRenderer component 
    private PlayerRespawn respawn;          // Reference to the PlayerRespawn script

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();    // Get and store the MeshRenderer component attached to the player
        game = FindObjectOfType<Game>();    // Find the Game script instance in the scene and store a reference to it
        respawn = GetComponent<PlayerRespawn>();    // Get and store the PlayerRespawn component attached to the player
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");   // Get input for horizontal movement
        float moveY = Input.GetAxisRaw("Vertical");     // Get input for vertical movement

        Vector3 movement = new Vector3(moveX, moveY);   // Create a Vector3 for movement

        if (Input.GetKey(KeyCode.LeftShift))    // Check if the Left Shift key is held down
        {
            transform.position += movement * moveSpeed / 2 * Time.deltaTime;  // Move at half the base speed
            meshRenderer.enabled = true;    // Ensure the player mesh is visible when focused
        }
        else
        {
            transform.position += movement * moveSpeed * Time.deltaTime;    // Move at the base speed
            meshRenderer.enabled = false;   // Hide the player mesh during normal movement
        }
    }

    public void TakeDamage()    
    {
        lives -= 1;     // Decrease player lives by 1
        game.PlayerHit();   // Notify the Game script that the player has been hit

        if (lives <= 0)  // Check if the player's lives have reached 0 or below
        {
            Destroy(gameObject);    // Destroy the player GameObject when lives are depleted
        }

        StartCoroutine(respawn.Respawn());  // Start the respawn coroutine from the PlayerRespawn script
    }
}
