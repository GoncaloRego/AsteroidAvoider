using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forceMagnitude;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float rotationSpeed;
    private Camera mainCamera;
    private Rigidbody2D playerRigidbody;

    private Vector2 touchPosition;
    private Vector2 worldPosition;

    private Vector2 movementDirection;
    private Transform cachedTransform;

    void Start()
    {
        cachedTransform = transform;
        mainCamera = Camera.main;
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ProcessInput();
        KeepPlayerOnScreen();
        RotatePlayer();
    }

    private void FixedUpdate()
    {
        if (movementDirection == Vector2.zero)
        {
            return;
        }

        playerRigidbody.AddForce(movementDirection * forceMagnitude, ForceMode2D.Force);
        playerRigidbody.velocity = Vector2.ClampMagnitude(playerRigidbody.velocity, maxVelocity);
    }

    private void ProcessInput()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

            movementDirection = (Vector2)cachedTransform.position - worldPosition;
            movementDirection.Normalize();
        }
        else
        {
            playerRigidbody.velocity = Vector2.zero;
        }
    }

    private void KeepPlayerOnScreen()
    {
        Vector2 newPosition = cachedTransform.position;

        Vector2 viewportPosition = mainCamera.WorldToViewportPoint(cachedTransform.position);

        if (viewportPosition.x > 1)
        {
            newPosition.x = -newPosition.x + 0.1f;
        }
        else if (viewportPosition.x < 0)
        {
            newPosition.x = -newPosition.x - 0.1f;
        }

        if (viewportPosition.y > 1)
        {
            newPosition.y = -newPosition.y + 0.1f;
        }
        else if (viewportPosition.y < 0)
        {
            newPosition.y = -newPosition.y - 0.1f;
        }

        cachedTransform.position = newPosition;
    }

    private void RotatePlayer()
    {
        if (playerRigidbody.velocity == Vector2.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
        cachedTransform.rotation = Quaternion.Slerp(cachedTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
