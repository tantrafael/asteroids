using UnityEngine;

public class Asteroid : MonoBehaviour
{
	private int size;
	private EventManager eventManager;

	public int Size
	{
		get
		{
			return this.size;
		}
	}

	public void Initialize(int size, Vector2 velocity, EventManager eventManager)
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
		this.transform.localScale = 0.5f * this.size * Vector3.one;
	}
}
