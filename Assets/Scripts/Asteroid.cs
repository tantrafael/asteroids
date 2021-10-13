using UnityEngine;

public class Asteroid : MonoBehaviour
{
	private AsteroidSize size;
	private EventManager eventManager;

	public AsteroidSize Size
	{
		get
		{
			return this.size;
		}
	}

	public void Initialize(AsteroidSize size, Vector2 velocity, EventManager eventManager)
	{
		this.size = size;
		this.eventManager = eventManager;

		CollisionDetector collisionDetector = this.GetComponent<CollisionDetector>();

		if (collisionDetector)
		{
			collisionDetector.Initialize(ColliderType.Asteroid, this.eventManager);
		}

		Rigidbody2D rigidbody2D = this.GetComponent<Rigidbody2D>();
		rigidbody2D.velocity = velocity;
		// TODO: Remove magic number.
		this.transform.localScale = 0.5f * (int)this.size * Vector3.one;
	}
}
