using UnityEngine;
using UnityEngine.Assertions;

public class Shot : MonoBehaviour
{
	private float speed;
	//private Collider colliderComponent;

	public float TraveledDistance { get; private set; }

	/*
	private void Awake()
	{
		this.colliderComponent = this.GetComponent<Collider>();
	}
	*/

	//public void Initialize(ColliderType colliderType, Vector2 velocity)
	public void Initialize(ColliderType colliderType, Vector2 velocity, EventManager eventManager)
	{
		//this.colliderComponent.Initialize(colliderType);
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
