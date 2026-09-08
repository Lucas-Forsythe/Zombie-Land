using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float runSpeedMultiplier = 1.5f;

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Rigidbody rb;
    private Vector2 moveInput;
    private bool isGrounded;
    public bool isRunning;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //Fixed interval update better for physcial calcs
    private void FixedUpdate()
    {
        MovePlayer();
    }
    
    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
    }

    void OnJump() 
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }

    void CheckGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    void OnWASD(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnRun(InputValue value)
    {
        isRunning = value.isPressed;
    }

    void MovePlayer()
    {
        // Calculate direction relative to player orientation
        Vector3 direction = transform.right * moveInput.x + transform.forward * moveInput.y;

        if (direction.magnitude > 0.1f)
        {
            direction.Normalize();
        }

        float currentSpeed;

        //Changed so camera wont shake when player is not moving but holding run button
        if (isRunning && direction.magnitude > 0.1f)
        {
            currentSpeed = moveSpeed * runSpeedMultiplier;
        }
        else
        {
            // If the player is NOT holding down the Shift key
            currentSpeed = moveSpeed;
        }

        // Apply velocity safely onto the Rigidbody
        rb.linearVelocity = new Vector3(direction.x * currentSpeed, rb.linearVelocity.y, direction.z * currentSpeed);
    }
}