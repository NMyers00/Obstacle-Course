using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 8f;
    public float rotationSpeed = 5f;
    public float jumpForce = 5f;
    public int maxJumps = 2; // Maximum number of jumps allowed
    private CharacterController characterController;
    private Transform cameraTransform;
    private Vector3 cameraOffset;
    private Quaternion initialRotation;
    private int jumpsRemaining; // Number of jumps remaining
    private float verticalVelocity = 0f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        cameraOffset = cameraTransform.position - transform.position;
        initialRotation = transform.rotation;
        jumpsRemaining = maxJumps;
    }

    private void Update()
    {
        // Player Rotation Reset
        transform.rotation = initialRotation;

        // Player Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        characterController.Move(movement * movementSpeed * Time.deltaTime);

        // Player Rotation
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        transform.rotation *= Quaternion.Euler(0f, mouseX, 0f);

        // Camera Rotation
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;
        Quaternion cameraRotation = cameraTransform.rotation * Quaternion.Euler(-mouseY, 0f, 0f);
        cameraTransform.rotation = Quaternion.Euler(cameraRotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        // Jumping
        if (characterController.isGrounded)
        {
            jumpsRemaining = maxJumps; // Reset jumps when grounded
        }

        if (Input.GetButtonDown("Jump") && jumpsRemaining > 0)
        {
            verticalVelocity = jumpForce;
            jumpsRemaining--;
        }

        ApplyGravity();
        characterController.Move(new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        // Camera Follow
        Vector3 targetPosition = transform.position + cameraOffset;
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPosition, Time.deltaTime);
        cameraTransform.LookAt(transform);
    }
}