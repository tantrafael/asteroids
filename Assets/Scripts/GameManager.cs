using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
	public GameObject playerShipPrefab;
	public GameObject enemyShipPrefab;

	private Camera mainCamera;
	private PlayerShip playerShip;
	private AsteroidManager asteroidManager;
	private EnemyShipManager enemyShipManager;

	// Actions
	private UnityAction<object> asteroidHitByShotAction;
	private UnityAction<object> enemyShipHitByShotAction;
	private UnityAction<object> playerShipHitByAsteroidAction;

	public float Level { get; private set; }
	public float Difficulty { get; private set; }

	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);

		this.mainCamera = Camera.main;

		this.asteroidManager = this.GetComponent<AsteroidManager>();
		this.enemyShipManager = this.GetComponent<EnemyShipManager>();

		this.asteroidHitByShotAction = new UnityAction<object>(this.HandleAsteroidHitByShot);
		this.enemyShipHitByShotAction = new UnityAction<object>(this.HandleEnemyShipHitByShot);
		this.playerShipHitByAsteroidAction = new UnityAction<object>(this.HandlePlayerShipHitByAsteroid);

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

		//Destroy(enemyShip);
		this.enemyShipManager.HandleEnemyShipHitByShot(enemyShip);
		Destroy(shot);
	}

	private void HandlePlayerShipHitByAsteroid(object data)
	{
		CollisionData collisionData = (CollisionData)data;
		GameObject playerShip = collisionData.self;

		Destroy(playerShip);
	}

	private void DecommissionGame()
	{
		EventManager.StopListening(GameEvent.AsteroidHitByShot, this.asteroidHitByShotAction);
		EventManager.StopListening(GameEvent.EnemyShipHitByShot, this.enemyShipHitByShotAction);
		EventManager.StopListening(GameEvent.PlayerShipHitByAsteroid, this.playerShipHitByAsteroidAction);

		if (this.playerShip)
		{
			Destroy(this.playerShip.gameObject);
		}

		//this.asteroidManager.Decommission();
	}
}
