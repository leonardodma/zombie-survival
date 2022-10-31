using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Models;

public class playerController : MonoBehaviour
{
    private CharacterController characterController;

    private DefaultInput defaultInput;

    private Vector2 inputMovement;

    [Header("References")]
    public Transform cameraHolder;

    [Header("Settings")]
    public PlayerSettingsModel playerSettings;

    public LayerMask playerMask;

    [Header("Gravity")]
    public float gravity;

    public float gravityMin;

    private float playerGravity;

    [Header("Jump")]
    public Vector3 jumpForce;

    private Vector3 jumpForceVelocity;

    [Header("Sprint")]
    private bool isSprinting;

    private float sprintSpeed = 12f;

    private void Awake()
    {
        // Get the DefaultInput asset
        defaultInput = new DefaultInput();

        // Read the input from the asset, when movement is performed
        defaultInput.Character.Movement.performed += e =>
            inputMovement = e.ReadValue<Vector2>();

        // Call Jump() when the Jump action is performed
        defaultInput.Character.Jump.performed += e => Jump();

        // Call Jump() when the Sprint action is performed
        defaultInput.Character.Sprint.performed += e => ToggleSprint();

        defaultInput.Enable();

        // Get the CharacterController component
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        CalculateMovement();
        //CalculateJump();
    }

    private void CalculateMovement()
    {
        float inputX = inputMovement.x;
        float inputY = inputMovement.y;

        Vector3 move = transform.right * inputX + transform.forward * inputY;
        characterController
            .Move(move * sprintSpeed * Time.deltaTime);
    }

    private void CalculateJump()
    {
        jumpForce =
            Vector3
                .SmoothDamp(jumpForce,
                Vector3.zero,
                ref jumpForceVelocity,
                playerSettings.JumpTime);
    }

    private void Jump()
    {
        if (!characterController.isGrounded) return;

        jumpForce = Vector3.up * playerSettings.JumpHeight;
        playerGravity = 0;
    }

    private void ToggleSprint()
    {
        isSprinting = !isSprinting;
    }
}
