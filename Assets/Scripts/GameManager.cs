using System;
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

	//private UnityAction<object> collisionAction;
	//private UnityAction asteroidCollisionAction;
	private UnityAction<object> asteroidHitByShotAction;

	//private Dictionary<Tuple<string, string>, string> collisionEventTable;

	private void Awake()
	{
		/*
		this.collisionEventTable = new Dictionary<Tuple<string, string>, string>
		{
			[new Tuple<string, string>("Asteroid", "Shot")] = "AsteroidHitByShot",
			[new Tuple<string, string>("EnemyShip", "Shot")] = "EnemyShipHitByShot",
			[new Tuple<string, string>("PlayerShip", "Asteroid")] = "PlayerShipHitByAsteroid"
		};
		*/

		DontDestroyOnLoad(this.gameObject);

		this.camera = Camera.main;
		this.asteroidManager = this.GetComponent<AsteroidManager>();

		//collisionAction = new UnityAction<object>(this.HandleCollision);
		//asteroidCollisionAction = new UnityAction(this.HandleAsteroidCollision);
		asteroidHitByShotAction = new UnityAction<object>(this.HandleAsteroidHitByShot);

		//EventManager.StartListening("Collision", collisionAction);

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
		//EventManager.StartListening("AsteroidCollision", asteroidCollisionAction);

		//StartCoroutine(this.SpawnEnemyShip());
		this.SpawnEnemyShip();
		//EnemyShip.OnCollision += HandleEnemyShipCollision;
		//CollisionDetector.OnCollision += HandleEnemyShipCollision;

		//CollisionDetector.OnCollision += HandleCollision;
		//EventManager.StartListening("Collision", collisionAction);
		EventManager.StartListening("AsteroidHitByShot", asteroidHitByShotAction);
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

	/*
	private void HandleCollision(object data)
	{
		//Debug.Log("GameManager::HandleCollision");

		CollisionData collisionData = (CollisionData)data;
		var key = new Tuple<string, string>(collisionData.self.tag, collisionData.other.tag);
		string eventName;
		this.collisionEventTable.TryGetValue(key, out eventName);

		if (eventName != null)
		{
			EventManager.TriggerEvent(eventName);
		}
	}
	*/

	private void HandleAsteroidHitByShot(object data)
	{
		CollisionData collisionData = (CollisionData)data;
		GameObject asteroid = collisionData.self;
		GameObject shot = collisionData.other;

		Destroy(collisionData.self);
		Destroy(collisionData.other);
	}

	/*
	private void HandleCollision(GameObject gameObject, GameObject other)
	{
		if ((gameObject.tag == "EnemyShip") && (other.tag == "Shot"))
		{
			Destroy(gameObject);
			Destroy(other);
		}
	}
	*/

	/*
	private void HandleAsteroidCollision(object data)
	{
		Debug.Log("Asteroid collision");
		CollisionData collisionData = (CollisionData)data;
		Debug.Log(collisionData.gameObject + ", " + collisionData.other);
	}
	*/

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

	private void DecommissionGame()
	{
		PlayerShip.OnCollision -= HandlePlayerShipCollision;
		//Asteroid.OnCollision -= HandleAsteroidCollision;
		EnemyShip.OnCollision -= HandleEnemyShipCollision;
		//EventManager.StopListening("AsteroidCollision", asteroidCollisionAction);

		if (this.playerShip)
		{
			Destroy(this.playerShip.gameObject);
		}

		//this.asteroidManager.Decommission();
	}
}
