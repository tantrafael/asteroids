using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject playerShipPrefab;
	private PlayerShip playerShip;

	public PlayerShip GetPlayerShip()
	{
		return this.playerShip;
	}

	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
		this.InitializeGame();
	}

	private void OnDestroy()
	{
		this.DecommissionGame();
	}

	private void InitializeGame()
	{
		this.playerShip = this.SpawnPlayerShip();
	}

	private PlayerShip SpawnPlayerShip()
	{
		GameObject playerShipInstance = Instantiate(playerShipPrefab, Vector3.zero, Quaternion.identity);
		PlayerShip playerShip = playerShipInstance.GetComponent<PlayerShip>();

		return playerShip;
	}

	private void DecommissionGame()
	{
		if (this.playerShip)
		{
			Object.Destroy(this.playerShip.gameObject);
		}
	}
}
