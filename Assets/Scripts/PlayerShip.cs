using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
    }

    private void Update()
    {
        // Handle laser input.
        // Handle hyper space input.
    }

    private void FixedUpdate()
    {
        // Handle rotation input.
        float horizontalInput = Input.GetAxis("Horizontal");
        float rotationAngle = -5.0f * horizontalInput;
        this.transform.Rotate(0.0f, 0.0f, rotationAngle, Space.Self);

        // Handle thrust input.
        float verticalInput = Input.GetAxis("Vertical");
        float mass = 1.0f;
        float deltaTime = Time.fixedDeltaTime;
        Vector3 thrust = 0.5f * verticalInput * this.transform.up;
        Vector3 friction = -0.2f * velocity;
        Vector3 force = thrust + friction;
        Vector3 acceleration = force / mass;
        Vector3 deltaVelocity = acceleration * deltaTime;
        velocity += deltaVelocity;
        this.transform.position += velocity;
    }
}
