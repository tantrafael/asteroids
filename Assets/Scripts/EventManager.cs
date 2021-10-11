using UnityEngine.Events;
using System.Collections.Generic;

public enum GameEvent
{
	AsteroidHitByPlayerShot,
	EnemyShipHitByPlayerShot,
	PlayerShipHitByAsteroid,
	PlayerShipHitByEnemyShot,
	PlayerShipHitByEnemyShip
};

[System.Serializable]
public class TypedEvent : UnityEvent<object> {}

public class EventManager
{
	private Dictionary<GameEvent, UnityEvent> eventDictionary;
	private Dictionary<GameEvent, TypedEvent> typedEventDictionary;

	public EventManager()
	{
		this.eventDictionary = new Dictionary<GameEvent, UnityEvent>();
		this.typedEventDictionary = new Dictionary<GameEvent, TypedEvent>();
	}

	public void StartListening(GameEvent gameEvent, UnityAction listener)
	{
		UnityEvent thisEvent = null;

		if (this.eventDictionary.TryGetValue(gameEvent, out thisEvent))
		{
			thisEvent.AddListener(listener);
		}
		else
		{
			thisEvent = new UnityEvent();
			thisEvent.AddListener(listener);
			this.eventDictionary.Add(gameEvent, thisEvent);
		}
	}

	public void StopListening(GameEvent gameEvent, UnityAction listener)
	{
		UnityEvent thisEvent = null;

		if (this.eventDictionary.TryGetValue(gameEvent, out thisEvent))
		{
			thisEvent.RemoveListener(listener);
		}
	}

	public void TriggerEvent(GameEvent gameEvent)
	{
		UnityEvent thisEvent = null;

		if (this.eventDictionary.TryGetValue(gameEvent, out thisEvent))
		{
			thisEvent.Invoke();
		}
	}

	public void StartListening(GameEvent gameEvent, UnityAction<object> listener)
	{
		TypedEvent thisEvent = null;

		if (this.typedEventDictionary.TryGetValue(gameEvent, out thisEvent))
		{
			thisEvent.AddListener(listener);
		}
		else
		{
			thisEvent = new TypedEvent();
			thisEvent.AddListener(listener);
			this.typedEventDictionary.Add(gameEvent, thisEvent);
		}
	}

	public void StopListening(GameEvent gameEvent, UnityAction<object> listener)
	{
		TypedEvent thisEvent = null;

		if (this.typedEventDictionary.TryGetValue(gameEvent, out thisEvent))
		{
			thisEvent.RemoveListener(listener);
		}
	}

	public void TriggerEvent(GameEvent gameEvent, object data)
	{
		TypedEvent thisEvent = null;

		if (this.typedEventDictionary.TryGetValue(gameEvent, out thisEvent))
		{
			thisEvent.Invoke(data);
		}
	}
}