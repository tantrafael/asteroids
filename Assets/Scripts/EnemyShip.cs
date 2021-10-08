//using System.Threading;
//using System.Threading.Tasks;
using System.Collections;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
	private int size;
	private float difficulty;
	private float speed;
	private Vector2 direction;
	private Rigidbody2D body;
	//private CancellationTokenSource source;

	private void Awake()
	{
		this.body = this.GetComponent<Rigidbody2D>();

		Collider collider = this.GetComponent<Collider>();

		if (collider)
		{
			collider.Initialize(ColliderType.EnemyShip);
		}
	}

	private void Start()
	{
		this.body.velocity = speed * this.direction;

		this.StartCoroutine(this.ControlMovement());

		//this.source = new CancellationTokenSource();
		//this.ControlMovement(this.source.Token);
	}

	private void OnDestroy()
	{
		this.StopAllCoroutines();
		//this.source.Cancel();
	}

	public void Initialize(int size, float difficulty, int direction)
	{
		this.size = size;
		this.difficulty = difficulty;
		this.direction = (direction == 1) ? Vector2.right : Vector2.left;
		this.speed = 4.0f * difficulty;
	}

	private IEnumerator ControlMovement()
	{
		while (true)
		{
			// TODO: Remove magic number.
			yield return new WaitForSeconds(3.0f);
			this.UpdateDirection();
		}
	}

	/*
	private async void ControlMovement(CancellationToken cancellationToken)
	{
		while (true)
		{
			try
			{
				// TODO: Remove magic number.
				await Task.Delay(System.TimeSpan.FromSeconds(3), cancellationToken);
				cancellationToken.ThrowIfCancellationRequested();
				this.UpdateDirection();
			}
			catch (System.OperationCanceledException)
			{
				break;
			}

			await Task.Delay(System.TimeSpan.FromSeconds(3), cancellationToken);
			cancellationToken.ThrowIfCancellationRequested();
			this.UpdateDirection();
		}
	}
	*/

	private void UpdateDirection()
	{
		// TODO: Remove magic numbers.

		float r = Random.value;

		if (r < 0.3333)
		{
			this.direction.y = -1.0f;
		}
		else if (r < 0.6667)
		{
			this.direction.y = 0.0f;
		}
		else
		{
			this.direction.y = 1.0f;
		}

		this.body.velocity = this.speed * this.direction;
	}
}
