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

	public GameObject laserShotPrefab;

	private RotatingBody rotatingBody;
	private TranslatingBody translatingBody;

	private void Awake()
	{
		this.rotatingBody = this.gameObject.AddComponent<RotatingBody>();

		this.translatingBody = this.gameObject.AddComponent<TranslatingBody>();
		this.translatingBody.Initialize(mass, drag);

		this.gameObject.AddComponent<ScreenWrapper>();
	}

	private void Update()
	{
		// Handle rotation input.
		float horizontalInput = Input.GetAxis("Horizontal");
		float torque = -rotationInputCoefficient * horizontalInput;
		this.rotatingBody.ApplyTorque(torque);

		// Handle thrust input.
		float verticalInput = Input.GetAxis("Vertical");

		if (verticalInput >= 0.0f)
		{
			float thrustMagnitude = this.thrustInputCoefficient * verticalInput;
			Vector2 force = thrustMagnitude * this.transform.up;
			this.translatingBody.ApplyForce(force);
		}

		// Handle laser input.
		bool fireButtonInput = Input.GetButton("Fire1");

		if (fireButtonInput)
		{
			this.Shoot();
		}

		// Handle hyper space input.
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log(collision.gameObject + ", " + Time.time);
	}

	public void Shoot()
	{
		Vector2 position = this.transform.position;
		Quaternion rotation = this.transform.rotation;
		GameObject laserShotInstance = Instantiate(laserShotPrefab, position, rotation);
	}
}
