using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody rb { get; private set; }
    public PlayerInputReader inputReader { get; private set; }
    public GroundCheck groundCheck { get; private set; }
    //public Animator animator { get; private set; }

    [Header("CurrentState")]
    [HideInInspector] public PlayerBaseState CurrentState { get; private set; }

    [Header("Player Settings")]
    public float playerMoveSpeed = 4f;
    public float playerJumpForce = 3f;

    [Header("Cam Settings")]
    [SerializeField] public Transform cameraTransform;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputReader = GetComponent<PlayerInputReader>();
        groundCheck = GetComponent<GroundCheck>();
        //animator = GetComponentInChildren<Animator>();

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

    public void TryPickup()
    {
        if (!inputReader.InteractionPressed)
            return;

        Vector3 origin = cameraTransform.position;
        Vector3 direction = cameraTransform.forward;
        float radius = 0.5f;
        float maxDistance = 2f;

        // Debug çizim için
        debugRayOrigin = origin;
        debugRayDirection = direction * maxDistance;
        debugRadius = radius;

        if (Physics.SphereCast(origin, radius, direction, out RaycastHit hit, maxDistance))
        {
            if (hit.collider.CompareTag("Pickupable"))
            {
                ChangeState(new HoldItemState(this, hit.collider.gameObject));
            }
        }
    }

    private Vector3 debugRayOrigin;
    private Vector3 debugRayDirection;
    private float debugRadius;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(debugRayOrigin, debugRayOrigin + debugRayDirection);
        Gizmos.DrawWireSphere(debugRayOrigin, debugRadius);
        Gizmos.DrawWireSphere(debugRayOrigin + debugRayDirection, debugRadius);
    }
}
