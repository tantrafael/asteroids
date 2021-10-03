using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
	public delegate void CollisionAction(GameObject gameObject, GameObject other);
	public static event CollisionAction OnCollision;

	public void Initialize(Vector2 velocity)
	{
		Rigidbody2D rigidbody2D = this.GetComponent<Rigidbody2D>();
		rigidbody2D.velocity = velocity;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (OnCollision != null)
		{
			OnCollision(this.gameObject, other.gameObject);
		}
	}
}
