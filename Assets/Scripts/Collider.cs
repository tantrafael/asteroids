using UnityEngine;

using CollisionEventTable = System.Collections.Generic.Dictionary<System.Tuple<ColliderType, ColliderType>, GameEvent>;
using CollisionPair = System.Tuple<ColliderType, ColliderType>;

public enum ColliderType
{
	Asteroid,
	PlayerShip,
	PlayerShot,
	EnemyShip,
	EnemyShot
};

struct CollisionData
{
	public GameObject self;
	public GameObject other;
}

public class Collider : MonoBehaviour
{
	// Lookup table for collisions that cause events.
	private static readonly CollisionEventTable collisionEventTable = new CollisionEventTable
	{
		[new CollisionPair(ColliderType.Asteroid, ColliderType.PlayerShot)] = GameEvent.AsteroidHitByPlayerShot,
		[new CollisionPair(ColliderType.EnemyShip, ColliderType.PlayerShot)] = GameEvent.EnemyShipHitByPlayerShot,
		[new CollisionPair(ColliderType.PlayerShip, ColliderType.Asteroid)] = GameEvent.PlayerShipHitByAsteroid,
		[new CollisionPair(ColliderType.PlayerShip, ColliderType.EnemyShot)] = GameEvent.PlayerShipHitByEnemyShot,
		[new CollisionPair(ColliderType.PlayerShip, ColliderType.EnemyShip)] = GameEvent.PlayerShipHitByEnemyShip
	};

	public ColliderType Type { get; private set; }

	public void Initialize(ColliderType type)
	{
		this.Type = type;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Collider otherCollider = other.gameObject.GetComponent<Collider>();

		if (!otherCollider)
		{
			return;
		}

		CollisionPair collisionPair = new CollisionPair(this.Type, otherCollider.Type);
		GameEvent gameEvent;
		bool isCausingEvent = Collider.collisionEventTable.TryGetValue(collisionPair, out gameEvent);

		if (isCausingEvent)
		{
			CollisionData collisionData;
			collisionData.self = this.gameObject;
			collisionData.other = other.gameObject;
			EventManager.TriggerEvent(gameEvent, collisionData);
		}
	}
}
