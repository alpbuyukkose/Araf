using UnityEngine;

public class HoldItemState : PlayerBaseState
{
    private GameObject heldObject;
    private float holdDistance = 2f;
    private float holdHeight = 1f;

    private Rigidbody heldRigidbody;

    public HoldItemState(PlayerStateMachine sm, GameObject obj)
    {
        this.stateMachine = sm;
        this.name = "HoldItem";
        this.heldObject = obj;
    }

    public override void EnterState()
    {
        base.EnterState();

        if (heldObject.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.isKinematic = true;
            heldRigidbody = rb;
        }

        Debug.Log("Entered HoldItemState");
    }

    public override void Update()
    {
        base.Update();

        // Eğer E bırakıldıysa objeyi bırak
        if (!stateMachine.inputReader.InteractionPressed)
        {
            DropObject();
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

        if (heldObject != null && heldRigidbody != null)
        {
            Vector3 holdPosition = stateMachine.transform.position
                                 + stateMachine.cameraTransform.forward * holdDistance
                                 + Vector3.up * holdHeight;

            heldRigidbody.MovePosition(holdPosition);
        }
    }

    private void DropObject()
    {
        if (heldObject != null && heldObject.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.isKinematic = false;
        }

        heldObject = null;
        heldRigidbody = null;
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("Exit HoldItemState");
    }
}
