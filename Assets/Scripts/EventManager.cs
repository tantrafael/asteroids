using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public enum GameEvent
{
	AsteroidHitByPlayerShot,
	EnemyShipHitByPlayerShot,
	PlayerShipHitByAsteroid,
	PlayerShipHitByEnemyShot,
	PlayerShipHitByEnemyShip,
	EnemyShipOutOfScope
};

[System.Serializable]
public class TypedEvent : UnityEvent<object> {}

public class EventManager : MonoBehaviour
{
	private Dictionary<GameEvent, UnityEvent> eventDictionary;
	private Dictionary<GameEvent, TypedEvent> typedEventDictionary;

	/*
	private static EventManager eventManager;

	public static EventManager instance
	{
		get
		{
			if (!eventManager)
			{
				eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

				if (!eventManager)
				{
					Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
				}
				else
				{
					eventManager.Init();
				}
			}

			return eventManager;
		}
	}

	void Init()
	{
		if (eventDictionary == null)
		{
			eventDictionary = new Dictionary<GameEvent, UnityEvent>();
			typedEventDictionary = new Dictionary<GameEvent, TypedEvent>();
		}
	}
	*/

	/*
	public static void StartListening(GameEvent gameEvent, UnityAction listener)
	{
		UnityEvent thisEvent = null;

		if (instance.eventDictionary.TryGetValue(gameEvent, out thisEvent))
		{
			thisEvent.AddListener(listener);
		}
		else
		{
			thisEvent = new UnityEvent();
			thisEvent.AddListener(listener);
			instance.eventDictionary.Add(gameEvent, thisEvent);
			this.eventDictionary.Add(gameEvent, thisEvent);
		}
	}
	*/

	private void Awake()
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

	/*
	public static void StopListening(GameEvent gameEvent, UnityAction listener)
	{
		if (eventManager == null)
		{
			return;
		}

		UnityEvent thisEvent = null;

		if (instance.eventDictionary.TryGetValue(gameEvent, out thisEvent))
		{
			thisEvent.RemoveListener(listener);
		}
	}
	*/

	public void StopListening(GameEvent gameEvent, UnityAction listener)
	{
		UnityEvent thisEvent = null;

		if (this.eventDictionary.TryGetValue(gameEvent, out thisEvent))
		{
			thisEvent.RemoveListener(listener);
		}
	}

	/*
	public static void TriggerEvent(GameEvent gameEvent)
	{
		UnityEvent thisEvent = null;

		if (instance.eventDictionary.TryGetValue(gameEvent, out thisEvent))
		{
			thisEvent.Invoke();
		}
	}
	*/

	public void TriggerEvent(GameEvent gameEvent)
	{
		UnityEvent thisEvent = null;

		if (this.eventDictionary.TryGetValue(gameEvent, out thisEvent))
		{
			thisEvent.Invoke();
		}
	}

	/*
	public static void StartListening(GameEvent gameEvent, UnityAction<object> listener)
	{
		TypedEvent thisEvent = null;

		if (instance.typedEventDictionary.TryGetValue(gameEvent, out thisEvent))
		{
			thisEvent.AddListener(listener);
		}
		else
		{
			thisEvent = new TypedEvent();
			thisEvent.AddListener(listener);
			instance.typedEventDictionary.Add(gameEvent, thisEvent);
		}
	}
	*/

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

	/*
	public static void StopListening(GameEvent gameEvent, UnityAction<object> listener)
	{
		if (eventManager == null) return;

		TypedEvent thisEvent = null;
		if (instance.typedEventDictionary.TryGetValue(gameEvent, out thisEvent))
		{
			thisEvent.RemoveListener(listener);
		}
	}
	*/

	public void StopListening(GameEvent gameEvent, UnityAction<object> listener)
	{
		TypedEvent thisEvent = null;

		if (this.typedEventDictionary.TryGetValue(gameEvent, out thisEvent))
		{
			thisEvent.RemoveListener(listener);
		}
	}

	/*
	public static void TriggerEvent(GameEvent gameEvent, object data)
	{
		TypedEvent thisEvent = null;

		if (instance.typedEventDictionary.TryGetValue(gameEvent, out thisEvent))
		{
			thisEvent.Invoke(data);
		}
	}
	*/

	public void TriggerEvent(GameEvent gameEvent, object data)
	{
		TypedEvent thisEvent = null;

		if (this.typedEventDictionary.TryGetValue(gameEvent, out thisEvent))
		{
			thisEvent.Invoke(data);
		}
	}
}