using UnityEngine;

public class LandingState : PlayerBaseState
{
    private float landingTimer;
    private const float LANDING_TIME = 0.1f;

    public LandingState(PlayerController playerController) : base(playerController) { }

    public override void Enter()
    {
        Debug.Log("Entered Landing State");
        landingTimer = 0f;
    }

    public override void Update()
    {
        landingTimer += Time.deltaTime;
        
        // After landing animation time, check what to do next
        if (landingTimer >= LANDING_TIME)
        {
            if (inputReader.MoveInput.magnitude > 0.1f)
            {
                playerController.StateMachine.ChangeState<WalkingState>();
            }
            else
            {
                playerController.StateMachine.ChangeState<IdleState>();
            }
        }
    }

    public override void FixedUpdate()
    {
        // Reduce movement during landing
        Vector2 input = inputReader.MoveInput;
        Vector3 movementDir = playerController.transform.TransformDirection(new Vector3(input.x, 0, input.y));
        rb.MovePosition(rb.position + movementDir * playerController.moveSpeed * 0.3f * Time.fixedDeltaTime);
    }

    public override void Exit()
    {
        Debug.Log("Exited Landing State");
    }
}