using UnityEngine;

public class MoveState : PlayerBaseState
{
    public MoveState(PlayerStateMachine sm)
    {
        this.stateMachine = sm;
        this.name = "Move";
    }

    public override void EnterState()
    {
        base.EnterState();

        // animation
        Debug.Log("Entered Move State");
    }

    public override void Update()
    {
        base.Update();

        Vector2 input = stateMachine.inputReader.MoveInput;
        //Debug.Log($"MoveState Update - Input magnitude: {input.magnitude}");

        if (input.magnitude <= 0.1f)
        {
            //Debug.Log("MoveState: No input, switching to IdleState");
            stateMachine.ChangeState(new IdleState(stateMachine));
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        stateMachine.HandleMovement();

        if (stateMachine.inputReader.JumpPressed && stateMachine.groundCheck.isGrounded)
        {
            stateMachine.rb.AddForce(Vector3.up * stateMachine.playerJumpForce, ForceMode.Impulse);
            stateMachine.ChangeState(new RiseState(stateMachine));
        }
    }

    public override void ExitState()
    {
        base.ExitState();

        Debug.Log("Exiting Move State");
    }
}
