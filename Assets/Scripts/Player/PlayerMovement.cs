using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float 
    speed, 
    drag, 
    jumpForce, 
    jumpCooldown, 
    airMovementSpeed, 
    sprintMultiplierConst,
    crouchMovementSpeed;

    [SerializeField]
    private Transform orientation;

    private float 
    hInput,  // Input values (WASD)
    vInput, 
    clampedSpeed,
    speedModifier,
    charHeight; // Used for crouching functionality

    private Rigidbody rb;
    private Vector3 
    moveDirection, 
    horizontalVelocity; // Interpolates inputs to get final movement direction

    private bool 
    grounded, 
    jumpCheck;

    private RaycastHit groundObj;

    public MovementState moveState;
    public enum MovementState // FSM for handling different types of movement
    {
        walking,
        sprinting,
        falling,
        crouching
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Turn off rotation to remove unexpected turns from collisions
        jumpCheck = true;
        charHeight = transform.localScale.y;
        speedModifier = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        // Input handler
        hInput = Input.GetAxisRaw("Horizontal"); // Get raw input values
        vInput = Input.GetAxisRaw("Vertical");

        StateHandler();

        //Ground Detection
        Vector3[] raycastPositions = { // Multiple rays for edge-cases with capsule collider
            new Vector3(0.48f, 0, 0),
            new Vector3(-0.48f, 0, 0),
            new Vector3(0, 0, 0.48f),
            new Vector3(0, 0, -0.48f)
        };
        bool groundCheck = false;
        foreach (var ray in raycastPositions)
        {
            if (Physics.Raycast(transform.position + ray, Vector3.down, out RaycastHit hit, 1.1f))
            {
                groundCheck = true;
                groundObj = hit; // Store ground data for later use (e.g. slope management)
            }
        }
        grounded = groundCheck; // If any ray hits the ground, groundCheck should be true

        // Ground Movement
        horizontalVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (rb.velocity.magnitude > clampedSpeed)
        {
            horizontalVelocity = horizontalVelocity.normalized * clampedSpeed; // Clamps velocity to prevent accelerating infinitely
            rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, horizontalVelocity.z);
        }

        // Jumping
        if (Input.GetButtonDown("Jump") && moveState != MovementState.falling)
        {
            jumpCheck = false;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Reset Y velocity to preserve jump height consistency
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // Crouching
        if (Input.GetButtonDown("Crouch"))
        {
            transform.localScale = new Vector3(transform.localScale.x, charHeight/2, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse); // Changing character scale will make character float. This forces player to ground.
        }
        if (Input.GetButtonUp("Crouch"))
        {
            transform.localScale = new Vector3(transform.localScale.x, charHeight, transform.localScale.z);
        }
    }

    private void FixedUpdate()
    {
        // Slope Management
        float slopeAngle = Vector3.Angle(Vector3.up, groundObj.normal);
        moveDirection = orientation.forward * vInput + orientation.right * hInput; // Interpolates direction controls for consistent speed
        if (slopeAngle < 60f && slopeAngle != 0)
        {
            moveDirection = Vector3.ProjectOnPlane(moveDirection, groundObj.normal);
        }
        
        rb.AddForce(moveDirection.normalized * speed * speedModifier * 10f, ForceMode.Force); // Applies force to player in direction of moveDirection
    }

    private void ResetJump() // Reset cooldown so player cannot jump multiple times while in isGrounded threshold
    {
        jumpCheck = true;
    }

    private void StateHandler() // Responsible for changing states
    {
        if (!grounded)
        {
            moveState = MovementState.falling;
            rb.drag = 0;
            clampedSpeed = speed;
            speedModifier = airMovementSpeed;
        } 
        else if (Input.GetButton("Crouch"))
        {
            moveState = MovementState.crouching;
            clampedSpeed = speed * 0.75f;
            speedModifier = crouchMovementSpeed;
            rb.drag = 1; // Crouch-sliding movement mechanic
        }
        else if (Input.GetButton("Sprint") && horizontalVelocity.magnitude > 0.5f)
        {
            moveState = MovementState.sprinting;
            rb.drag = drag; // Apply constant drag value to prevent sliding
            clampedSpeed = speed * sprintMultiplierConst; // Apply multiplier if sprint key is held
            speedModifier = sprintMultiplierConst;
        } 
        else
        {
            moveState = MovementState.walking;
            rb.drag = drag; // Apply constant drag value to prevent sliding
            clampedSpeed = speed;
            speedModifier = 1f;
        }
    }
}
