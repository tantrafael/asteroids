using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AsteroidSize
{
	Small = 1,
	Medium = 2,
	Large = 3
};

public class AsteroidManager : MonoBehaviour
{
	public GameObject asteroidPrefab;

	private List<GameObject> asteroids = new List<GameObject>();
	private int spawningAsteroidCount = 5;
	private int subAsteroidsCount = 2;

	private EventManager eventManager;

	public void Initialize(EventManager eventManager)
	{
		this.eventManager = eventManager;
	}

	public int GetAsteroidCount()
	{
		return this.asteroids.Count;
	}

	public List<GameObject> GetAsteroids()
	{
		return this.asteroids;
	}

	public void SpawnAsteroids()
	{
		for (var i = 0; i < spawningAsteroidCount; ++i)
		{
			AsteroidSize size = AsteroidSize.Large;

			// TODO: Position asteroids around the screen edges.
			float angle = Random.value * 2 * Mathf.PI;
			Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
			// TODO: Remove magic number.
			Vector2 position = 6.0f * direction;

			this.SpawnAsteroid(size, position);
		}
	}

	public GameObject SpawnAsteroid(AsteroidSize size, Vector2 position)
	{
		float angle = Random.value * 2 * Mathf.PI;
		Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
		// TODO: Remove magic numbers.
		float speed = Random.Range(2.0f, 3.0f) / (int)size;
		Vector2 velocity = speed * direction;

		GameObject asteroidInstance = Instantiate(asteroidPrefab, position, Quaternion.identity);
		Asteroid asteroid = asteroidInstance.GetComponent<Asteroid>();
		asteroid.Initialize(size, velocity, this.eventManager);
		this.asteroids.Add(asteroidInstance);

		return asteroidInstance;
	}

	public void HandleAsteroidHit(GameObject asteroidInstance)
	{
		Asteroid asteroid = asteroidInstance.GetComponent<Asteroid>();
		Vector2 position = asteroidInstance.transform.position;

		this.asteroids.Remove(asteroidInstance);
		Destroy(asteroidInstance);

		// TODO: Remove magic numbers.
		if (asteroid.Size != AsteroidSize.Small)
		{
			AsteroidSize size = asteroid.Size - 1;

			for (var i = 0; i < subAsteroidsCount; ++i)
			{
				this.SpawnAsteroid(size, position);
			}
		}
	}

	public void ClearAsteroids()
	{
		for (var i = this.asteroids.Count - 1; i >= 0; --i)
		{
			GameObject asteroidInstance = this.asteroids[i];
			this.asteroids.RemoveAt(i);
			Object.Destroy(asteroidInstance);
		}
	}

	private void OnDestroy()
	{
		this.ClearAsteroids();
	}
}
