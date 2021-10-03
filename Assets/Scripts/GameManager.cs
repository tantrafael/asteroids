using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject playerShipPrefab;

	private PlayerShip playerShip;
	private AsteroidManager asteroidManager;

	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
		this.asteroidManager = this.GetComponent<AsteroidManager>();
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
		this.SpawnAsteroids();
	}

	private PlayerShip SpawnPlayerShip()
	{
		GameObject playerShipInstance = Instantiate(playerShipPrefab, Vector3.zero, Quaternion.identity);
		PlayerShip playerShip = playerShipInstance.GetComponent<PlayerShip>();

		return playerShip;
	}

	private void SpawnAsteroids()
	{
		for (var i = 0; i < 10; ++i)
		{
			Vector2 position = Vector2.zero;
			position.x = 6.0f * Random.Range(-1.0f, 1.0f);
			position.y = 6.0f * Random.Range(-1.0f, 1.0f);

			Vector2 direction = Vector2.zero;
			direction.x = Random.Range(-1.0f, 1.0f);
			direction.y = Random.Range(-1.0f, 1.0f);
			float speed = Random.Range(1.0f, 2.0f);
			Vector2 velocity = speed * direction;

			this.asteroidManager.SpawnAsteroid(position, velocity);
		}
	}

	private void DecommissionGame()
	{
		if (this.playerShip)
		{
			Object.Destroy(this.playerShip.gameObject);
		}
	}
}
