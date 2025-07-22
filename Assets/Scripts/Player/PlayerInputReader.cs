using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputReader : MonoBehaviour
{
    private InputActions inputActions;

    public Vector2 MoveInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool InteractionPressed { get; private set; }
    public event System.Action OnCompassToggle;

    private void Start()
    {
        inputActions = new InputActions();

        inputActions.OnFoot.Movement.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        inputActions.OnFoot.Movement.canceled += ctx => MoveInput = Vector2.zero;

        inputActions.OnFoot.Jump.performed += ctx => JumpPressed = true;
        inputActions.OnFoot.Jump.canceled += ctx => JumpPressed = false;

        inputActions.OnFoot.Interaction.performed += ctx => InteractionPressed = true;
        inputActions.OnFoot.Interaction.canceled += ctx => InteractionPressed = false;

        inputActions.OnFoot.CompassToggle.performed += ctx => OnCompassToggle?.Invoke();

        inputActions.OnFoot.Enable();
    }

    void Update()
    {
        if (JumpPressed)
            Debug.Log("Jump pressed!");

        if (InteractionPressed)
            Debug.Log("Interact!");
    }

    private void OnDestroy()
    {
        inputActions.OnFoot.Disable();
    }
}