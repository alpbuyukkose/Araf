using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    [Header("State Machine")]
    public StateMachine StateMachine { get; private set; }
    
    private Rigidbody rb;
    private PlayerInputReader inputReader;
    private bool isGrounded;

    public bool IsGrounded => isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputReader = GetComponent<PlayerInputReader>();
        
        // Initialize StateMachine
        StateMachine = gameObject.AddComponent<StateMachine>();
        
        // Initialize and add all states
        InitializeStates();
    }

    private void Start()
    {
        // Start with Idle state
        StateMachine.ChangeState<IdleState>();
    }

    private void InitializeStates()
    {
        StateMachine.AddState(new IdleState(this));
        StateMachine.AddState(new WalkingState(this));
        StateMachine.AddState(new JumpingState(this));
        StateMachine.AddState(new FallingState(this));
        StateMachine.AddState(new LandingState(this));
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = false;
        }
    }
}