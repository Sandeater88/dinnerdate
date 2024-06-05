using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garlicspawner : MonoBehaviour
{
    public GameObject garlicPrefab; // Drag your garlic prefab here
    public float spawnHeight = 1f;  // Height above the oggarlic object to spawn the garlic
    public float dragDistance = 1f; // Distance from the camera to maintain while dragging
    private GameObject pickedObject = null; // The currently picked object
    private Vector3 offset; // Offset from the object's position to the mouse position

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Check if we're clicking on an object to pick up or spawn
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("oggarlic"))
                {
                    // Spawn the garlic object above the hit position
                    SpawnGarlic(hit.point);
                }
                else if (hit.collider.CompareTag("garlic"))
                {
                    // Pick up the garlic object
                    pickedObject = hit.collider.gameObject;
                    offset = pickedObject.transform.position - GetMouseWorldPosition(dragDistance);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0) && pickedObject != null)
        {
            // Release the picked object
            pickedObject = null;
        }

        // Move the picked object with the mouse
        if (pickedObject != null)
        {
            pickedObject.transform.position = GetMouseWorldPosition(dragDistance) + offset;
        }
    }

    void SpawnGarlic(Vector3 spawnPosition)
    {
        // Adjust the spawn position to be directly above the hit point
        Vector3 spawnLocation = spawnPosition + Vector3.up * spawnHeight;

        // Instantiate the garlic object at the calculated position
        Instantiate(garlicPrefab, spawnLocation, Quaternion.identity);
    }

    Vector3 GetMouseWorldPosition(float distance)
    {
        // Get the mouse position in the world
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = distance;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
