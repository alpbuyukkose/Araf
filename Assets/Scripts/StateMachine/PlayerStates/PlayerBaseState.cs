using UnityEngine;

public abstract class PlayerBaseState : IState
{
    protected PlayerController playerController;
    protected Rigidbody rb;
    protected PlayerInputReader inputReader;

    public PlayerBaseState(PlayerController playerController)
    {
        this.playerController = playerController;
        this.rb = playerController.GetComponent<Rigidbody>();
        this.inputReader = playerController.GetComponent<PlayerInputReader>();
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void Exit() { }
}