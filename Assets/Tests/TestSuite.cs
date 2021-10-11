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
	public IEnumerator PlayerShipSpawnsWhenGameStarts()
	{
		this.gameManager.StartGame();
		PlayerShip playerShip = this.gameManager.GetPlayerShip();
		UnityEngine.Assertions.Assert.IsNotNull(playerShip);

		yield return null;
	}

	[UnityTest]
	public IEnumerator AsteroidsMove()
	{
		AsteroidManager asteroidManager = this.gameManager.GetAsteroidManager();

		int size = 3;
		Vector2 spawnPosition = Vector2.zero;
		GameObject asteroidInstance = asteroidManager.SpawnAsteroid(size, spawnPosition);
		yield return new WaitForSeconds(0.1f);

		Assert.AreNotEqual(asteroidInstance.transform.position, spawnPosition);
	}
}
