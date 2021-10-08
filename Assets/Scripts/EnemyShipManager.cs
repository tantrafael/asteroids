using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class EnemyShipManager : MonoBehaviour
{
	public GameObject enemyShipPrefab;
	public int maxConcurrentEnemyShips = 1;

	private Camera mainCamera;
	private List<GameObject> enemyShips;

	//private UnityAction<object> enemyShipHitByShotAction;
	private UnityAction<object> enemyShipOutOfScopeAction;

	private ShotManager shotManager;

	private void Awake()
	{
		this.mainCamera = Camera.main;
		this.enemyShips = new List<GameObject>();

		//this.enemyShipHitByShotAction = new UnityAction<object>(this.HandleEnemyShipHitByShot);
		this.enemyShipOutOfScopeAction = new UnityAction<object>(this.HandleEnemyShipOutOfScope);
	}

	private void Start()
	{
		//EventManager.StartListening(GameEvent.EnemyShipHitByShot, this.enemyShipHitByShotAction);
		EventManager.StartListening(GameEvent.EnemyShipOutOfScope, this.enemyShipOutOfScopeAction);

		this.ScheduleNextEnemyShip();
	}

	public void Initialize(ShotManager shotManager)
	{
		this.shotManager = shotManager;
	}

	public void SpawnEnemyShip()
	{
		Assert.IsNotNull(this.mainCamera);
		Assert.IsNotNull(this.enemyShipPrefab);

		if (this.enemyShips.Count >= this.maxConcurrentEnemyShips)
		{
			Debug.LogWarning("Attempt to exceed allowed number of concurrent enemy ships.");
			return;
		}

		int size = 1;
		//float difficulty = 0.5f;
		float difficulty = Random.value;
		int direction = (Random.value > 0.5f) ? 1 : -1;
		Vector2 viewportPosition = Vector2.zero;
		viewportPosition.x = (direction == 1) ? 0.0f : 1.0f;
		viewportPosition.y = Random.value;
		Vector2 worldPosition = this.mainCamera.ViewportToWorldPoint(viewportPosition);

		GameObject enemyShipInstance = Instantiate(this.enemyShipPrefab, worldPosition, Quaternion.identity);
		EnemyShip enemyShip = enemyShipInstance.GetComponent<EnemyShip>();
		//enemyShip.Initialize(size, difficulty, direction);
		enemyShip.Initialize(size, difficulty, direction, this.shotManager);
		this.enemyShips.Add(enemyShipInstance);
	}

	public void HandleEnemyShipHitByShot(GameObject enemyShipInstance)
	{
		this.DeleteEnemyShip(enemyShipInstance);
		this.ScheduleNextEnemyShip();
	}

	/*
	public void HandleEnemyShipHitByShot(object data)
	{
		CollisionData collisionData = (CollisionData)data;
		GameObject enemyShipInstance = collisionData.other;

		this.DeleteEnemyShip(enemyShipInstance);
		this.ScheduleNextEnemyShip();
	}
	*/

	public void HandleEnemyShipOutOfScope(object data)
	{
		GameObject enemyShipInstance = (GameObject)data;
		this.DeleteEnemyShip(enemyShipInstance);
		this.ScheduleNextEnemyShip();
	}

	private void ScheduleNextEnemyShip()
	{
		float timeUntilSpawning = 5.0f;
		this.StartCoroutine(this.ControlEnemyShipSpawning(timeUntilSpawning));
	}

	private IEnumerator ControlEnemyShipSpawning(float delay)
	{
		// TODO: Remove magic number.
		yield return new WaitForSeconds(delay);
		this.SpawnEnemyShip();
	}

	// TODO: Consider handling this in a coroutine.
	private void FixedUpdate()
	{
		// TODO: Consider perfomance.
		List<GameObject> enemyShipsOutOfScope = new List<GameObject>();

		foreach (GameObject enemyShipInstance in this.enemyShips)
		{
			Vector2 worldPosition = enemyShipInstance.transform.position;
			Vector2 viewportPosition = this.mainCamera.WorldToViewportPoint(worldPosition);

			if (!this.IsWithinScope(viewportPosition))
			{
				enemyShipsOutOfScope.Add(enemyShipInstance);
			}
		}

		foreach (GameObject enemyShipInstance in enemyShipsOutOfScope)
		{
			EventManager.TriggerEvent(GameEvent.EnemyShipOutOfScope, enemyShipInstance);
		}
	}

	private bool IsWithinScope(Vector2 position)
	{
		return (position.x >= 0.0f) && (position.x <= 1.0f);
	}

	private void DeleteEnemyShip(GameObject enemyShipInstance)
	{
		this.enemyShips.Remove(enemyShipInstance);
		Destroy(enemyShipInstance);
	}

	private void OnDestroy()
	{
		//EventManager.StopListening(GameEvent.EnemyShipHitByShot, this.enemyShipHitByShotAction);
		EventManager.StopListening(GameEvent.EnemyShipOutOfScope, this.enemyShipOutOfScopeAction);

		this.StopAllCoroutines();
	}
}
