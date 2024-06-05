using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarFiller : MonoBehaviour
{
    public HealthBar healthBar; // Reference to the health bar script

    private bool playerIsHoldingGarlic; // Flag to track if the player is holding a garlic object

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check if the left mouse button is pressed
        {
            // Update the flag to indicate if the player is holding a garlic object
            playerIsHoldingGarlic = PlayerIsHoldingGarlic();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerIsHoldingGarlic && other.CompareTag("Player")) // Check if the player is holding a garlic object and collision occurs with the player
        {
            // Increase the health bar fill by 1/30
            healthBar.IncreaseFill(1f / 30f);
        }
    }

    // Check if the player is holding a garlic object
    private bool PlayerIsHoldingGarlic()
    {
        // Implement your logic to check if the player is holding a garlic object here
        // For example, you can cast a ray from the camera to detect if it hits a garlic object

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if (hit.collider.CompareTag("garlic"))
            {
                return true; // Player is holding a garlic object
            }
        }

        return false; // Player is not holding a garlic object
    }
}


