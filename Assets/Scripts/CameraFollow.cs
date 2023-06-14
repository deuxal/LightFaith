using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public Vector2 boundsMin;
    public Vector2 boundsMax;

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Apply bounds to the camera's position
        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, boundsMin.x, boundsMax.x);
        smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, boundsMin.y, boundsMax.y);

        transform.position = smoothedPosition;
    }
}

