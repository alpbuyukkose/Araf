using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private float radius = .3f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheckPosition;

    public bool isGrounded { get; private set; }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheckPosition.position, radius, groundLayer);
    }

    private void OnDrawGizmos()
    {
        if (groundCheckPosition == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheckPosition.position, radius);
    }
}
