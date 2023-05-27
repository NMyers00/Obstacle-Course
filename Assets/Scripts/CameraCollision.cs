using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public float minDistance = 1f;
    public float maxDistance = 4f;
    public float smooth = 10f;

    private Vector3 offset;
    private float closestDistance;

    private void Start()
    {
        // Calculate the initial offset from the camera to the player
        offset = transform.position - Camera.main.transform.position;
    }

    private void LateUpdate()
    {
        // Check if the camera has a parent object
        if (transform.parent != null)
        {
            // Calculate the desired camera position based on the player's position and offset
            Vector3 targetPosition = transform.parent.position + offset;
            float distance = Vector3.Distance(targetPosition, transform.position);

            // Adjust the camera position to avoid collisions with objects between the player and camera
            if (Physics.Raycast(targetPosition, -transform.forward, out RaycastHit hit, distance))
            {
                float adjustedDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
                transform.position = Vector3.Lerp(transform.position, targetPosition - transform.forward * adjustedDistance, smooth * Time.deltaTime);

                // Update the closest distance for subsequent camera movements
                closestDistance = adjustedDistance;
            }
            else
            {
                // Check if the camera is closer to the player than the closest distance encountered
                if (distance < closestDistance)
                {
                    transform.position = Vector3.Lerp(transform.position, targetPosition - transform.forward * closestDistance, smooth * Time.deltaTime);
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, targetPosition, smooth * Time.deltaTime);
                }
            }
        }
        else
        {
            // If the camera doesn't have a parent, simply follow its original offset position
            transform.position = Vector3.Lerp(transform.position, Camera.main.transform.position + offset, smooth * Time.deltaTime);
        }
    }
}
