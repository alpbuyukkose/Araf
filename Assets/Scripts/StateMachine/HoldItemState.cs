using UnityEngine;

public class HoldItemState : PlayerBaseState
{
    private GameObject heldObject;
    private Rigidbody heldRigidbody;

    private float holdDistance = 3f;
    private float holdHeight = .5f;

    private float maxVelocity = 6f;
    private float maxAngularSpeed = 15f;

    private float originalMass;

    private int originalLayer;


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
            rb.isKinematic = false;
            rb.useGravity = false;
            rb.linearDamping = 8f;

            heldRigidbody = rb;
            originalMass = rb.mass;
            rb.mass = originalMass * .5f;

            originalLayer = heldObject.layer;
            heldObject.layer = LayerMask.NameToLayer("HeldObject");
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
            Vector3 target = stateMachine.cameraTransform.position
               + stateMachine.cameraTransform.forward * holdDistance
               + stateMachine.cameraTransform.up * holdHeight;

            Vector3 toTarget = target - heldRigidbody.position;
            float distance = toTarget.magnitude;

            // Eğer hedefe çok yaklaştıysa, sabitle (titremeyi engeller)
            if (distance < 0.01f)
            {
                heldRigidbody.linearVelocity = Vector3.zero;
            }
            else
            {
                float followSpeed = 20f; // burada dengeyi bu ayarlar
                heldRigidbody.linearVelocity = toTarget.normalized * followSpeed * distance;
            }

            if (heldRigidbody.angularVelocity.magnitude > maxAngularSpeed)
            {
                heldRigidbody.angularVelocity = Vector3.ClampMagnitude(heldRigidbody.angularVelocity, maxAngularSpeed);
            }
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("Exit HoldItemState");
    }

    private void DropObject()
    {
        if (heldObject != null && heldObject.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxVelocity);
            rb.angularVelocity = Vector3.ClampMagnitude(rb.angularVelocity, maxVelocity);

            rb.useGravity = true;
            rb.linearDamping = 0f;
            rb.angularDamping = 0f;
            rb.mass = originalMass;

            heldObject.layer = originalLayer;
        }

        heldObject = null;
        heldRigidbody = null;
    }
}
