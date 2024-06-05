using UnityEngine;
using UnityEngine.UI;

public class GarlicInteraction : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject garlicPrefab;

    public float holdDistance = 1.0f;
    public float maxThrowForce = 10.0f;
    public float throwChargeTime = 2.0f;

    public Slider throwForceSlider; // Reference to the slider in the UI

    private GameObject heldGarlic = null;
    private Rigidbody heldGarlicRigidbody = null;

    private Vector3 throwStartPoint;
    private float throwStartTime;
    private bool isChargingThrow;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryPickUpOrSpawnGarlic();
        }

        if (Input.GetMouseButtonUp(0) && heldGarlic != null)
        {
            DropGarlic();
        }

        if (Input.GetMouseButton(1))
        {
            ChargeThrow();
            throwForceSlider.gameObject.SetActive(true); // Show slider during throw charge
        }
        else
        {
            throwForceSlider.gameObject.SetActive(false); // Hide slider when not charging
        }

        if (Input.GetMouseButtonDown(1))
        {
            StartThrowCharge();
        }

        if (Input.GetMouseButtonUp(1))
        {
            ReleaseThrow();
        }

        if (Input.GetMouseButton(0) && heldGarlic != null)
        {
            MoveHeldGarlic();
        }
    }

    private void TryPickUpOrSpawnGarlic()
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

    private void SpawnGarlic(GameObject ogGarlic)
    {
        Vector3 spawnPosition = ogGarlic.transform.position;
        Quaternion spawnRotation = ogGarlic.transform.rotation;
        GameObject newGarlic = Instantiate(garlicPrefab, spawnPosition, spawnRotation);
        newGarlic.tag = "garlic";
    }

    private void PickUpGarlic(GameObject garlic)
    {
        heldGarlic = garlic;
        heldGarlicRigidbody = heldGarlic.GetComponent<Rigidbody>();
        if (heldGarlicRigidbody != null)
        {
            heldGarlicRigidbody.isKinematic = true;
        }
        Collider garlicCollider = heldGarlic.GetComponent<Collider>();
        if (garlicCollider != null)
        {
            garlicCollider.enabled = false;
        }
    }

    private void MoveHeldGarlic()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Ray ray = playerCamera.ScreenPointToRay(screenCenter);
        Vector3 holdPosition = ray.origin + ray.direction * holdDistance;
        heldGarlic.transform.position = holdPosition;
    }

    private void DropGarlic()
    {
        if (heldGarlicRigidbody != null)
        {
            heldGarlicRigidbody.isKinematic = false;
        }
        Collider garlicCollider = heldGarlic.GetComponent<Collider>();
        if (garlicCollider != null)
        {
            garlicCollider.enabled = true;
        }
        heldGarlic = null;
        heldGarlicRigidbody = null;
    }

    private void StartThrowCharge()
    {
        if (heldGarlic != null)
        {
            throwStartPoint = heldGarlic.transform.position;
            throwStartTime = Time.time;
            isChargingThrow = true;
        }
    }

    private void ChargeThrow()
    {
        if (isChargingThrow)
        {
            float chargeTime = Time.time - throwStartTime;
            float throwForce = Mathf.Clamp(chargeTime / throwChargeTime, 0f, 1f) * maxThrowForce;
            throwForceSlider.value = throwForce / maxThrowForce; // Update slider value
        }
    }

    private void ReleaseThrow()
    {
        if (isChargingThrow && heldGarlic != null)
        {
            Vector3 throwDirection = (playerCamera.transform.forward + playerCamera.transform.up).normalized;
            float chargeTime = Time.time - throwStartTime;
            float throwForce = Mathf.Clamp(chargeTime / throwChargeTime, 0f, 1f) * maxThrowForce;
            heldGarlicRigidbody.isKinematic = false;
            heldGarlicRigidbody.AddForce(throwDirection * throwForce, ForceMode.Impulse);
            throwForceSlider.value = 0f; // Reset slider value after releasing throw
        }

        isChargingThrow = false;

        // Re-enable collider if there's a held garlic
        if (heldGarlic != null)
        {
            Collider garlicCollider = heldGarlic.GetComponent<Collider>();
            if (garlicCollider != null)
            {
                garlicCollider.enabled = true;
            }
        }

        heldGarlic = null;
        heldGarlicRigidbody = null;
    }

}