using UnityEngine;

public class WalkingState : PlayerBaseState
{
    public WalkingState(PlayerController playerController) : base(playerController) { }

    public override void Enter()
    {
        Debug.Log("Entered Walking State");
    }

    public override void Update()
    {
        // Check if player stopped moving
        if (inputReader.MoveInput.magnitude <= 0.1f)
        {
            playerController.StateMachine.ChangeState<IdleState>();
        }
        
        // Check for jump input
        if (inputReader.JumpPressed && playerController.IsGrounded)
        {
            playerController.StateMachine.ChangeState<JumpingState>();
        }
    }

    public override void FixedUpdate()
    {
        // Handle movement
        Vector2 input = inputReader.MoveInput;
        Vector3 movementDir = playerController.transform.TransformDirection(new Vector3(input.x, 0, input.y));
        rb.MovePosition(rb.position + movementDir * playerController.moveSpeed * Time.fixedDeltaTime);
    }

    public override void Exit()
    {
        Debug.Log("Exited Walking State");
    }
}