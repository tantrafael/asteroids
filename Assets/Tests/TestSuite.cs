using System.Collections;
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
		this.gameManager.StartGame();
		PlayerShip playerShip = this.gameManager.GetPlayerShip();
		UnityEngine.Assertions.Assert.IsNotNull(playerShip);

		yield return null;
	}

	[UnityTest]
	public IEnumerator Asteroid_moves()
	{
		AsteroidManager asteroidManager = this.gameManager.GetAsteroidManager();

		int size = 3;
		Vector2 spawnPosition = Vector2.zero;
		GameObject asteroidInstance = asteroidManager.SpawnAsteroid(size, spawnPosition);
		yield return new WaitForSeconds(0.1f);

		Assert.AreNotEqual(asteroidInstance.transform.position, spawnPosition);
	}

	[UnityTest]
	public IEnumerator Asteroid_hit_by_player_fire_breaks_into_two_smaller_asteroids()
	{
		GameObject playerShipInstance = this.gameManager.SpawnPlayerShip();
		PlayerShip playerShip = playerShipInstance.GetComponent<PlayerShip>();

		AsteroidManager asteroidManager = this.gameManager.GetAsteroidManager();
		int size = 3;
		Vector2 viewportPosition = new Vector2(0.5f, 0.25f);
		Vector2 worldPosition = Camera.main.ViewportToWorldPoint(viewportPosition);
		GameObject asteroidInstance = asteroidManager.SpawnAsteroid(size, worldPosition);

		GameObject shotInstance = playerShip.Shoot();
		Rigidbody2D body = shotInstance.GetComponent<Rigidbody2D>();
		body.position = worldPosition;

		yield return new WaitForSeconds(0.1f);

		Assert.Equals(asteroidManager.AsteroidCount, 2);
	}
}
