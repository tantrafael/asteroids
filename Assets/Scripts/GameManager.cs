using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject playerShipPrefab;
	//private GameObject playerShipInstance;
	private PlayerShip playerShip;

	/*
	public PlayerShip GetPlayerShip()
	{
		return playerShip;
	}
	*/

	public PlayerShip SpawnPlayerShip()
	{
		/*
		playerShipInstance = Instantiate(playerShipPrefab, Vector3.zero, Quaternion.identity);
		playerShip = playerShipInstance.GetComponent<PlayerShip>();

		return playerShip;
		*/

		return null;
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
		playerShip = this.SpawnPlayerShip();
	}

	private void DecommissionGame()
	{
		Object.Destroy(playerShip.gameObject);
	}
}
