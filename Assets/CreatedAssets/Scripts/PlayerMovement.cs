using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float sprintMultiplier = 1.5f;
    public float jumpForce = 8.0f;

    private Rigidbody rb;
    [SerializeField]private InputManager inputManager;
    private bool isGrounded = true;
    private bool canJump = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        inputManager.OnJump += HandleJump;
    }

    private void Update()
    {
        HandleMovementInput();

        // Apply sprinting if the player is sprinting
        

        // Check if the player can jump
        if (isGrounded)
        {
            canJump = true;
        }

        
    }

    private void HandleMovementInput()
    {
        Vector2 movementInput = inputManager.GetMovementInputsNormalized();
        Vector3 moveDirection = new Vector3(movementInput.x, 0, movementInput.y);
        float currentMoveSpeed = inputManager.isPlayerSprinting() ? moveSpeed * sprintMultiplier : moveSpeed;
        // Translate the input into world space and apply movement
        Vector3 movement = transform.TransformDirection(moveDirection) * currentMoveSpeed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Change "Ground" to your ground tag
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Change "Ground" to your ground tag
        {
            isGrounded = false;
        }
    }

    private void HandleJump(object sender, EventArgs e)
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnDestroy()
    {
        inputManager.OnJump -= HandleJump; // Unsubscribe from the jump event to avoid memory leaks.
    }
}
