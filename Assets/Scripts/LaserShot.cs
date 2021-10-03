using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShot : MonoBehaviour
{
	public float speed;
	public float maximumDistance;
	private float traveledDistance;

	private void Start()
	{
		Rigidbody2D rigidbody2D = this.GetComponent<Rigidbody2D>();
		rigidbody2D.velocity = this.speed * this.transform.up;
	}

	private void FixedUpdate()
	{
		this.traveledDistance += this.speed * Time.fixedDeltaTime;

		if (this.traveledDistance >= maximumDistance)
		{
			Destroy(this.gameObject);
		}
	}
}
