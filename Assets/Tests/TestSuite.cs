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
		Object.Destroy(gameManager.gameObject);
	}

	[UnityTest]
	public IEnumerator PlayerShipSpawns()
	{
		PlayerShip playerShip = gameManager.GetPlayerShip();
		UnityEngine.Assertions.Assert.IsNotNull(playerShip);

		yield return null;
	}
}
