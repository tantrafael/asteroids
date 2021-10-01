using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBody : MonoBehaviour
{
	public float momentOfInertia;
	public float angularDrag;
	private float fixedDeltaTime;
	private Vector3 angularVelocity;
	private Vector3 appliedTorque;

	private void Awake()
	{
		fixedDeltaTime = Time.fixedDeltaTime;
	}

	public void Initialize(float momentOfInertia, float angularDrag)
	{
		this.momentOfInertia = momentOfInertia;
		this.angularDrag = angularDrag;
	}

	public void ApplyTorque(Vector3 torque)
	{
		this.appliedTorque = torque;
	}

	private void FixedUpdate()
	{
		Vector3 angularDragTorque = -this.angularDrag * this.angularVelocity;
		Vector3 netTorque = this.appliedTorque + angularDragTorque;
		Vector3 angularAcceleration = netTorque / this.momentOfInertia;
		Vector3 deltaAngularVelocity = angularAcceleration * fixedDeltaTime;
		this.angularVelocity += deltaAngularVelocity;
		this.transform.Rotate(this.angularVelocity, Space.Self);
	}
}
