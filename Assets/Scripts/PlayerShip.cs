using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
	// Rotation
	public float momentOfInertia;
	public float angularDrag;
	public float rotationInputCoefficient;

	// Translation
	public float mass;
	public float drag;
	public float thrustInputCoefficient;

	private RotatingBody rotatingBody;
	private TranslatingMass translatingMass;

	private void Awake()
	{
		this.rotatingBody = this.gameObject.AddComponent<RotatingBody>();
		this.rotatingBody.Initialize(momentOfInertia, angularDrag);

		this.translatingMass = this.gameObject.AddComponent<TranslatingMass>();
		this.translatingMass.Initialize(mass, drag);

		this.gameObject.AddComponent<ScreenWrapper>();
	}

	private void Update()
	{
		// Handle rotation input.
		float horizontalInput = Input.GetAxis("Horizontal");
		float rotationMagnitude = -rotationInputCoefficient * horizontalInput;
		Vector3 torque = rotationMagnitude * this.transform.forward;
		this.rotatingBody.ApplyTorque(torque);

		// Handle thrust input.
		float verticalInput = Input.GetAxis("Vertical");

		if (verticalInput >= 0.0f)
		{
			float thrustMagnitude = this.thrustInputCoefficient * verticalInput;
			//this.translatingObject.Thrust(thrustMagnitude);
			Vector3 force = thrustMagnitude * this.transform.up;
			this.translatingMass.ApplyForce(force);
		}

		// Handle laser input.
		// Handle hyper space input.
	}
}
