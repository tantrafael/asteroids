using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
	public GameObject playerShipPrefab;
	public GameObject enemyShipPrefab;

	private Camera camera;
	private PlayerShip playerShip;
	private AsteroidManager asteroidManager;

	private UnityAction asteroidCollisionAction;

	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);

		this.camera = Camera.main;
		this.asteroidManager = this.GetComponent<AsteroidManager>();

		asteroidCollisionAction = new UnityAction(this.HandleAsteroidCollision);

		this.InitializeGame();
	}

	private void OnEnable()
	{
		//EventManager.StartListening("AsteroidCollision", asteroidCollisionAction);
	}

	private void OnDisable()
	{
		//EventManager.StopListening("AsteroidCollision", asteroidCollisionAction);
	}

	private void OnDestroy()
	{
		this.DecommissionGame();
	}

	public PlayerShip GetPlayerShip()
	{
		return this.playerShip;
	}

	private void InitializeGame()
	{
		this.playerShip = this.SpawnPlayerShip();
		PlayerShip.OnCollision += HandlePlayerShipCollision;

		this.asteroidManager.SpawnAsteroids();
		//Asteroid.OnCollision += HandleAsteroidCollision;
		EventManager.StartListening("AsteroidCollision", asteroidCollisionAction);

		//StartCoroutine(this.SpawnEnemyShip());
		this.SpawnEnemyShip();
		//EnemyShip.OnCollision += HandleEnemyShipCollision;
		//CollisionDetector.OnCollision += HandleEnemyShipCollision;

		CollisionDetector.OnCollision += HandleCollision;
	}

	private PlayerShip SpawnPlayerShip()
	{
		GameObject playerShipInstance = Instantiate(playerShipPrefab, Vector3.zero, Quaternion.identity);
		PlayerShip playerShip = playerShipInstance.GetComponent<PlayerShip>();

		return playerShip;
	}

	private void SpawnEnemyShip()
	{
		int difficulty = 2;

		Vector2 viewportPosition = new Vector2(0.1f, 0.5f);
		Vector2 worldPosition = this.camera.ViewportToWorldPoint(viewportPosition);
		//int direction = 1;

		//GameObject enemyShipInstance = Instantiate(enemyShipPrefab);
		GameObject enemyShipInstance = Instantiate(enemyShipPrefab, worldPosition, Quaternion.identity);
		EnemyShip enemyShip = enemyShipInstance.GetComponent<EnemyShip>();
		enemyShip.Initialize(difficulty);
	}

	private void HandleAsteroidCollision()
	{
		Debug.Log("Asteroid collision");
	}

	/*
	private void HandleAsteroidCollision(GameObject asteroid, GameObject other)
	{
		if (other.tag == "Shot")
		{
			this.asteroidManager.HandleHit(asteroid);
			Destroy(other);
		}
	}
	*/

	private void HandlePlayerShipCollision(GameObject playerShip, GameObject other)
	{
		if (other.tag == "Asteroid")
		{
			//Destroy(playerShip);
		}
	}

	private void HandleEnemyShipCollision(GameObject enemyShip, GameObject other)
	{
		if (other.tag == "Shot")
		{
			Destroy(enemyShip);
		}
	}

	private void HandleCollision(GameObject gameObject, GameObject other)
	{
		if ((gameObject.tag == "EnemyShip") && (other.tag == "Shot"))
		{
			Destroy(gameObject);
			Destroy(other);
		}
	}

	private void DecommissionGame()
	{
		PlayerShip.OnCollision -= HandlePlayerShipCollision;
		//Asteroid.OnCollision -= HandleAsteroidCollision;
		EnemyShip.OnCollision -= HandleEnemyShipCollision;
		EventManager.StopListening("AsteroidCollision", asteroidCollisionAction);

		if (this.playerShip)
		{
			Object.Destroy(this.playerShip.gameObject);
		}

		//this.asteroidManager.Decommission();
	}
}
