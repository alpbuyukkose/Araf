using UnityEngine;

public class FallingState : PlayerBaseState
{
    public FallingState(PlayerController playerController) : base(playerController) { }

    public override void Enter()
    {
        Debug.Log("Entered Falling State");
    }

    public override void Update()
    {
        // Check if player landed
        if (playerController.IsGrounded && rb.velocity.y <= 0.1f)
        {
            playerController.StateMachine.ChangeState<LandingState>();
        }
    }

    public override void FixedUpdate()
    {
        // Allow air movement but reduced
        Vector2 input = inputReader.MoveInput;
        Vector3 movementDir = playerController.transform.TransformDirection(new Vector3(input.x, 0, input.y));
        rb.MovePosition(rb.position + movementDir * playerController.moveSpeed * 0.5f * Time.fixedDeltaTime);
    }

    public override void Exit()
    {
        Debug.Log("Exited Falling State");
    }
}