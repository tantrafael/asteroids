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

	public GameObject shotPrefab;
	public float shotSpeed = 6.0f;

	private Rigidbody2D body;
	private Rotator rotator;
	private EventManager eventManager;
	private ShotManager shotManager;

	private void Awake()
	{
		this.body = this.GetComponent<Rigidbody2D>();
		this.rotator = this.GetComponent<Rotator>();
	}

	public void Initialize(EventManager eventManager, ShotManager shotManager)
	{
		this.eventManager = eventManager;
		this.shotManager = shotManager;

		CollisionDetector collisionDetector = this.GetComponent<CollisionDetector>();

		if (collisionDetector)
		{
			collisionDetector.Initialize(ColliderType.PlayerShip, this.eventManager);
		}
	}

	private void Update()
	{
		this.HandleRotationInput();
		this.HandleThrustInput();
		this.HandleFireInput();
		// TODO: Handle hyper space input.
	}

	private void HandleRotationInput()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		float torque = -rotationInputCoefficient * horizontalInput;
		this.rotator.ApplyTorque(torque);
	}

	private void HandleThrustInput()
	{
		float verticalInput = Input.GetAxis("Vertical");

		if (verticalInput >= 0.0f)
		{
			float thrustMagnitude = this.thrustInputCoefficient * verticalInput;
			Vector2 force = thrustMagnitude * this.transform.up;
			this.body.AddForce(force);
		}
	}

	private void HandleFireInput()
	{
		bool fireButtonInput = Input.GetKeyDown("space");

		if (fireButtonInput == true)
		{
			this.Shoot();
		}
	}

	public GameObject Shoot()
	{
		Vector2 position = this.transform.position;
		Vector2 velocity = this.shotSpeed * this.transform.up;
		GameObject shotInstance = this.shotManager.Shoot(ColliderType.PlayerShot, position, velocity);

		return shotInstance;
	}
}
