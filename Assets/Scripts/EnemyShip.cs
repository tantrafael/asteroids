using System.Collections;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
	public float shotSpeed = 6.0f;

	private EnemyShipSize size;
	private float difficulty;
	private float speed;
	private Vector2 direction;
	private Rigidbody2D body;
	private EventManager eventManager;
	private ShotManager shotManager;

	private void Awake()
	{
		this.body = this.GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		this.body.velocity = speed * this.direction;

		this.StartCoroutine(this.ControlShooting());
		this.StartCoroutine(this.ControlMovement());
	}

	public void Initialize(EnemyShipSize size, float difficulty, int direction, EventManager eventManager, ShotManager shotManager)
	{
		this.size = size;
		this.difficulty = difficulty;
		this.direction = (direction == 1) ? Vector2.right : Vector2.left;
		this.speed = 4.0f * difficulty;
		this.eventManager = eventManager;
		this.shotManager = shotManager;

		CollisionDetector collisionDetector = this.GetComponent<CollisionDetector>();

		if (collisionDetector)
		{
			collisionDetector.Initialize(ColliderType.EnemyShip, this.eventManager);
		}

		this.transform.localScale = (int)this.size * Vector3.one;
	}

	private IEnumerator ControlShooting()
	{
		while (true)
		{
			// TODO: Remove magic number.
			yield return new WaitForSeconds(1.0f);
			this.Shoot();
		}
	}

	private IEnumerator ControlMovement()
	{
		while (true)
		{
			// TODO: Remove magic number.
			yield return new WaitForSeconds(3.0f);
			this.UpdateDirection();
		}
	}

	private void Shoot()
	{
		Vector2 position = this.transform.position;
		float angle = Random.value * 2 * Mathf.PI;
		Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
		Vector2 velocity = this.shotSpeed * direction;
		this.shotManager.Shoot(ColliderType.EnemyShot, position, velocity);
	}

	private void UpdateDirection()
	{
		// TODO: Remove magic numbers.

		float r = Random.value;

		if (r < 0.3333)
		{
			this.direction.y = -1.0f;
		}
		else if (r < 0.6667)
		{
			this.direction.y = 0.0f;
		}
		else
		{
			this.direction.y = 1.0f;
		}

		this.body.velocity = this.speed * this.direction;
	}

	private void OnDestroy()
	{
		this.StopAllCoroutines();
	}
}
