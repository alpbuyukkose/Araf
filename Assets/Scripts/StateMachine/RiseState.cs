using UnityEngine;

public class RiseState : PlayerBaseState
{
    public RiseState(PlayerStateMachine sm)
    {
        this.stateMachine = sm;
        this.name = "Rise";
    }

    public override void EnterState()
    {
        base.EnterState();

        //stateMachine.animator.SetBool("isJumping", true);
        Debug.Log("Enter Rise State");
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.rb.linearVelocity.y <= -.1f)
        {
            stateMachine.ChangeState(new FallState(stateMachine));
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
        Debug.Log("Exit Rise State");
    }
}
