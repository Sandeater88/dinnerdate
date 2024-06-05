using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarFiller : MonoBehaviour
{
    public HealthBar healthBar; // Reference to the health bar script

    private bool playerIsHoldingGarlic; // Flag to track if the player is holding a garlic object
    private GameObject heldGarlic; // Reference to the held garlic object

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check if the left mouse button is pressed
        {
            // Update the flag and reference to indicate if the player is holding a garlic object
            playerIsHoldingGarlic = PlayerIsHoldingGarlic(out heldGarlic);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerIsHoldingGarlic && other.CompareTag("Player")) // Check if the player is holding a garlic object and collision occurs with the player
        {
            // Increase the health bar fill by 1/15
            healthBar.IncreaseFill(1f / 15f);

            // Destroy the garlic object that is currently being held
            if (heldGarlic != null)
            {
                Destroy(heldGarlic);
                heldGarlic = null; // Clear the reference after destroying
            }
        }
    }

    // Check if the player is holding a garlic object
    private bool PlayerIsHoldingGarlic(out GameObject garlicObject)
    {
        // Implement your logic to check if the player is holding a garlic object here
        // For example, you can cast a ray from the camera to detect if it hits a garlic object

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if (hit.collider.CompareTag("garlic"))
            {
                garlicObject = hit.collider.gameObject;
                return true; // Player is holding a garlic object
            }
        }

        garlicObject = null; // No garlic object is being held
        return false; // Player is not holding a garlic object
    }
}
