using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
	public GameObject asteroidPrefab;
	//private List<Asteroid> asteroids = new List<Asteroid>();
	private List<GameObject> asteroids = new List<GameObject>();

	public void SpawnAsteroid(Vector2 position, Vector2 velocity)
	{
		GameObject asteroidInstance = Instantiate(asteroidPrefab, position, Quaternion.identity);
		Asteroid asteroid = asteroidInstance.GetComponent<Asteroid>();
		asteroid.Initialize(velocity);
		//this.asteroids.Add(asteroid);
		this.asteroids.Add(asteroidInstance);
	}

	/*
	private void HandleCollision()
	{
		Debug.Log("AsteroidManager::HandleCollision, " + Time.time);
	}
	*/

	public void HandleHit(GameObject asteroidInstance)
	{
		Vector2 position = asteroidInstance.transform.position;

		this.asteroids.Remove(asteroidInstance);
		Destroy(asteroidInstance);

		for (var i = 0; i < 2; ++i)
		{

			float angle = Random.value * 2 * Mathf.PI;
			Vector2 direction = Vector2.zero;
			direction.x = Mathf.Cos(angle);
			direction.y = Mathf.Sin(angle);
			float speed = Random.Range(1.0f, 2.0f);
			Vector2 velocity = speed * direction;

			this.SpawnAsteroid(position, velocity);
		}
	}
}
