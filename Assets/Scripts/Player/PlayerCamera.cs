using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private float 
    xSens, 
    ySens, // Camera Sensitivity
    defaultFOV,
    transitionSpeed;

    [SerializeField]
    private Transform orientation; // Player transform
    
    [SerializeField]
    private GameObject player;

    private float 
    xRotation, 
    yRotation, // Camera Rotation
    movementSpeed,
    dynamicFOV;

    private Camera cam;
    private PlayerMovement playerMovement;
    private PlayerMovement.MovementState playerState;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor so it cannot exit game window
        Cursor.visible = false; // Hide cursor to replace with crosshair UI element
        cam = GetComponent<Camera>();

        rb = player.GetComponent<Rigidbody>();
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMouse =  Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSens; // Capture Mouse movement
        float yMouse =  Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySens;

        yRotation += xMouse; // Apply mouse values to unity rotation
        xRotation -= yMouse;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Ensure camera cannot surpass 90 degrees (infinite rotation)

        // Apply rotation to camera and orientation
        orientation.rotation = Quaternion.Euler(0, yRotation, 0); // xRotation not needed as character only needs to rotate on yaw axis
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        // Dynamic FOV Control
        playerState = playerMovement.moveState;
        PlayerStateHandler();
    }

    private void PlayerStateHandler()
    {
        switch (playerState)
        {
            case PlayerMovement.MovementState.walking:
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, defaultFOV, Time.deltaTime * transitionSpeed);
                break;

            case PlayerMovement.MovementState.sprinting:
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, defaultFOV + 5, Time.deltaTime * transitionSpeed);
                break;

            case PlayerMovement.MovementState.crouching:
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, defaultFOV - 5, Time.deltaTime * transitionSpeed);
                break;

            case PlayerMovement.MovementState.falling:
                movementSpeed = rb.velocity.magnitude;
                dynamicFOV = Mathf.Lerp(defaultFOV, defaultFOV + 10f, movementSpeed / 9f); // Calculate FOV based off movement speed
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, dynamicFOV, Time.deltaTime * transitionSpeed);
                break;
        }
    }

    public void ChangeSensitivity(int sens)
    {
        xSens = ySens = sens;
    }
}
