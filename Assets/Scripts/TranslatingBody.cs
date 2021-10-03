using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslatingBody : MonoBehaviour
{
	public float mass;
	public float drag;

	private Rigidbody2D rigidbody2D;
	private float fixedDeltaTime;
	private Vector2 appliedForce;

	private void Awake()
	{
		this.rigidbody2D = this.GetComponent<Rigidbody2D>();
		this.fixedDeltaTime = Time.fixedDeltaTime;
	}

	public void Initialize(float mass, float drag)
	{
		this.mass = mass;
		this.drag = drag;
	}

	public void SetVelocity(Vector2 velocity)
	{
		this.rigidbody2D.velocity = velocity;
	}

	public void ApplyForce(Vector2 force)
	{
		this.appliedForce = force;
	}

	private void FixedUpdate()
	{
		Vector2 dragForce = -this.drag * this.rigidbody2D.velocity;
		Vector2 netForce = this.appliedForce + dragForce;
		Vector2 acceleration = netForce / this.mass;
		Vector2 deltaVelocity = acceleration * fixedDeltaTime;
		this.rigidbody2D.velocity += deltaVelocity;
	}
}
