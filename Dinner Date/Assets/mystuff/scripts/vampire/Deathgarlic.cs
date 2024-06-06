using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Deathgarlic : MonoBehaviour
{
    public List<GameObject> hearts; // List to hold references to heart GameObjects

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("garlic")) // Check if the colliding object has the "garlic" tag
        {
            Debug.Log("gotcha"); 
            DestroyHeart();
            Destroy(other.gameObject); // Optionally destroy the garlic object as well
        }
    }

    private void DestroyHeart()
    {
        if (hearts.Count > 1) // Check if there are any hearts left
        {
            GameObject heartToRemove = hearts[hearts.Count - 1]; // Get the last heart in the list
            hearts.RemoveAt(hearts.Count - 1); // Remove the heart from the list
            Destroy(heartToRemove); // Destroy the heart GameObject
        }
        else
        {
            Debug.Log("No more hearts left!"); // Optionally log a message if no hearts are left
            SceneManager.LoadScene(3);
        }
    }
}
