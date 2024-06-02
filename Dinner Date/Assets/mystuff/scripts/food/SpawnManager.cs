using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] normalPrefabs; // Array to hold the normal prefabs to spawn
    public GameObject[] poisonousPrefabs; // Array to hold the poisonous prefabs to spawn
    public Vector3 spawnLocation; // Location where the prefab will be spawned
    public Canvas canvas; // Canvas to display the hover text

    private GameObject currentPrefab; // The currently spawned prefab
    private Text hoverText; // Text element to display instructions
    private Font customFont; // Reference to the custom font

    public List<GameObject> hearts; // List to hold references to heart GameObjects

    void Start()
    {
        // Check if the prefabs arrays have been set in the inspector
        if (normalPrefabs.Length == 0 || poisonousPrefabs.Length == 0)
        {
            Debug.LogError("Normal or poisonous prefabs arrays not assigned in the inspector!");
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
            // Decide whether to spawn a normal or a poisonous prefab
            bool spawnPoisonous = Random.Range(0f, 1f) < 0.5f;

            if (spawnPoisonous)
                SpawnPoisonousPrefab();
            else
                SpawnNormalPrefab();

            // Wait for one minute before spawning the next prefab
            yield return new WaitForSeconds(60f);
        }
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
                        if (IsPoisonousPrefab(currentPrefab))
                            EatPoisonousPrefab();
                        else
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
            else
            {
                // Hide the hover text if the raycast doesn't hit anything
                hoverText.text = "";
            }
        }
        else
        {
            // Hide the hover text if there's no prefab
            hoverText.text = "";
        }
    }

    void SpawnNormalPrefab()
    {
        int randomIndex = Random.Range(0, normalPrefabs.Length);
        currentPrefab = Instantiate(normalPrefabs[randomIndex], spawnLocation, Quaternion.identity);
        currentPrefab.tag = "safe"; // Ensure normal prefabs have this tag
    }

    void SpawnPoisonousPrefab()
    {
        int randomIndex = Random.Range(0, poisonousPrefabs.Length);
        currentPrefab = Instantiate(poisonousPrefabs[randomIndex], spawnLocation, Quaternion.identity);
        currentPrefab.tag = "poison"; // Ensure poisonous prefabs have this tag
    }

    void EatPrefab()
    {
        Debug.Log("Normal prefab eaten!");
        Destroy(currentPrefab);
    }

    void EatPoisonousPrefab()
    {
        Debug.Log("Poisonous prefab eaten!");
        Destroy(currentPrefab);

        // Remove one heart if available
        if (hearts.Count > 0)
        {
            GameObject heartToRemove = hearts[hearts.Count - 1];
            hearts.Remove(heartToRemove);
            Destroy(heartToRemove);
        }
        else
        {
            Debug.Log("Game Over - No hearts remaining");
            // You can add game over logic here
        }
    }

    void PassPrefab()
    {
        Debug.Log("Prefab passed!");
        Destroy(currentPrefab);
    }

    bool IsPoisonousPrefab(GameObject prefab)
    {
        return prefab.CompareTag("poison");
    }
}