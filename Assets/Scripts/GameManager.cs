using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
	public GameObject playerShipPrefab;

	private Camera mainCamera;
	private EventManager eventManager;
	private AsteroidManager asteroidManager;
	private EnemyShipManager enemyShipManager;
	private ShotManager shotManager;
	private PlayerShip playerShip;

	private UnityAction<object> asteroidHitByPlayerShotAction;
	private UnityAction<object> enemyShipHitByPlayerShotAction;
	private UnityAction<object> playerShipHitByAsteroidAction;
	private UnityAction<object> playerShipHitByEnemyShotAction;
	private UnityAction<object> playerShipHitByEnemyShipAction;

	public int Score { get; private set; }
	public int Lives { get; private set; }
	public int Level { get; private set; }

	public PlayerShip GetPlayerShip()
	{
		return this.playerShip;
	}

	public AsteroidManager GetAsteroidManager()
	{
		return this.asteroidManager;
	}

	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);

		this.mainCamera = Camera.main;

		this.eventManager = new EventManager();

		this.asteroidManager = this.GetComponent<AsteroidManager>();
		this.asteroidManager.Initialize(this.eventManager);

		this.shotManager = this.GetComponent<ShotManager>();

		this.enemyShipManager = this.GetComponent<EnemyShipManager>();
		this.enemyShipManager.Initialize(this.eventManager, this.shotManager);

		this.asteroidHitByPlayerShotAction = new UnityAction<object>(this.HandleAsteroidHitByPlayerShot);
		this.enemyShipHitByPlayerShotAction = new UnityAction<object>(this.HandleEnemyShipHitByPlayerShot);
		this.playerShipHitByAsteroidAction = new UnityAction<object>(this.HandlePlayerShipHitByAsteroid);
		this.playerShipHitByEnemyShotAction = new UnityAction<object>(this.HandlePlayerShipHitByEnemyShot);
		this.playerShipHitByEnemyShipAction = new UnityAction<object>(this.HandlePlayerShipHitByEnemyShip);
	}

	private void Start()
	{
		this.eventManager.StartListening(GameEvent.AsteroidHitByPlayerShot, this.asteroidHitByPlayerShotAction);
		this.eventManager.StartListening(GameEvent.EnemyShipHitByPlayerShot, this.enemyShipHitByPlayerShotAction);
		this.eventManager.StartListening(GameEvent.PlayerShipHitByAsteroid, this.playerShipHitByAsteroidAction);
		this.eventManager.StartListening(GameEvent.PlayerShipHitByEnemyShot, this.playerShipHitByEnemyShotAction);
		this.eventManager.StartListening(GameEvent.PlayerShipHitByEnemyShip, this.playerShipHitByEnemyShipAction);
	}

	private void Update()
	{
		bool startButtonInput = Input.GetKeyDown("return");

		if (startButtonInput == true)
		{
			this.EndGame();
			this.StartGame();
		}
	}

	public void StartGame()
	{
		GameObject playerShipInstance = this.SpawnPlayerShip();
		this.playerShip = playerShipInstance.GetComponent<PlayerShip>();
		this.asteroidManager.SpawnAsteroids();
		this.enemyShipManager.ScheduleEnemyShip();
	}

	public void EndGame()
	{
		if (this.playerShip)
		{
			Destroy(this.playerShip.gameObject);
		}

		this.asteroidManager.ClearAsteroids();
		this.enemyShipManager.ClearEnemyShips();
		this.shotManager.ClearShots();
	}

	public GameObject SpawnPlayerShip()
	{
		GameObject playerShipInstance = Instantiate(playerShipPrefab, Vector3.zero, Quaternion.identity);
		PlayerShip playerShip = playerShipInstance.GetComponent<PlayerShip>();
		playerShip.Initialize(this.eventManager, this.shotManager);

		return playerShipInstance;
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

	private void OnDestroy()
	{
		this.eventManager.StopListening(GameEvent.AsteroidHitByPlayerShot, this.asteroidHitByPlayerShotAction);
		this.eventManager.StopListening(GameEvent.EnemyShipHitByPlayerShot, this.enemyShipHitByPlayerShotAction);
		this.eventManager.StopListening(GameEvent.PlayerShipHitByAsteroid, this.playerShipHitByAsteroidAction);
		this.eventManager.StopListening(GameEvent.PlayerShipHitByEnemyShot, this.playerShipHitByEnemyShotAction);
		this.eventManager.StopListening(GameEvent.PlayerShipHitByEnemyShip, this.playerShipHitByEnemyShipAction);

		this.EndGame();
	}
}
