using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveplayer : MonoBehaviour
{
    public float minYRotation = 264.363f;
    public float maxYRotation = 90.755f;
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    private Camera playerCamera;
    private GameObject pickedObject = null;
    private Vector3 objectOffset;
    private float initialObjectDistance;

    void Start()
    {
        playerCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMouseLook();
        HandleObjectPickUpAndMovement();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        playerBody.Rotate(Vector3.up * mouseX);

        // Clamp y-rotation within the specified range
        float currentYRotation = playerBody.eulerAngles.y;
        currentYRotation = (currentYRotation > 180) ? currentYRotation - 360 : currentYRotation;
        currentYRotation = Mathf.Clamp(currentYRotation, minYRotation, maxYRotation);
        playerBody.rotation = Quaternion.Euler(0, currentYRotation, 0);
    }

    void HandleObjectPickUpAndMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.transform.CompareTag("object"))
                {
                    pickedObject = hit.transform.gameObject;
                    initialObjectDistance = Vector3.Distance(playerCamera.transform.position, pickedObject.transform.position);
                    objectOffset = pickedObject.transform.position - playerCamera.transform.position;
                }
            }
        }

        if (pickedObject != null && Input.GetMouseButton(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            Vector3 pointAlongRay = ray.GetPoint(initialObjectDistance);
            pickedObject.transform.position = pointAlongRay + objectOffset;
        }

        if (Input.GetMouseButtonUp(0))
        {
            pickedObject = null;
        }
    }
}


