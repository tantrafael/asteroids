using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
	public delegate void CollisionAction(GameObject gameObject, GameObject other);
	public static event CollisionAction OnCollision;

	private int size;
	private int difficulty;
	private float speed = 1.0f;
	private Rigidbody2D rigidbody2D;

	private void Awake()
	{
		this.rigidbody2D = this.GetComponent<Rigidbody2D>();

		Collider collider = this.gameObject.AddComponent<Collider>();
		collider.Initialize(ColliderType.EnemyShip);
	}

	private void Start()
	{
		this.rigidbody2D.velocity = speed * this.transform.right;
		this.StartCoroutine(ControlMovement());
	}

	public void Initialize(int difficulty)
	{
		this.difficulty = difficulty;
	}

	private IEnumerator ControlMovement()
	{
		while (true)
		{
			//yield return new WaitForSeconds(3.0f);
			this.UpdateDirection();
			// TODO: Remove magic number.
			yield return new WaitForSeconds(3.0f);
		}
	}

	private void UpdateDirection()
	{
		// TODO: Remove magic numbers.

		Vector2 direction = Vector2.zero;
		direction.x = 1.0f;

		float r = Random.value;

		if (r < 0.3333)
		{
			direction.y = -1.0f;
		}
		else if (r < 0.6667)
		{
			direction.y = 0.0f;
		}
		else
		{
			direction.y = 1.0f;
		}

		this.rigidbody2D.velocity = speed * direction;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (OnCollision != null)
		{
			OnCollision(this.gameObject, other.gameObject);
		}
	}
}
