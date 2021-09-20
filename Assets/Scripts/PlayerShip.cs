using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        var torque = -8000000.0f * horizontalInput;
        rigidBody.AddTorque(torque);

        float verticalInput = Input.GetAxis("Vertical");
        var thrust = verticalInput * 5.0f;
        var force = this.transform.up * thrust;
        rigidBody.AddForce(force);
    }
}
