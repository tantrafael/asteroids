using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine;

public class ShotManager : MonoBehaviour
{
	public GameObject shotPrefab;
	public float speed;
	public float maxDistance;

	private List<GameObject> shots;

	private EventManager eventManager;

	public void Initialize(EventManager eventManager)
	{
		this.eventManager = eventManager;
	}

	private void Awake()
	{
		this.shots = new List<GameObject>();
	}

	public void Shoot(ColliderType colliderType, Vector2 position, Vector2 velocity)
	{
		Assert.IsNotNull(this.shotPrefab);

		Quaternion rotation = Quaternion.LookRotation(Vector3.forward, velocity);
		GameObject shotInstance = Instantiate(this.shotPrefab, position, rotation);
		Shot shot = shotInstance.GetComponent<Shot>();
		shot.Initialize(colliderType, velocity, this.eventManager);
		this.shots.Add(shotInstance);
	}

	// TODO: Consider handling this in a coroutine.
	private void FixedUpdate()
	{
		// TODO: Consider perfomance.
		List<GameObject> shotsOutOfScope = new List<GameObject>();

		foreach (GameObject shotInstance in this.shots)
		{
			Shot shot = shotInstance.GetComponent<Shot>();

			if (shot.TraveledDistance > this.maxDistance)
			{
				shotsOutOfScope.Add(shotInstance);
			}
		}

		foreach (GameObject shotInstance in shotsOutOfScope)
		{
			this.DeleteShot(shotInstance);
		}
	}

	public void DeleteShot(GameObject shotInstance)
	{
		this.shots.Remove(shotInstance);
		Destroy(shotInstance);
	}
}
