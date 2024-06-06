using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public Image fillImage; // Reference to the fill image of the health bar
    private float currentFill = 0f; // Current fill amount of the health bar

    void Start()
    {
        // Set the initial fill amount to 0
        fillImage.fillAmount = 0f;
    }

    // Increase the fill amount of the health bar
    public void IncreaseFill(float amount)
    {
        currentFill = Mathf.Clamp01(currentFill + amount);
        fillImage.fillAmount = currentFill;

        // Check if the health bar is full
        if (currentFill >= 1f)
        {
            // Load scene number 2
            SceneManager.LoadScene(2);
        }
    }

 
}
