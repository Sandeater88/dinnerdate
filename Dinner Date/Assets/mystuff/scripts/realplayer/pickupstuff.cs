using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupstuff : MonoBehaviour
{
    public float pickupRange = 5f;       // Range within which objects can be picked up
    public float holdDistance = 2f;      // Distance from the player where the object will be held
    public float throwForceMultiplier = 10f; // Multiplier for the throwing force

    private Transform playerCamera;      // Reference to the player's camera
    private GameObject pickedObject = null; // Currently picked up object
    private Rigidbody pickedObjectRb = null; // Rigidbody of the currently picked up object
    private Vector3 previousPosition;    // Previous position of the picked object

    void Start()
    {
        playerCamera = Camera.main.transform;
    }

    void Update()
    {
        // Check for left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            TryPickupObject();
        }

        // Check for left mouse button release
        if (Input.GetMouseButtonUp(0))
        {
            DropObject();
        }

        // If an object is picked up, hold it in front of the player and track its velocity
        if (pickedObject != null)
        {
            HoldObject();
            TrackVelocity();
        }
    }

    void TryPickupObject()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("object"))
            {
                pickedObject = hit.collider.gameObject;
                pickedObjectRb = pickedObject.GetComponent<Rigidbody>();
                if (pickedObjectRb != null)
                {
                    pickedObjectRb.isKinematic = true; // Disable physics while holding
                    previousPosition = pickedObject.transform.position; // Initialize previous position
                }
            }
        }
    }

    void HoldObject()
    {
        Vector3 holdPosition = playerCamera.position + playerCamera.forward * holdDistance;
        pickedObject.transform.position = holdPosition;
        pickedObject.transform.rotation = playerCamera.rotation;
    }

    void TrackVelocity()
    {
        if (pickedObjectRb != null)
        {
            Vector3 currentPosition = pickedObject.transform.position;
            pickedObjectRb.velocity = (currentPosition - previousPosition) / Time.deltaTime;
            previousPosition = currentPosition;
        }
    }

    void DropObject()
    {
        if (pickedObject != null && pickedObjectRb != null)
        {
            pickedObjectRb.isKinematic = false; // Enable physics when dropped
            pickedObjectRb.velocity *= throwForceMultiplier; // Apply throw force multiplier
            pickedObject = null;
            pickedObjectRb = null;
        }
    }
}


