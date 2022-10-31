using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private CharacterController characterController;

    private DefaultInput defaultInput;

    private Vector2 inputMovement;

    [Header("References")]
    public Transform cameraHolder;

    [Header("Settings")]
    public LayerMask playerMask;

    [Header("Jump")]
    private float gravity = -9.81f;

    private float jumpHeight = 3f;

    Vector3 velocity;

    [Header("Sprint")]
    private bool isSprinting;

    private float speed = 6f;

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
        CalculateJump();
    }

    private void CalculateMovement()
    {
        float inputX = inputMovement.x;
        float inputY = inputMovement.y;

        Vector3 move = transform.right * inputX + transform.forward * inputY;

        if (isSprinting)
        {
            characterController.Move(move * sprintSpeed * Time.deltaTime);
        }
        else
        {
            characterController.Move(move * speed * Time.deltaTime);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void CalculateJump()
    {
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    private void Jump()
    {
        if (characterController.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void ToggleSprint()
    {
        isSprinting = !isSprinting;
    }
}
