using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody rb { get; private set; }
    public PlayerInputReader inputReader { get; private set; }
    public GroundCheck groundCheck { get; private set; }

    [Header("CurrentState")]
    [HideInInspector] public PlayerBaseState CurrentState { get; private set; }

    [Header("Player Settings")]
    public float playerMoveSpeed = 4f;
    public float playerJumpForce = 3f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputReader = GetComponent<PlayerInputReader>();
        groundCheck = GetComponent<GroundCheck>();

        ChangeState(new IdleState(this));
    }

    private void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.Update();
        }
    }

    private void FixedUpdate()
    {
        rb.angularVelocity = Vector3.zero;

        if (CurrentState != null)
        {
            CurrentState.FixedUpdate();
        }
    }

    public void ChangeState(PlayerBaseState newState)
    {
        if (CurrentState != null)
        {
            CurrentState.ExitState();
        }

        CurrentState = newState;

        if (CurrentState != null)
        {
            CurrentState.EnterState();
        }
    }

    public void HandleMovement()
    {
        Vector2 input = inputReader.MoveInput;
        Vector3 movementDir = transform.TransformDirection(new Vector3(input.x, 0, input.y));
        //Debug.Log($"MoveState FixedUpdate - Moving with direction {movementDir}");
        rb.MovePosition(rb.position + movementDir * playerMoveSpeed * Time.fixedDeltaTime);
    }
}
