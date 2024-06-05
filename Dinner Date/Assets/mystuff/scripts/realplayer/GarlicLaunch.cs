using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicLaunch : MonoBehaviour
{
    public GameObject objectPrefab;    // Drag your object prefab here
    public float launchForce = 10f;    // Initial launch force
    public float upwardsForce = 2f;    // Upwards force to add to the launch
    public Transform launchPoint;      // Point from where the object is launched
    public LayerMask launchMask;       // Mask for objects that can be launched

    private GameObject launchArrow;    // The arrow object to visualize launch direction
    private Vector3 launchDirection;   // Direction in which the object will be launched
    private bool aiming = false;       // Flag to indicate if the player is aiming

    void Update()
    {
        if (Input.GetMouseButtonDown(1))    // Right mouse button pressed
        {
            if (CanLaunchObject())
            {
                StartAiming();
            }
        }
        else if (Input.GetMouseButtonUp(1) && aiming)   // Right mouse button released
        {
            LaunchObject();
            StopAiming();
        }

        if (aiming)
        {
            UpdateLaunchDirection();
            UpdateLaunchArrow();
        }
    }

    bool CanLaunchObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, launchMask))
        {
            if (hit.collider.CompareTag("garlic"))
            {
                return true;
            }
        }

        return false;
    }

    void StartAiming()
    {
        aiming = true;
        launchArrow = new GameObject("LaunchArrow");
        launchArrow.transform.position = launchPoint.position;
        launchArrow.AddComponent<LineRenderer>();
    }

    void StopAiming()
    {
        aiming = false;
        Destroy(launchArrow);
    }

    void UpdateLaunchDirection()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Vector3.Distance(transform.position, launchPoint.position);
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        launchDirection = (targetPosition - launchPoint.position).normalized;
    }

    void UpdateLaunchArrow()
    {
        LineRenderer lineRenderer = launchArrow.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, launchPoint.position);
        lineRenderer.SetPosition(1, launchPoint.position + launchDirection * 3); // Adjust arrow length
    }

    void LaunchObject()
    {
        // Create the object
        GameObject launchedObject = Instantiate(objectPrefab, launchPoint.position, Quaternion.identity);

        // Apply launch force
        Rigidbody rb = launchedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 finalLaunchDirection = launchDirection * launchForce;
            finalLaunchDirection.y += upwardsForce;
            rb.AddForce(finalLaunchDirection, ForceMode.Impulse);
        }
        else
        {
            Debug.LogError("Object is missing Rigidbody component!");
            Destroy(launchedObject);    // Destroy the object if Rigidbody component is missing
        }
    }
}
