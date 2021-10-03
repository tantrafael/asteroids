using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBody : MonoBehaviour
{
	private Rigidbody2D rigidbody2D;
	private float fixedDeltaTime;
	private float appliedTorque;

	private void Awake()
	{
		this.rigidbody2D = this.GetComponent<Rigidbody2D>();
		this.fixedDeltaTime = Time.fixedDeltaTime;
	}

	public void ApplyTorque(float torque)
	{
		this.appliedTorque = torque;
	}


	private void FixedUpdate()
	{
		this.rigidbody2D.rotation += appliedTorque;
	}
}
