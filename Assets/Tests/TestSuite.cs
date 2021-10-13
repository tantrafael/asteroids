using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestSuite
{
	private GameManager gameManager;

	[SetUp]
	public void Setup()
	{
		GameObject gameManagerPrefab = Resources.Load<GameObject>("Prefabs/GameManager");
		GameObject gameManagerInstance = MonoBehaviour.Instantiate(gameManagerPrefab);
		gameManager = gameManagerInstance.GetComponent<GameManager>();
	}

	[TearDown]
	public void Teardown()
	{
		Object.Destroy(this.gameManager.gameObject);
	}

	[UnityTest]
	public IEnumerator Player_ship_exists_when_the_game_starts()
	{
		// Arrange
		this.gameManager.StartGame();

		// Act
		PlayerShip playerShip = this.gameManager.GetPlayerShip();

		// Assert
		UnityEngine.Assertions.Assert.IsNotNull(playerShip);

		yield return null;
	}

	[UnityTest]
	public IEnumerator Asteroid_moves()
	{
		// Arrange
		AsteroidManager asteroidManager = this.gameManager.GetAsteroidManager();

		// Act
		AsteroidSize size = AsteroidSize.Large;
		Vector2 spawnPosition = Vector2.zero;
		GameObject asteroidInstance = asteroidManager.SpawnAsteroid(size, spawnPosition);
		yield return new WaitForSeconds(0.1f);

		// Assert
		Assert.AreNotEqual(asteroidInstance.transform.position, spawnPosition);
	}

	[UnityTest]
	public IEnumerator Large_asteroid_hit_by_player_fire_breaks_into_two_medium_asteroids()
	{
		// Arrange
		GameObject playerShipInstance = this.gameManager.SpawnPlayerShip();
		PlayerShip playerShip = playerShipInstance.GetComponent<PlayerShip>();

		AsteroidManager asteroidManager = this.gameManager.GetAsteroidManager();
		AsteroidSize size = AsteroidSize.Large;
		Vector2 viewportPosition = new Vector2(0.5f, 0.75f);
		Vector2 worldPosition = Camera.main.ViewportToWorldPoint(viewportPosition);
		GameObject asteroidInstance = asteroidManager.SpawnAsteroid(size, worldPosition);

		// Act
		GameObject shotInstance = playerShip.Shoot();
		Rigidbody2D body = shotInstance.GetComponent<Rigidbody2D>();
		body.position = worldPosition;

		yield return new WaitForSeconds(0.1f);

		int asteroidCount = asteroidManager.GetAsteroidCount();
		int mediumAsteroidCount = 0;

		List<GameObject> asteroids = asteroidManager.GetAsteroids();

		foreach (GameObject instance in asteroids)
		{
			Asteroid asteroid = instance.GetComponent<Asteroid>();

			if (asteroid.Size == AsteroidSize.Medium)
			{
				mediumAsteroidCount++;
			}
		}

		Object.Destroy(playerShipInstance);

		// Assert
		Assert.AreEqual(2, asteroidCount);
		Assert.AreEqual(2, mediumAsteroidCount);
	}

	[UnityTest]
	public IEnumerator Medium_asteroid_hit_by_player_fire_breaks_into_two_small_asteroids()
	{
		// Arrange
		GameObject playerShipInstance = this.gameManager.SpawnPlayerShip();
		PlayerShip playerShip = playerShipInstance.GetComponent<PlayerShip>();

		AsteroidManager asteroidManager = this.gameManager.GetAsteroidManager();
		AsteroidSize size = AsteroidSize.Medium;
		Vector2 viewportPosition = new Vector2(0.5f, 0.75f);
		Vector2 worldPosition = Camera.main.ViewportToWorldPoint(viewportPosition);
		GameObject asteroidInstance = asteroidManager.SpawnAsteroid(size, worldPosition);

		// Act
		GameObject shotInstance = playerShip.Shoot();
		Rigidbody2D body = shotInstance.GetComponent<Rigidbody2D>();
		body.position = worldPosition;

		yield return new WaitForSeconds(0.1f);

		int asteroidCount = asteroidManager.GetAsteroidCount();
		int smallAsteroidCount = 0;

		List<GameObject> asteroids = asteroidManager.GetAsteroids();

		foreach (GameObject instance in asteroids)
		{
			Asteroid asteroid = instance.GetComponent<Asteroid>();

			if (asteroid.Size == AsteroidSize.Small)
			{
				smallAsteroidCount++;
			}
		}

		Object.Destroy(playerShipInstance);

		// Assert
		Assert.AreEqual(2, asteroidCount);
		Assert.AreEqual(2, smallAsteroidCount);
	}

	[UnityTest]
	public IEnumerator Small_asteroid_hit_by_player_fire_disappears()
	{
		// Arrange
		GameObject playerShipInstance = this.gameManager.SpawnPlayerShip();
		PlayerShip playerShip = playerShipInstance.GetComponent<PlayerShip>();

		AsteroidManager asteroidManager = this.gameManager.GetAsteroidManager();
		AsteroidSize size = AsteroidSize.Small;
		Vector2 viewportPosition = new Vector2(0.5f, 0.75f);
		Vector2 worldPosition = Camera.main.ViewportToWorldPoint(viewportPosition);
		GameObject asteroidInstance = asteroidManager.SpawnAsteroid(size, worldPosition);

		// Act
		GameObject shotInstance = playerShip.Shoot();
		Rigidbody2D body = shotInstance.GetComponent<Rigidbody2D>();
		body.position = worldPosition;

		yield return new WaitForSeconds(0.1f);

		int asteroidCount = asteroidManager.GetAsteroidCount();

		Object.Destroy(playerShipInstance);

		// Assert
		Assert.AreEqual(0, asteroidCount);
	}
}
