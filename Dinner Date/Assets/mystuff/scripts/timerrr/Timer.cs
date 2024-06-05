using System.Collections;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to the TextMeshProUGUI component
    private float timerDuration = 60f; // Duration of the timer in seconds
    private float currentTime; // Current time of the timer
    private bool timerRunning; // Flag to indicate if the timer is running
    private GameObject[] taggedObjects; // Array to hold objects with "safe" or "poison" tags

    void Start()
    {
        // Initialize the timer
        currentTime = timerDuration;
        timerText.enabled = false;
    }

    void Update()
    {
        // Check for objects with "safe" or "poison" tags
        taggedObjects = GameObject.FindGameObjectsWithTag("safe");
        if (taggedObjects.Length == 0)
        {
            taggedObjects = GameObject.FindGameObjectsWithTag("poison");
        }

        // If there are no tagged objects, disable the timer
        if (taggedObjects.Length == 0)
        {
            timerRunning = false;
            timerText.enabled = false;
            currentTime = timerDuration; // Reset the timer
        }
        else
        {
            // If tagged objects are present, start the timer
            if (!timerRunning)
            {
                timerRunning = true;
                timerText.enabled = true;
            }

            // Update the timer
            if (timerRunning)
            {
                currentTime -= Time.deltaTime;
                if (currentTime <= 0)
                {
                    // Timer reached 0, reset the timer
                    currentTime = timerDuration;
                }

                // Update the timer text
                timerText.text = Mathf.RoundToInt(currentTime).ToString();
            }
        }
    }
}
