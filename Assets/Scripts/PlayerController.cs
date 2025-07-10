using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    private Rigidbody rb;
    private PlayerInputReader inputReader;
    private bool isGrounded;

    private Vector3 movementDir = Vector3.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputReader = GetComponent<PlayerInputReader>();
    }

    private void FixedUpdate()
    {
        Vector2 input = inputReader.MoveInput;
        movementDir = transform.TransformDirection(new Vector3(input.x, 0, input.y));
        rb.MovePosition(rb.position + movementDir * moveSpeed * Time.fixedDeltaTime);

        if (inputReader.JumpPressed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}