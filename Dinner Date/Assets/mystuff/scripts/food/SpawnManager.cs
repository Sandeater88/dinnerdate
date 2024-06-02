using System.Collections;
using System.Collections.Generic;
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
        if (prefabs.Length == 0)
        {
            Debug.LogError("No prefabs assigned in the inspector!");
            return;
        }

        // Load the custom font from the Resources folder
        customFont = Resources.Load<Font>("Fonts/JazzCreateBubble"); // Adjust the path as necessary

        if (customFont == null)
        {
            Debug.LogError("Font not found in Resources folder!");
            return;
        }

        // Spawn a random prefab at the specified location
        SpawnRandomPrefab();

        // Create a UI Text element
        GameObject hoverTextObject = new GameObject("HoverText");
        hoverTextObject.transform.SetParent(canvas.transform);

        hoverText = hoverTextObject.AddComponent<Text>();
        hoverText.font = customFont; // Assign the custom font
        hoverText.fontSize = 24;
        hoverText.color = Color.white;
        hoverText.alignment = TextAnchor.MiddleCenter;
        hoverText.text = "";
    }

    void Update()
    {
        if (currentPrefab != null)
        {
            // Raycast from the center of the screen
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
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
                    hoverText.text = "";
                }
            }
            else
            {
                hoverText.text = "";
            }
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
        SpawnRandomPrefab();
    }

    void PassPrefab()
    {
        Debug.Log("Prefab passed!");
        Destroy(currentPrefab);
        SpawnRandomPrefab();
    }
}

