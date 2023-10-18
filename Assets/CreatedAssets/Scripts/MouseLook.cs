using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 2.0f;
    public bool lockCursor = true; // Lock the cursor to the center of the screen

    private float rotationX = 0;

    private void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Rotate the player's camera (or object) around the X and Y axes
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90, 90); // Limit the vertical rotation

        // Rotate the camera or object
        transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.parent.Rotate(Vector3.up * mouseX);
    }
}
