using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarFiller : MonoBehaviour
{
    public HealthBar healthBar; // Reference to the health bar script

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Print "seen" to the console
            Debug.Log("seen");

            // Increase the health bar fill by 1/30
            healthBar.IncreaseFill(1f / 30f);
        }
    }
}

