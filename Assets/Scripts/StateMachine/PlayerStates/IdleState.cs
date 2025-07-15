using UnityEngine;

public class IdleState : PlayerBaseState
{
    public IdleState(PlayerController playerController) : base(playerController) { }

    public override void Enter()
    {
        Debug.Log("Entered Idle State");
    }

    public override void Update()
    {
        // Check for movement input
        if (inputReader.MoveInput.magnitude > 0.1f)
        {
            playerController.StateMachine.ChangeState<WalkingState>();
        }
        
        // Check for jump input
        if (inputReader.JumpPressed && playerController.IsGrounded)
        {
            playerController.StateMachine.ChangeState<JumpingState>();
        }
    }

    public override void FixedUpdate()
    {
        // Apply minimal movement (for stopping smoothly)
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
    }

    public override void Exit()
    {
        Debug.Log("Exited Idle State");
    }
}