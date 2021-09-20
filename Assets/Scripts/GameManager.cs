using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
	public class GameManager : MonoBehaviour
	{
		private void Awake()
		{
			DontDestroyOnLoad(this.gameObject);

			// Initialize game.
			Debug.Log(Camera.main.pixelWidth);
		}

		private void OnDestroy()
		{
			// Decommission game.
		}
	}
}