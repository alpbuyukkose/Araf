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
        stateMachine.inputReader.OnCompassToggle += stateMachine.HandleCompassToggle;
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
            return;
        }

        Vector2 input = stateMachine.inputReader.MoveInput;
        if (input.magnitude <= 0.1f)
        {
            //Debug.Log("MoveState: No input, switching to IdleState");
            stateMachine.ChangeState(new IdleState(stateMachine));
            return;
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
        stateMachine.inputReader.OnCompassToggle -= stateMachine.HandleCompassToggle;
        //stateMachine.animator.SetBool("isRunning", false);
        Debug.Log("Exiting Move State");
    }
}
