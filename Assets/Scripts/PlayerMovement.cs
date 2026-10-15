using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public enum SprintMode
    {
        Hold,
        Toggle
    }

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float runSpeedMultiplier = 1.5f;

    [Header("Sprint Settings")]
    public SprintMode sprintMode = SprintMode.Hold;

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Rigidbody rb;
    private Vector2 moveInput;

    private bool isGrounded;

    // Public so other scripts, can check whether the player is running. (Foreshadowing is a narritive device...)
    public bool isRunning;

    // Prevents Toggle mode from toggling repeatedly while the button is being held.
    private bool runButtonHeld = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        sprintMode = Settings.SprintMode;
    }


    // Runs at a fixed interval. Used for Rigidbody physics.
    private void FixedUpdate()
    {
        MovePlayer();
    }


    // Runs once per frame.
    void Update()
    {
        CheckGrounded();
    }


    // Called by the Jump action.
    void OnJump()
    {
        if (isGrounded)
        {
            rb.AddForce(
                new Vector3(0f, jumpForce, 0f),
                ForceMode.Impulse
            );
        }
    }


    // Checks whether the player is touching the ground.
    void CheckGrounded()
    {
        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundDistance,
            groundMask
        );
    }


    // Get Movement input from the WASD keys or the left stick of a controller.
    void OnWASD(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }


    // Called by the Run action.
    void OnRun(InputValue value)
    {
        if (sprintMode == SprintMode.Hold)
        {
            // Hold-to-run
            isRunning = value.isPressed;
        }
        else
        {
            // Toggle-to-run
            if (value.isPressed && !runButtonHeld)
            {
                isRunning = !isRunning;
                runButtonHeld = true;
            }

            // Allow the next press to toggle again
            if (!value.isPressed)
            {
                runButtonHeld = false;
            }
        }
    }


    // Allows a settings menu to change between Hold and Toggle.
    public void SetSprintMode(SprintMode newMode)
    {
        sprintMode = newMode;

        // Reset sprint state when changing modes.
        isRunning = false;
        runButtonHeld = false;
    }


    // Handles player movement.
    void MovePlayer()
    {
        // Convert the input into movement relative to the player's current orientation.
        Vector3 direction =
            transform.right * moveInput.x +
            transform.forward * moveInput.y;


        // Prevent diagonal movement from being faster.
        if (direction.magnitude > 0.1f)
        {
            direction.Normalize();
        }


        float currentSpeed;


        // Only run if:
        // - Sprint is enabled
        // - Player is actually moving
        if (isRunning && direction.magnitude > 0.1f)
        {
            currentSpeed = moveSpeed * runSpeedMultiplier;
        }
        else
        {
            currentSpeed = moveSpeed;
        }


        // Apply movement while keeping the current vertical velocity.
        rb.linearVelocity = new Vector3(
            direction.x * currentSpeed,
            rb.linearVelocity.y,
            direction.z * currentSpeed
        );
    }
}