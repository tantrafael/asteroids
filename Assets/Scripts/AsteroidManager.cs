using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
	public GameObject asteroidPrefab;
	private List<GameObject> asteroids = new List<GameObject>();
	private int nrSpawningAsteroids = 5;
	private int maxSize = 3;
	private int nrSubAsteroids = 2;

	public void SpawnAsteroids()
	{
		for (var i = 0; i < nrSpawningAsteroids; ++i)
		{
			float angle;
			Vector2 direction = Vector2.zero;
			int size = maxSize;

			angle = Random.value * 2 * Mathf.PI;
			direction.x = Mathf.Cos(angle);
			direction.y = Mathf.Sin(angle);
			// TODO: Remove magic number.
			Vector2 position = 6.0f * direction;

			this.SpawnAsteroid(size, position);
		}
	}

	public void SpawnAsteroid(int size, Vector2 position)
	{
		float angle = Random.value * 2 * Mathf.PI;
		Vector2 direction = Vector2.zero;
		direction.x = Mathf.Cos(angle);
		direction.y = Mathf.Sin(angle);
		// TODO: Remove magic numbers.
		float speed = Random.Range(2.0f, 3.0f) / size;
		Vector2 velocity = speed * direction;

		GameObject asteroidInstance = Instantiate(asteroidPrefab, position, Quaternion.identity);
		Asteroid asteroid = asteroidInstance.GetComponent<Asteroid>();
		asteroid.Initialize(size, velocity);
		this.asteroids.Add(asteroidInstance);
	}

	public void HandleHit(GameObject asteroidInstance)
	{
		Asteroid asteroid = asteroidInstance.GetComponent<Asteroid>();
		Vector2 position = asteroidInstance.transform.position;

		this.asteroids.Remove(asteroidInstance);
		Destroy(asteroidInstance);

		if (asteroid.Size > 1)
		{
			int size = asteroid.Size - 1;

			for (var i = 0; i < nrSubAsteroids; ++i)
			{
				this.SpawnAsteroid(size, position);
			}
		}
	}
}
