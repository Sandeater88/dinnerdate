using UnityEngine;

public class Poisoneddonut : MonoBehaviour
{
    public AudioClip destructionSound; // Assign the audio clip in the Inspector

    private AudioSource audioSource;

    void Start()
    {
        // Get AudioSource component from the same GameObject
        audioSource = GetComponent<AudioSource>();
    }

    void OnDestroy()
    {
        // Play audio clip if assigned
        if (destructionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(destructionSound);
        }
    }
}
