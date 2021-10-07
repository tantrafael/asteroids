using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class EnemyShipManager : MonoBehaviour
{
	public GameObject enemyShipPrefab;
	public int maxConcurrentEnemyShips = 1;

	private Camera mainCamera;
	private List<GameObject> enemyShips = new List<GameObject>();

	private UnityAction<object> enemyShipLeftScopeAction;

	private void Awake()
	{
		this.mainCamera = Camera.main;

		this.enemyShipLeftScopeAction = new UnityAction<object>(this.HandleEnemyShipLeftScope);
	}

	private void Start()
	{
		EventManager.StartListening(GameEvent.EnemyShipLeftScope, this.enemyShipLeftScopeAction);
		this.ScheduleNextEnemyShip();
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

		GameObject enemyShipInstance = Instantiate(enemyShipPrefab, worldPosition, Quaternion.identity);
		EnemyShip enemyShip = enemyShipInstance.GetComponent<EnemyShip>();
		enemyShip.Initialize(size, difficulty, direction);
		this.enemyShips.Add(enemyShipInstance);
	}

	public void HandleEnemyShipHitByShot(GameObject enemyShipInstance)
	{
		this.DeleteEnemyShip(enemyShipInstance);
		this.ScheduleNextEnemyShip();
	}

	public void HandleEnemyShipLeftScope(object data)
	{
		GameObject enemyShipInstance = (GameObject)data;
		this.DeleteEnemyShip(enemyShipInstance);
		this.ScheduleNextEnemyShip();
	}

	private async void ScheduleNextEnemyShip()
	{
		// TODO: Remove magic number.
		await Task.Delay(System.TimeSpan.FromSeconds(5.0f));
		this.SpawnEnemyShip();
	}

	private void FixedUpdate()
	{
		List<GameObject> enemyShipsOutOfScope = new List<GameObject>();

		foreach (GameObject enemyShip in enemyShips)
		{
			Vector2 worldPosition = enemyShip.transform.position;
			Vector2 viewportPosition = this.mainCamera.WorldToViewportPoint(worldPosition);

			if (!this.IsWithinScope(viewportPosition))
			{
				enemyShipsOutOfScope.Add(enemyShip);
			}
		}

		foreach (GameObject enemyShip in enemyShipsOutOfScope)
		{
			EventManager.TriggerEvent(GameEvent.EnemyShipLeftScope, enemyShip);
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
}
