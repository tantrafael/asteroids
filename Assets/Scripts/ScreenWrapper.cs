using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
	private Camera mainCamera;
	private Rigidbody2D body;

	private void Awake()
	{
		this.mainCamera = Camera.main;
		this.body = this.GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		if (!this.mainCamera)
		{
			Debug.Log(this.gameObject);
		}

		Vector2 worldPosition = this.body.position;
		Vector2 viewportPosition = this.mainCamera.WorldToViewportPoint(worldPosition);

		if (!this.IsWithinViewport(viewportPosition))
		{
			// Wrap position around screen edges.
			Vector2 wrappedViewportPosition = Vector2.zero;
			wrappedViewportPosition.x = WrapUnitInterval(viewportPosition.x);
			wrappedViewportPosition.y = WrapUnitInterval(viewportPosition.y);
			Vector2 wrappedWorldPosition = this.mainCamera.ViewportToWorldPoint(wrappedViewportPosition);
			this.body.position = wrappedWorldPosition;
		}
	}

	private bool IsWithinViewport(Vector2 position)
	{
		return (position.x >= 0.0f)
			&& (position.x <= 1.0f)
			&& (position.y >= 0.0f)
			&& (position.y <= 1.0f);
	}

	private float WrapUnitInterval(float f)
	{
		if (f >= 1.0f)
		{
			f = 0.0f;
		}
		else if (f <= 0.0f)
		{
			f = 1.0f;
		}

		return f;
	}
}
