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
	private ShotManager shotManager;

	private UnityAction<object> asteroidHitByPlayerShotAction;
	private UnityAction<object> enemyShipHitByPlayerShotAction;
	private UnityAction<object> playerShipHitByAsteroidAction;
	private UnityAction<object> playerShipHitByEnemyShotAction;
	private UnityAction<object> playerShipHitByEnemyShipAction;

	public float Level { get; private set; }
	public float Difficulty { get; private set; }

	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);

		this.mainCamera = Camera.main;

		this.asteroidManager = this.GetComponent<AsteroidManager>();
		this.shotManager = this.GetComponent<ShotManager>();
		this.enemyShipManager = this.GetComponent<EnemyShipManager>();
		this.enemyShipManager.Initialize(this.shotManager);

		this.asteroidHitByPlayerShotAction = new UnityAction<object>(this.HandleAsteroidHitByPlayerShot);
		this.enemyShipHitByPlayerShotAction = new UnityAction<object>(this.HandleEnemyShipHitByPlayerShot);
		this.playerShipHitByAsteroidAction = new UnityAction<object>(this.HandlePlayerShipHitByAsteroid);
		this.playerShipHitByEnemyShotAction = new UnityAction<object>(this.HandlePlayerShipHitByEnemyShot);
		this.playerShipHitByEnemyShipAction = new UnityAction<object>(this.HandlePlayerShipHitByEnemyShip);

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
		EventManager.StartListening(GameEvent.AsteroidHitByPlayerShot, this.asteroidHitByPlayerShotAction);
		EventManager.StartListening(GameEvent.EnemyShipHitByPlayerShot, this.enemyShipHitByPlayerShotAction);
		EventManager.StartListening(GameEvent.PlayerShipHitByAsteroid, this.playerShipHitByAsteroidAction);
		EventManager.StartListening(GameEvent.PlayerShipHitByEnemyShot, this.playerShipHitByEnemyShotAction);
		EventManager.StartListening(GameEvent.PlayerShipHitByEnemyShip, this.playerShipHitByEnemyShipAction);

		this.playerShip = this.SpawnPlayerShip();
		this.asteroidManager.SpawnAsteroids();
	}

	private PlayerShip SpawnPlayerShip()
	{
		GameObject playerShipInstance = Instantiate(playerShipPrefab, Vector3.zero, Quaternion.identity);
		PlayerShip playerShip = playerShipInstance.GetComponent<PlayerShip>();
		playerShip.Initialize(this.shotManager);

		return playerShip;
	}

	private void HandleAsteroidHitByPlayerShot(object data)
	{
		CollisionData collisionData = (CollisionData)data;
		GameObject asteroid = collisionData.self;
		GameObject shot = collisionData.other;

		this.asteroidManager.HandleAsteroidHit(asteroid);
		this.shotManager.DeleteShot(shot);
	}

	private void HandleEnemyShipHitByPlayerShot(object data)
	{
		CollisionData collisionData = (CollisionData)data;
		GameObject enemyShip = collisionData.self;
		GameObject shot = collisionData.other;

		this.enemyShipManager.HandleEnemyShipHit(enemyShip);
		this.shotManager.DeleteShot(shot);
	}

	private void HandlePlayerShipHitByAsteroid(object data)
	{
		CollisionData collisionData = (CollisionData)data;
		GameObject playerShip = collisionData.self;

		Destroy(playerShip);
	}

	private void HandlePlayerShipHitByEnemyShot(object data)
	{
		CollisionData collisionData = (CollisionData)data;
		GameObject playerShip = collisionData.self;
		GameObject shot = collisionData.other;

		Destroy(playerShip);
		this.shotManager.DeleteShot(shot);
	}

	private void HandlePlayerShipHitByEnemyShip(object data)
	{
		CollisionData collisionData = (CollisionData)data;
		GameObject playerShip = collisionData.self;

		Destroy(playerShip);
	}

	private void DecommissionGame()
	{
		EventManager.StopListening(GameEvent.AsteroidHitByPlayerShot, this.asteroidHitByPlayerShotAction);
		EventManager.StopListening(GameEvent.EnemyShipHitByPlayerShot, this.enemyShipHitByPlayerShotAction);
		EventManager.StopListening(GameEvent.PlayerShipHitByAsteroid, this.playerShipHitByAsteroidAction);
		EventManager.StopListening(GameEvent.PlayerShipHitByEnemyShot, this.playerShipHitByEnemyShotAction);
		EventManager.StopListening(GameEvent.PlayerShipHitByEnemyShip, this.playerShipHitByEnemyShipAction);

		if (this.playerShip)
		{
			Destroy(this.playerShip.gameObject);
		}

		//this.asteroidManager.Decommission();
	}
}
