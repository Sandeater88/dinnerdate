using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ouch : MonoBehaviour
{
    public HealthBar healthBar; // Reference to the health bar script

    private void Start()
    {
        // Ensure the object has a collider
        Collider col = GetComponent<Collider>();
        if (col == null)
        {
            Debug.LogError("No collider found on the object. Please add a collider.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name); // Debug statement to verify collision detection

        if (collision.gameObject.CompareTag("garlic")) // Check if the colliding object has the "garlic" tag
        {
            Debug.Log("gotcha"); // Print "gotcha" to the console

            // Increase the health bar fill by 1/5
            healthBar.IncreaseFill(1f / 5f);

            // Optionally destroy the garlic object
            Destroy(collision.gameObject);
        }
        else
        {
            Debug.Log("Collided object is not tagged as garlic.");
        }
    }
}