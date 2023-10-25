using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the player's transform
    public float smoothSpeed = 0.125f; // Smoothing factor for camera movement
    public Vector3 offset; // Camera's position relative to the player

    void LateUpdate()
    {
        if (target == null)
        {
            // If the target (player) is not set, return early.
            return;
        }

        // Calculate the desired position for the camera
        Vector3 desiredPosition = target.position + offset;

        // Use Vector3.Lerp to smoothly interpolate between the current camera position
        // and the desired position based on the smoothSpeed
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Set the camera's position to the smoothed position
        transform.position = smoothedPosition;
    }
}
