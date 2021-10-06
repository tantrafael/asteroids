using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
	public GameObject playerShipPrefab;
	public GameObject enemyShipPrefab;

	private Camera camera;
	private PlayerShip playerShip;
	private AsteroidManager asteroidManager;
	private UnityAction<object> asteroidHitByShotAction;
	private UnityAction<object> enemyShipHitByShotAction;
	private UnityAction<object> playerShipHitByAsteroidAction;

	public float Level { get; private set; }
	public float Difficulty { get; private set; }

	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);

		this.camera = Camera.main;
		this.asteroidManager = this.GetComponent<AsteroidManager>();
		this.asteroidHitByShotAction = new UnityAction<object>(this.HandleAsteroidHitByShot);
		this.enemyShipHitByShotAction = new UnityAction<object>(this.HandleEnemyShipHitByShot);
		this.playerShipHitByAsteroidAction = new UnityAction<object>(this.HandleplayerShipHitByAsteroid);

		this.InitializeGame();
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

		this.asteroidManager.SpawnAsteroids();

		//StartCoroutine(this.SpawnEnemyShip());
		this.SpawnEnemyShip();

		EventManager.StartListening(GameEvent.AsteroidHitByShot, this.asteroidHitByShotAction);
		EventManager.StartListening(GameEvent.EnemyShipHitByShot, this.enemyShipHitByShotAction);
		EventManager.StartListening(GameEvent.PlayerShipHitByAsteroid, this.playerShipHitByAsteroidAction);
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

	private void HandleAsteroidHitByShot(object data)
	{
		CollisionData collisionData = (CollisionData)data;
		GameObject asteroid = collisionData.self;
		GameObject shot = collisionData.other;

		this.asteroidManager.HandleAsteroidHitByShot(asteroid);
		Destroy(shot);
	}

	private void HandleEnemyShipHitByShot(object data)
	{
		CollisionData collisionData = (CollisionData)data;
		GameObject enemyShip = collisionData.self;
		GameObject shot = collisionData.other;

		Destroy(enemyShip);
		Destroy(shot);
	}

	private void HandleplayerShipHitByAsteroid(object data)
	{
		CollisionData collisionData = (CollisionData)data;
		GameObject playerShip = collisionData.self;

		Destroy(playerShip);
	}

	private void DecommissionGame()
	{
		EventManager.StartListening(GameEvent.AsteroidHitByShot, asteroidHitByShotAction);

		if (this.playerShip)
		{
			Destroy(this.playerShip.gameObject);
		}

		//this.asteroidManager.Decommission();
	}
}
