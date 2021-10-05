using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
	private Camera camera;
	private Rigidbody2D rigidbody2D;

	private void Awake()
	{
		// TODO: pass camera as argument.
		this.camera = Camera.main;
		this.rigidbody2D = this.GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		Vector2 worldPosition = this.rigidbody2D.position;
		Vector2 viewportPosition = this.camera.WorldToViewportPoint(worldPosition);

		if (!IsWithinViewport(viewportPosition))
		{
			// Wrap position around screen edges.
			Vector2 wrappedViewportPosition = Vector2.zero;
			wrappedViewportPosition.x = WrapUnitInterval(viewportPosition.x);
			wrappedViewportPosition.y = WrapUnitInterval(viewportPosition.y);
			Vector2 wrappedWorldPosition = this.camera.ViewportToWorldPoint(wrappedViewportPosition);
			this.rigidbody2D.position = wrappedWorldPosition;
		}
	}

	private bool IsWithinViewport(Vector2 pos)
	{
		return (pos.x >= 0.0f)
			&& (pos.x <= 1.0f)
			&& (pos.y >= 0.0f)
			&& (pos.y <= 1.0f);
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
