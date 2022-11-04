using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class viewController : MonoBehaviour
{
    // Input
    private DefaultInput defaultInput;

    private Vector2 inputView;

    [Header("References")]
    public Transform playerBody;

    [Header("Settings")]
    public float ViewSensitivity = 5f;

    private float xRotation = 0f;

    public Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        // Mouse Lock on Screen
        Cursor.lockState = CursorLockMode.Locked;

        // Get the DefaultInput asset
        defaultInput = new DefaultInput();

        // Read the input from the asset, when view is changed
        defaultInput.Character.View.performed += e =>
            inputView = e.ReadValue<Vector2>();

        // Enable the input
        defaultInput.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateView();
    }

    // Calculate the view
    private void CalculateView()
    {
        float mouseX = inputView.x * ViewSensitivity * Time.deltaTime;
        float mouseY = inputView.y * ViewSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void EndShoot()
    {
        playerAnimator.SetBool("isShooting", false);
    }
}
