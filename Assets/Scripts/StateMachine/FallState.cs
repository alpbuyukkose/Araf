using UnityEngine;

public class FallState : PlayerBaseState
{
    public FallState(PlayerStateMachine sm)
    {
        this.stateMachine = sm;
        this.name = "Fall";
    }

    public override void EnterState()
    {
        base.EnterState();

        Debug.Log("Enter Fall State");
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.groundCheck.isGrounded)
        {
            Vector2 input = stateMachine.inputReader.MoveInput;
            if (input.magnitude > 0.1f)
                stateMachine.ChangeState(new MoveState(stateMachine));
            else
                stateMachine.ChangeState(new IdleState(stateMachine));
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        stateMachine.HandleMovement();
    }

    public override void ExitState()
    {
        base.ExitState();

        Debug.Log("Exit Fall State");
    }
}
