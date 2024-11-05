using System.Collections;
using System.Collections.Generic;
using TMPro;                             
using UnityEngine;
using UnityEngine.SceneManagement;       

public class Game : MonoBehaviour
{
    public int numLives;                 // Number of starting lives
    public TextMeshProUGUI livesText;    // UI element for displaying number of lives
    public GameObject GameOver;          // Reference to GameOver UI element

    public AudioClip deathSound;         // Audio clip for death sound
    private AudioSource audioSource;     // AudioSource component for playing audio

    private int maxLives;                // Variable to store the initial maximum number of lives
    private Player player;               // Reference to Player script

    void Start()
    {
        maxLives = numLives;    // Initialize maxLives with the starting number of lives
        UpdateLivesUI();    // Update the UI to show the initial number of lives
        livesText = GameObject.Find("LivesCounter").GetComponent<TextMeshProUGUI>(); // Get TextMeshPro component for lives counter
        GameOver.SetActive(false);  // Hide game over screen when the game starts

        audioSource = GetComponent<AudioSource>();  // Get the AudioSource component from player

        player = FindObjectOfType<Player>();    // Find the Player script instance in the scene
    }

    public void PlayerHit()
    {
        if (player != null)     // Check if the player exists
        {
            numLives--;     // Decrease the number of lives by 1
            UpdateLivesUI();    // Update the UI to reflect the new number of lives

            if (deathSound != null && audioSource != null)  // Check if the death sound and audio source are available
            {
                audioSource.PlayOneShot(deathSound);    // Play the death sound effect
            }

            if (numLives <= 0)  // Check if the number of lives has reached 0 or below
            {
                StartCoroutine(HandleGameOver());   // Start the coroutine to handle game over logic
            }
        }
    }

    void UpdateLivesUI()
    {
        if (livesText != null)  // Check if the livesText UI element exists
        {
            livesText.text = numLives + "/" + maxLives;     // Update the UI text to show current lives out of max lives
        }
    }

    private IEnumerator HandleGameOver()
    {
        GameOver.SetActive(true);   // Show the Game Over canvas

        if (deathSound != null)     // Check if the death sound is available
        {
            yield return new WaitForSeconds(deathSound.length);     // Wait for the death sound to finish playing
        }
        else
        {
            yield return new WaitForSeconds(1f);    // Wait for a default period if no sound is available
        }

        SceneManager.LoadScene("Stage1");   // Reload the scene
    }
}
