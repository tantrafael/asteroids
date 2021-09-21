using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    //private float drag = 0.1f;
    //private Rigidbody2D rigidBody;

    private void Awake()
    {
        //rigidBody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        /*
        float horizontalInput = Input.GetAxis("Horizontal");
        var torque = -8000000.0f * horizontalInput;
        rigidBody.AddTorque(torque);

        float verticalInput = Input.GetAxis("Vertical");
        var thrust = verticalInput * 5.0f;
        var force = this.transform.up * thrust;
        rigidBody.AddForce(force);
        */
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float rotationAngle = -5.0f * horizontalInput;
        this.transform.Rotate(0.0f, 0.0f, rotationAngle, Space.Self);

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
