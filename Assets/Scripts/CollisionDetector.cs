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

public class CollisionDetector : MonoBehaviour
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

	private EventManager eventManager;

	public ColliderType Type { get; private set; }

	public void Initialize(ColliderType type, EventManager eventManager)
	{
		this.Type = type;
		this.eventManager = eventManager;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		CollisionDetector otherCollider = other.gameObject.GetComponent<CollisionDetector>();

		if (!otherCollider)
		{
			return;
		}

		CollisionPair collisionPair = new CollisionPair(this.Type, otherCollider.Type);
		GameEvent gameEvent;
		bool isCausingEvent = CollisionDetector.collisionEventTable.TryGetValue(collisionPair, out gameEvent);

		if (isCausingEvent)
		{
			CollisionData collisionData;
			collisionData.self = this.gameObject;
			collisionData.other = other.gameObject;
			this.eventManager.TriggerEvent(gameEvent, collisionData);
		}
	}
}
