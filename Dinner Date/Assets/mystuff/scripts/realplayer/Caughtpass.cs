using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caughtpass : MonoBehaviour
{
    public HealthBar healthBar; // Reference to the health bar script

    private bool isPlayerColliding; // Flag to track if the player is colliding with the object
    private bool isPKeyPressed; // Flag to track if the "P" key is pressed

    void Update()
    {
        if (isPlayerColliding && isPKeyPressed)
        {
            // Increase the health bar fill by 1/15
            healthBar.IncreaseFill(1f / 15f);
            Debug.Log("Caught pass"); // Print "Caught pass" to the console
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerColliding = false;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPKeyPressed = true;
        }
        else if (Input.GetKeyUp(KeyCode.P))
        {
            isPKeyPressed = false;
        }
    }
}