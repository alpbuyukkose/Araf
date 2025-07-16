using UnityEngine;

public class IdleState : PlayerBaseState
{
    public IdleState(PlayerStateMachine sm)
    {
        this.stateMachine = sm;
        this.name = "Idle";
    }

    public override void EnterState()
    {
        base.EnterState();

        // animation
        Debug.Log("Entered Idle State");
    }

    public override void Update()
    {
        base.Update();

        Vector2 input = stateMachine.inputReader.MoveInput;
        //Debug.Log($"IdleState Update - Input magnitude: {input.magnitude}");

        if (input.magnitude > 0.1f)
        {
            //Debug.Log("IdleState: Input detected, switching to MoveState");
            stateMachine.ChangeState(new MoveState(stateMachine));
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (stateMachine.inputReader.JumpPressed && stateMachine.groundCheck.isGrounded)
        {
            stateMachine.rb.AddForce(Vector3.up * stateMachine.playerJumpForce, ForceMode.Impulse);
            stateMachine.ChangeState(new RiseState(stateMachine));
        }
    }

    public override void ExitState()
    {
        base.ExitState();

        Debug.Log("Exiting Idle State");
    }
}
