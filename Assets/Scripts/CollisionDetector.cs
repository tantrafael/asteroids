using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct CollisionData
{
	public GameObject self;
	public GameObject other;
}

public class CollisionDetector : MonoBehaviour
{
	public static readonly Dictionary<Tuple<string, string>, string> collisionEventTable = new Dictionary<Tuple<string, string>, string>
	{
		[new Tuple<string, string>("Asteroid", "Shot")] = "AsteroidHitByShot",
		[new Tuple<string, string>("EnemyShip", "Shot")] = "EnemyShipHitByShot",
		[new Tuple<string, string>("PlayerShip", "Asteroid")] = "PlayerShipHitByAsteroid"
	};

	/*
	public delegate void CollisionAction(GameObject gameObject, GameObject other);
	public static event CollisionAction OnCollision;
	*/

	private void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log("CollisionDetectro::OnTriggerEnter2D");

		/*
		if (OnCollision != null)
		{
			OnCollision(this.gameObject, other.gameObject);
		}
		*/

		var key = new Tuple<string, string>(this.gameObject.tag, other.gameObject.tag);
		string eventName;
		CollisionDetector.collisionEventTable.TryGetValue(key, out eventName);

		if (eventName != null)
		{
			CollisionData collisionData;
			collisionData.self = this.gameObject;
			collisionData.other = other.gameObject;
			EventManager.TriggerEvent(eventName, collisionData);
		}
	}
}
