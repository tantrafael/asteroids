using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
	public void Initialize(Vector2 velocity)
	{
		Rigidbody2D rigidbody2D = this.GetComponent<Rigidbody2D>();
		rigidbody2D.velocity = velocity;
	}
}
