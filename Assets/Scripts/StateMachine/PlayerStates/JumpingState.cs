using UnityEngine;

public class JumpingState : PlayerBaseState
{
    private float jumpTimer;
    private const float MAX_JUMP_TIME = 0.2f;

    public JumpingState(PlayerController playerController) : base(playerController) { }

    public override void Enter()
    {
        Debug.Log("Entered Jumping State");
        // Apply jump force
        rb.AddForce(Vector3.up * playerController.jumpForce, ForceMode.Impulse);
        jumpTimer = 0f;
    }

    public override void Update()
    {
        jumpTimer += Time.deltaTime;
        
        // Transition to falling after jump time or when falling
        if (jumpTimer >= MAX_JUMP_TIME || rb.velocity.y < 0)
        {
            playerController.StateMachine.ChangeState<FallingState>();
        }
    }

    public override void FixedUpdate()
    {
        // Allow air movement
        Vector2 input = inputReader.MoveInput;
        Vector3 movementDir = playerController.transform.TransformDirection(new Vector3(input.x, 0, input.y));
        rb.MovePosition(rb.position + movementDir * playerController.moveSpeed * 0.7f * Time.fixedDeltaTime);
    }

    public override void Exit()
    {
        Debug.Log("Exited Jumping State");
    }
}