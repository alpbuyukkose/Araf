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

        //stateMachine.animator.SetBool("isJumping", true);
        Debug.Log("Enter Fall State");
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.groundCheck.isGrounded)
        {
            Vector3 temp = stateMachine.rb.linearVelocity;
            temp.y = 0f;
            stateMachine.rb.linearVelocity = temp;

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

        //stateMachine.animator.SetBool("isJumping", false);
        Debug.Log("Exit Fall State");
    }
}
