using UnityEngine;
using UnityEngine.Assertions;

public class Shot : MonoBehaviour
{
	//public float speed;
	//public float maxDistance;
	//private ColliderType colliderType;
	private float speed;
	private Collider colliderComponent;
	//private Rigidbody2D body;

	public float TraveledDistance { get; private set; }

	private void Awake()
	{
		//this.body = this.GetComponent<Rigidbody2D>();

		/*
		Collider collider = this.GetComponent<Collider>();
		Assert.IsNotNull(collider);

		if (collider)
		{
			collider.Initialize(ColliderType.PlayerShot);
		}
		*/

		this.colliderComponent = this.GetComponent<Collider>();
	}

	/*
	private void Start()
	{
		Rigidbody2D rigidbody2D = this.GetComponent<Rigidbody2D>();
		rigidbody2D.velocity = this.speed * this.transform.up;
	}
	*/

	public void Initialize(ColliderType colliderType, Vector2 velocity)
	{
		//Debug.Log("Shot::Initialize");
		//this.colliderType = colliderType;
		this.colliderComponent.Initialize(colliderType);

		Rigidbody2D body = this.GetComponent<Rigidbody2D>();
		body.velocity = velocity;
		this.speed = velocity.magnitude;
	}

	private void FixedUpdate()
	{
		this.TraveledDistance += this.speed * Time.fixedDeltaTime;

		/*
		if (this.traveledDistance >= maximumDistance)
		{
			Destroy(this.gameObject);
		}
		*/
	}
}
