using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections;

public class lookaround : MonoBehaviour
{
    // Define the minimum and maximum y-rotation angles
    public float minYRotation = 264.363f;
    public float maxYRotation = 90.755f;

    // Define the minimum and maximum intervals between rotations
    public float minInterval = 1.0f;
    public float maxInterval = 5.0f;

    // Speed of rotation transition
    public float rotationSpeed = 2.0f;

    private float targetYRotation;
    private float currentYRotation;

    void Start()
    {
        // Initialize the current rotation
        currentYRotation = transform.rotation.eulerAngles.y;
        StartCoroutine(RandomizeRotation());
    }

    IEnumerator RandomizeRotation()
    {
        while (true)
        {
            // Randomly select the next target y-rotation
            targetYRotation = Random.Range(minYRotation, maxYRotation);

            // Rotate towards the target rotation over time
            while (Mathf.Abs(Mathf.DeltaAngle(currentYRotation, targetYRotation)) > 0.1f)
            {
                currentYRotation = Mathf.LerpAngle(currentYRotation, targetYRotation, Time.deltaTime * rotationSpeed);
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, currentYRotation, transform.rotation.eulerAngles.z);
                yield return null;
            }

            // Wait for a random interval before returning to 180 degrees
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);

            // Rotate back to 180 degrees
            while (Mathf.Abs(Mathf.DeltaAngle(currentYRotation, -90f)) > 0.1f)
            {
                currentYRotation = Mathf.LerpAngle(currentYRotation, -90f, Time.deltaTime * rotationSpeed);
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, currentYRotation, transform.rotation.eulerAngles.z);
                yield return null;
            }

            // Wait for a random interval before starting the next random rotation
            waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);
        }
    }
}

