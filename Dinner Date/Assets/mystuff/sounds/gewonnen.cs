using UnityEngine;

public class Gewonnen : MonoBehaviour
{
    private AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();

        // Check if the AudioSource component exists and an audio clip is assigned
        if (audioSource != null && audioSource.clip != null)
        {
            // Play the audio clip
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is missing. Make sure they are assigned in the Inspector.");
        }
    }
}
