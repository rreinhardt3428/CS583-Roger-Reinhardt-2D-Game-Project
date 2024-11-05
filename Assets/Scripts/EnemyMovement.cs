using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;               // Speed of the enemy movement along the x-axis
    public float amplitude = 1f;           // Amplitude of the vertical movement (y-axis)
    public float frequency = 1f;           // Frequency of the vertical oscillation

    private float initialXPosition;        // Stores the initial x position of the enemy
    private float time;                     // Timer to track the elapsed time for movement

    void Start()
    {
        initialXPosition = transform.position.x; // Save the initial x position of the enemy
    }

    void Update()
    {
        time += Time.deltaTime;     // Increment time by the time passed since the last frame

        float newXPosition = initialXPosition - speed * time;   // Move enemy left based on speed and elapsed time

        float newYPosition = amplitude * Mathf.Sin(frequency * time);   // Move enemy vertically using a sine wave

        transform.position = new Vector3(newXPosition, newYPosition, transform.position.z);     // Update enemy position to the new calculated x and y coordinates
    }
}
