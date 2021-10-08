using UnityEngine;
using UnityEngine.Assertions;

public class Shot : MonoBehaviour
{
	private float speed;

	public float TraveledDistance { get; private set; }

	public void Initialize(ColliderType colliderType, Vector2 velocity, EventManager eventManager)
	{
		Collider collider = this.GetComponent<Collider>();
		collider.Initialize(colliderType, eventManager);

		Rigidbody2D body = this.GetComponent<Rigidbody2D>();
		body.velocity = velocity;
		this.speed = velocity.magnitude;
	}

	private void FixedUpdate()
	{
		// Assuming that velocity is constant.
		this.TraveledDistance += this.speed * Time.fixedDeltaTime;
	}
}
