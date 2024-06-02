using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] prefabs; // Array to hold the prefabs to spawn
    public Vector3 spawnLocation; // Location where the prefab will be spawned
    public Canvas canvas; // Canvas to display the hover text

    private GameObject currentPrefab; // The currently spawned prefab
    private Text hoverText; // Text element to display instructions
    private Font customFont; // Reference to the custom font

    void Start()
    {
        // Check if the prefabs array has been set in the inspector
        if (prefabs.Length != 6)
        {
            Debug.LogError("Invalid number of prefabs assigned in the inspector!");
            return;
        }

        // Load the custom font from the Resources folder
        customFont = Resources.Load<Font>("Fonts/JazzCreateBubble"); // Adjust the path as necessary

        if (customFont == null)
        {
            Debug.LogError("Font not found in Resources folder!");
            return;
        }

        // Create a UI Text element
        GameObject hoverTextObject = new GameObject("HoverText");
        hoverTextObject.transform.SetParent(canvas.transform);

        hoverText = hoverTextObject.AddComponent<Text>();
        hoverText.font = customFont; // Assign the custom font
        hoverText.fontSize = 24;
        hoverText.color = Color.white;
        hoverText.alignment = TextAnchor.MiddleCenter;
        hoverText.text = "";

        // Start the coroutine to spawn prefabs
        StartCoroutine(SpawnPrefabsWithDelay());
    }

    IEnumerator SpawnPrefabsWithDelay()
    {
        while (true)
        {
            // Spawn a random prefab at the specified location
            SpawnRandomPrefab();

            // Wait for one minute before spawning the next prefab
            yield return new WaitForSeconds(60f);
        }
    }

    void Update()
    {
        // Check if a prefab is currently active
        if (currentPrefab != null)
        {
            // Raycast from the center of the screen
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the raycast hits the current prefab
                if (hit.transform.gameObject == currentPrefab)
                {
                    // Display the hover text at the center of the screen
                    hoverText.transform.position = new Vector2(Screen.width / 2, Screen.height / 2 - 30);
                    hoverText.text = "Press E to eat, P to pass";

                    // Check for player input
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        EatPrefab();
                    }
                    else if (Input.GetKeyDown(KeyCode.P))
                    {
                        PassPrefab();
                    }
                }
                else
                {
                    // Hide the hover text if the raycast doesn't hit the prefab
                    hoverText.text = "";
                }
            }
        }
        else
        {
            // Hide the hover text if there's no prefab
            hoverText.text = "";
        }
    }

    void SpawnRandomPrefab()
    {
        int randomIndex = Random.Range(0, prefabs.Length);
        currentPrefab = Instantiate(prefabs[randomIndex], spawnLocation, Quaternion.identity);
    }

    void EatPrefab()
    {
        Debug.Log("Prefab eaten!");
        Destroy(currentPrefab);
    }

    void PassPrefab()
    {
        Debug.Log("Prefab passed!");
        Destroy(currentPrefab);
    }
}
