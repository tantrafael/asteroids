using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
	private int size;
	private float difficulty;
	private float speed;
	private Vector2 direction;
	private Rigidbody2D body;

	private void Awake()
	{
		this.body = this.GetComponent<Rigidbody2D>();

		Collider collider = this.GetComponent<Collider>();

		if (collider)
		{
			collider.Initialize(ColliderType.EnemyShip);
		}
	}

	public void Initialize(int size, float difficulty, int direction)
	{
		this.size = size;
		this.difficulty = difficulty;
		this.direction = (direction == 1) ? Vector2.right : Vector2.left;
		this.speed = 4.0f * difficulty;
	}

	private void Start()
	{
		this.body.velocity = speed * this.direction;

		this.ControlMovement();
	}

	private async void ControlMovement()
	{
		while (true)
		{
			// TODO: Remove magic number.
			await Task.Delay(System.TimeSpan.FromSeconds(5.0));
			this.UpdateDirection();
		}
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
}
