using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject playerShipPrefab;
	private GameObject playerShipInstance;

	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
		InitializeGame();
	}

	private void OnDestroy()
	{
		DecommissionGame();
	}

	private void InitializeGame()
	{
		playerShipInstance = Instantiate(playerShipPrefab, Vector3.zero, Quaternion.identity);
	}

	private void DecommissionGame()
	{
		Object.Destroy(playerShipInstance);
	}
}
