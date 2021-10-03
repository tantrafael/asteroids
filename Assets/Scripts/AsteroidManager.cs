using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
	public GameObject asteroidPrefab;
	private List<Asteroid> asteroids = new List<Asteroid>();

	public void SpawnAsteroid(Vector2 position, Vector2 velocity)
	{
		GameObject asteroidInstance = Instantiate(asteroidPrefab, position, Quaternion.identity);
		Asteroid asteroid = asteroidInstance.GetComponent<Asteroid>();
		asteroid.Initialize(velocity);
		this.asteroids.Add(asteroid);
	}
}
