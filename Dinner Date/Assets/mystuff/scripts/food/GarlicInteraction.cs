using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicInteraction : MonoBehaviour
{
    public Camera playerCamera; // Assign the main camera in the Inspector
    public GameObject garlicPrefab; // Assign the garlic prefab in the Inspector

    private GameObject heldGarlic = null;
    private Rigidbody heldGarlicRigidbody = null;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryPickUpOrSpawnGarlic();
        }

        if (Input.GetMouseButton(0) && heldGarlic != null)
        {
            MoveHeldGarlic();
        }

        if (Input.GetMouseButtonUp(0) && heldGarlic != null)
        {
            DropGarlic();
        }
    }

    void TryPickUpOrSpawnGarlic()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("oggarlic"))
            {
                SpawnGarlic(hit.collider.gameObject);
            }
            else if (hit.collider.CompareTag("garlic"))
            {
                PickUpGarlic(hit.collider.gameObject);
            }
        }
    }

    void SpawnGarlic(GameObject ogGarlic)
    {
        Vector3 spawnPosition = ogGarlic.transform.position;
        Quaternion spawnRotation = ogGarlic.transform.rotation;
        GameObject newGarlic = Instantiate(garlicPrefab, spawnPosition, spawnRotation);
        newGarlic.tag = "garlic"; // Ensure the new object is tagged as "garlic"
    }

    void PickUpGarlic(GameObject garlic)
    {
        heldGarlic = garlic;
        heldGarlicRigidbody = heldGarlic.GetComponent<Rigidbody>();
        if (heldGarlicRigidbody != null)
        {
            heldGarlicRigidbody.isKinematic = true; // Disable physics while holding
        }
        Collider garlicCollider = heldGarlic.GetComponent<Collider>();
        if (garlicCollider != null)
        {
            garlicCollider.enabled = false; // Disable collider while holding
        }
    }

    void MoveHeldGarlic()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 holdPosition = ray.origin + ray.direction * 1; // Adjust the distance as needed
        heldGarlic.transform.position = holdPosition;
    }

    void DropGarlic()
    {
        if (heldGarlicRigidbody != null)
        {
            heldGarlicRigidbody.isKinematic = false; // Enable physics when dropping
        }
        Collider garlicCollider = heldGarlic.GetComponent<Collider>();
        if (garlicCollider != null)
        {
            garlicCollider.enabled = true; // Enable collider when dropping
        }
        heldGarlic = null;
        heldGarlicRigidbody = null;
    }
}


