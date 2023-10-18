using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float jumpForce = 8f;
    private Vector3 moveDirection;
    private bool isJumping;

    private void Awake()
    {
        if (inputManager == null)
        {
            Debug.LogError("InputManager is not assigned.");
            enabled = false;
            return;
        }

        if (characterController == null)
        {
            Debug.LogError("CharacterController is not assigned.");
            enabled = false;
            return;
        }

        inputManager.OnJump += OnJump;
    }

    private void Update()
    {
        // Get player input for movement
        Vector2 movementInput = inputManager.GetMovementInputsNormalized();

        // Calculate movement direction based on input
        moveDirection = new Vector3(movementInput.x, 0f, movementInput.y);

        // Apply sprint speed if the player is sprinting
        float speed = inputManager.isPlayerSprinting() ? sprintSpeed : moveSpeed;

        // Apply movement
        Vector3 move = transform.TransformDirection(moveDirection) * speed;
        characterController.SimpleMove(move);

        // Handle jumping
        if (isJumping)
        {
            characterController.Move(Vector3.up * jumpForce * Time.deltaTime);
            isJumping = false;
            Debug.Log("abc");
        }
    }

    private void OnJump(object sender, EventArgs e)
    {
        // Set the jump flag to true when the jump event is triggered
        isJumping = true;
        Debug.Log("Jump");
    }
}
