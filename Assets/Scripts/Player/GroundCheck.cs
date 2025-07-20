using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private float radius = 0.3f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private float coyoteTime = 0.1f;

    private float lastGroundedTime;
    public bool isGrounded => Time.time < lastGroundedTime + coyoteTime;

    void Update()
    {
        bool groundedNow = Physics.CheckSphere(groundCheckPosition.position, radius, groundLayer);
        if (groundedNow)
            lastGroundedTime = Time.time;
    }

    private void OnDrawGizmos()
    {
        if (groundCheckPosition == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheckPosition.position, radius);
    }
}
