using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslatingMass : MonoBehaviour
{
	public float mass;
	public float drag;
	private float fixedDeltaTime;
	private Vector3 velocity;
	private Vector3 appliedForce;

	private void Awake()
	{
		fixedDeltaTime = Time.fixedDeltaTime;
	}

	public void Initialize(float mass, float drag)
	{
		this.mass = mass;
		this.drag = drag;
	}

	public void ApplyForce(Vector3 force)
	{
		this.appliedForce = force;
	}

	private void FixedUpdate()
	{
		Vector3 dragForce = -this.drag * this.velocity;
		Vector3 netForce = this.appliedForce + dragForce;
		Vector3 acceleration = netForce / this.mass;
		Vector3 deltaVelocity = acceleration * fixedDeltaTime;
		this.velocity += deltaVelocity;
		this.transform.Translate(this.velocity, Space.World);
	}
}
