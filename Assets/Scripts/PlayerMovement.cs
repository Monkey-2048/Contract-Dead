using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Adjust the speed as needed
    private Rigidbody playerRigidbody;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get input from the player
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;

        if (moveDirection != Vector3.zero)
        {
            // Rotate the player to face the direction of movement
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }

        // Move the player
        Vector3 moveVelocity = moveDirection * moveSpeed;
        playerRigidbody.velocity = moveVelocity;
    }
}
