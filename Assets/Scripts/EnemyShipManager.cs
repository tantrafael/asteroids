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

	private EventManager eventManager;
	private ShotManager shotManager;

	private void Awake()
	{
		this.mainCamera = Camera.main;
		this.enemyShips = new List<GameObject>();
	}

	public void Initialize(EventManager eventManager, ShotManager shotManager)
	{
		this.eventManager = eventManager;
		this.shotManager = shotManager;
	}

	public void ScheduleEnemyShip()
	{
		// TODO: Remove magic number.
		float timeUntilSpawning = 5.0f;
		this.StartCoroutine(this.ControlEnemyShipSpawning(timeUntilSpawning));
	}

	public GameObject SpawnEnemyShip()
	{
		Assert.IsNotNull(this.mainCamera);
		Assert.IsNotNull(this.enemyShipPrefab);

		if (this.enemyShips.Count >= this.maxConcurrentEnemyShips)
		{
			Debug.LogWarning("Attempt to exceed allowed number of concurrent enemy ships.");
			return null;
		}

		int size = 1;
		float difficulty = Random.value;
		int direction = (Random.value > 0.5f) ? 1 : -1;
		Vector2 viewportPosition = Vector2.zero;
		viewportPosition.x = (direction == 1) ? 0.0f : 1.0f;
		viewportPosition.y = Random.value;
		Vector2 worldPosition = this.mainCamera.ViewportToWorldPoint(viewportPosition);

		GameObject enemyShipInstance = Instantiate(this.enemyShipPrefab, worldPosition, Quaternion.identity);
		EnemyShip enemyShip = enemyShipInstance.GetComponent<EnemyShip>();
		enemyShip.Initialize(size, difficulty, direction, this.eventManager, this.shotManager);
		this.enemyShips.Add(enemyShipInstance);

		return enemyShipInstance;
	}

	public void HandleEnemyShipHit(GameObject enemyShipInstance)
	{
		this.DeleteEnemyShip(enemyShipInstance);
		this.ScheduleEnemyShip();
	}

	private void HandleEnemyShipOutOfScope(GameObject enemyShipInstance)
	{
		this.DeleteEnemyShip(enemyShipInstance);
		this.ScheduleEnemyShip();
	}

	private IEnumerator ControlEnemyShipSpawning(float delay)
	{
		yield return new WaitForSeconds(delay);
		this.SpawnEnemyShip();
	}

	// TODO: Consider handling this in a coroutine.
	private void FixedUpdate()
	{
		for (var i = this.enemyShips.Count - 1; i >= 0; --i)
		{
			GameObject enemyShipInstance = this.enemyShips[i];
			Vector2 worldPosition = enemyShipInstance.transform.position;
			Vector2 viewportPosition = this.mainCamera.WorldToViewportPoint(worldPosition);

			if (!this.IsWithinScope(viewportPosition))
			{
				this.HandleEnemyShipOutOfScope(enemyShipInstance);
			}
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

	public void ClearEnemyShips()
	{
		this.StopAllCoroutines();

		for (var i = this.enemyShips.Count - 1; i >= 0; --i)
		{
			GameObject enemyShipInstance = this.enemyShips[i];
			this.enemyShips.RemoveAt(i);
			Object.Destroy(enemyShipInstance);
		}
	}

	private void OnDestroy()
	{
		this.ClearEnemyShips();
	}
}
