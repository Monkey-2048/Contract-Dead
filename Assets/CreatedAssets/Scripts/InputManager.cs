using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private GameInputs gameInputs;
    private bool isSprinting;

    public event EventHandler OnJump;

    private void Awake()
    {
        gameInputs = new GameInputs();
        gameInputs.Player.Enable();
        gameInputs.Player.Jump.performed += Jump_performed;

    }

    private void Update()
    {
        if (gameInputs.Player.Sprint.ReadValue<float>() > 0)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJump?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementInputsNormalized()
    {
        Vector2 inputs = gameInputs.Player.Movement.ReadValue<Vector2>();
        inputs = inputs.normalized;

        return inputs;
    }

    public bool isPlayerSprinting()
    {
        return isSprinting;
    }

}
