using System.Collections;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
	private int size;
	private float difficulty;
	//private int direction;
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

	//public void Initialize(int size, float difficulty, int direction)
	public void Initialize(int size, float difficulty, Vector2 position, int direction)
	{
		this.size = size;
		this.difficulty = difficulty;
		//this.direction = direction;
		this.body.position = position;
		this.direction = (direction == 1) ? Vector2.right : Vector2.left;
		this.speed = 4.0f * difficulty;
	}

	private void Start()
	{
		//this.body.velocity = speed * this.transform.right;
		//Vector2 direction = (this.direction == 1) ? Vector2.right : Vector2.left;
		this.body.velocity = speed * this.direction;

		this.StartCoroutine(ControlMovement());
	}

	private IEnumerator ControlMovement()
	{
		while (true)
		{
			yield return new WaitForSeconds(3.0f);
			this.UpdateDirection();
			// TODO: Remove magic number.
			//yield return new WaitForSeconds(3.0f);
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
