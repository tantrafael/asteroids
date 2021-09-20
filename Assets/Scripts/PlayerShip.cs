using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    private float angle = 0.0f;
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
        float angle = -5.0f * horizontalInput;
        this.transform.Rotate(0.0f, 0.0f, angle, Space.Self);

        float verticalInput = Input.GetAxis("Vertical");
        //float thrust = 0.01f * verticalInput;
        //velocity += thrust * this.transform.up;

        /*
        float f = 0.01f * verticalInput;
        float m = 1.0f;
        float a = f / m;
        velocity += a * this.transform.up;
        */

        float mass = 1.0f;
        float dt = Time.fixedDeltaTime;
        Vector3 thrust = 0.5f * verticalInput * this.transform.up;
        Vector3 friction = -0.2f * velocity;
        Vector3 force = thrust + friction;
        Vector3 acceleration = force / mass;
        velocity += acceleration * dt;
        this.transform.position += velocity;
    }
}
