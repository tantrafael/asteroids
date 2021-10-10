//using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
//using UnityEditor.SceneManagement;
//using UnityEngine.SceneManagement;

public class TestSuite
{
	private GameManager gameManager;

	[SetUp]
	/*
	public void ResetScene()
	{
		EditorSceneManager.NewScene(NewSceneSetup.EmptyScene);
	}
	*/

	public void Setup()
	{
		GameObject gameManagerPrefab = Resources.Load<GameObject>("Prefabs/GameManager");
		GameObject gameManagerInstance = MonoBehaviour.Instantiate(gameManagerPrefab);
		gameManager = gameManagerInstance.GetComponent<GameManager>();

		/*
		//EditorSceneManager.LoadScene("Scenes/Game");
		//SceneManager.LoadScene("Scenes/Game");
		//SceneManager.LoadScene("Scenes/Game", LoadSceneMode.Additive);
		//SceneManager.SetActiveScene(SceneManager.GetSceneByName("Scenes/Game"));
		//var activeScene = EditorSceneManager.GetActiveScene();
		//var activeScene = SceneManager.GetActiveScene();
		//GameObject[] rootGameObjects = activeScene.GetRootGameObjects();
		//GameObject gameManager = Array.Find(rootGameObjects, element => element.name == "GameManager");

		foreach (GameObject go in rootGameObjects)
		{
			Debug.Log("Name: " + go.name);
		}
		*/
	}

	[TearDown]
	public void Teardown()
	{
		Object.Destroy(this.gameManager.gameObject);
	}

	[UnityTest]
	public IEnumerator PlayerShipSpawns()
	{
		PlayerShip playerShip = this.gameManager.GetPlayerShip();
		UnityEngine.Assertions.Assert.IsNotNull(playerShip);

		yield return null;
	}

	[UnityTest]
	public IEnumerator AsteroidsMove()
	{
		AsteroidManager asteroidManager = this.gameManager.GetAsteroidManager();

		int size = 1;
		Vector2 spawnPosition = Vector2.zero;
		GameObject asteroidInstance = asteroidManager.SpawnAsteroid(size, spawnPosition);
		yield return new WaitForSeconds(0.1f);
		//yield return new WaitForFixedUpdate();

		Assert.AreNotEqual(asteroidInstance.transform.position, spawnPosition);
	}
}
