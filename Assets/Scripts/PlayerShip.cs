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

	public delegate void CollisionAction(GameObject gameObject, GameObject other);
	public static event CollisionAction OnCollision;

	private RotatingBody rotatingBody;
	private TranslatingBody translatingBody;
	private EventManager eventManager;
	private ShotManager shotManager;

	private void Awake()
	{
		this.rotatingBody = this.gameObject.AddComponent<RotatingBody>();

		this.translatingBody = this.gameObject.AddComponent<TranslatingBody>();
		this.translatingBody.Initialize(mass, drag);

		Collider collider = this.GetComponent<Collider>();

		if (collider)
		{
			//collider.Initialize(ColliderType.PlayerShip);
		}
	}

	//public void Initialize(ShotManager shotManager)
	public void Initialize(EventManager eventManager, ShotManager shotManager)
	{
		this.eventManager = eventManager;
		this.shotManager = shotManager;

		Collider collider = this.GetComponent<Collider>();

		if (collider)
		{
			collider.Initialize(ColliderType.PlayerShip, this.eventManager);
		}
	}

	private void Update()
	{
		this.HandleRotationInput();
		this.HandleThrustInput();
		this.HandleFireInput();
		// Handle hyper space input.
	}

	private void HandleRotationInput()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		float torque = -rotationInputCoefficient * horizontalInput;
		this.rotatingBody.ApplyTorque(torque);
	}

	private void HandleThrustInput()
	{
		float verticalInput = Input.GetAxis("Vertical");

		if (verticalInput >= 0.0f)
		{
			float thrustMagnitude = this.thrustInputCoefficient * verticalInput;
			Vector2 force = thrustMagnitude * this.transform.up;
			this.translatingBody.ApplyForce(force);
		}
	}

	private void HandleFireInput()
	{
		//bool fireButtonInput = Input.GetButton("Fire1");
		bool fireButtonInput = Input.GetKeyDown("space");

		if (fireButtonInput == true)
		{
			this.Shoot();
		}
	}

	public void Shoot()
	{
		Vector2 position = this.transform.position;
		Vector2 velocity = this.shotSpeed * this.transform.up;
		this.shotManager.Shoot(ColliderType.PlayerShot, position, velocity);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (OnCollision != null)
		{
			OnCollision(this.gameObject, other.gameObject);
		}
	}
}
