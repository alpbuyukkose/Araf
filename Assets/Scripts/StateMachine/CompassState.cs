using UnityEngine;

public class CompassState : PlayerBaseState
{
    private Transform compassInstance;
    private Vector3 targetLocalPos = new Vector3(0f, 0f, 2f);
    private float moveSpeed = 5f;

    public CompassState(PlayerStateMachine sm)
    {
        this.stateMachine = sm;
        this.name = "Compass";
    }

    public override void EnterState()
    {
        base.EnterState();

        GameObject spawned = GameObject.Instantiate(stateMachine.compassPrefab, stateMachine.cameraTransform);
        compassInstance = spawned.transform;

        compassInstance.localPosition = new Vector3(0f, 0f, -1f);
        compassInstance.localRotation = Quaternion.Euler(-45f, 0f, 0f);

        stateMachine.inputReader.OnCompassToggle += ExitCompassState;
    }

    public override void Update()
    {
        base.Update();

        Vector3 current = compassInstance.localPosition;
        Vector3 target = targetLocalPos;

        Vector3 newPos = Vector3.MoveTowards(current, target, moveSpeed * Time.deltaTime);
        compassInstance.localPosition = newPos;

        Vector2 input = stateMachine.inputReader.MoveInput;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        stateMachine.HandleMovement(stateMachine.playerMoveSpeed / 2);
    }

    public override void ExitState()
    {
        base.ExitState();

        stateMachine.inputReader.OnCompassToggle -= ExitCompassState;

        if (compassInstance != null)
            GameObject.Destroy(compassInstance.gameObject);
    }

    private void ExitCompassState()
    {
        Vector2 input = stateMachine.inputReader.MoveInput;

        if (input.magnitude > 0.1f)
            stateMachine.ChangeState(new MoveState(stateMachine));
        else
            stateMachine.ChangeState(new IdleState(stateMachine));
    }
}
