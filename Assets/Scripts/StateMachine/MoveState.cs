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

        //stateMachine.animator.SetBool("isRunning", true);
        Debug.Log("Entered Move State");
    }

    public override void Update()
    {
        base.Update();

        stateMachine.TryPickup();

        if (stateMachine.rb.linearVelocity.y > 0.1f)
        {
            stateMachine.ChangeState(new RiseState(stateMachine));
        }

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
        stateMachine.TryJump();
    }

    public override void ExitState()
    {
        base.ExitState();
        //stateMachine.animator.SetBool("isRunning", false);
        Debug.Log("Exiting Move State");
    }
}
