using UnityEngine;

public abstract class PlayerBaseState
{
    public string name;
    protected PlayerStateMachine stateMachine;

    public virtual void EnterState() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void ExitState() { }
}
