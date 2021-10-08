using UnityEngine;

public class Asteroid : MonoBehaviour
{
	private int size;

	private void Awake()
	{
		Collider collider = this.GetComponent<Collider>();

		if (collider)
		{
			collider.Initialize(ColliderType.Asteroid);
		}
	}

	public int Size
	{
		get
		{
			return this.size;
		}
	}

	public void Initialize(int size, Vector2 velocity)
	{
		this.size = size;
		Rigidbody2D rigidbody2D = this.GetComponent<Rigidbody2D>();
		rigidbody2D.velocity = velocity;
		// TODO: Remove magic number.
		this.transform.localScale = 0.5f * this.size * Vector3.one;
	}
}
