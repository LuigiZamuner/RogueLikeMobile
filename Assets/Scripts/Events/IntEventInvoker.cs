using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Extends MonoBehaviour to support invoking 
/// one integer argument UnityEvents
/// </summary>
public class IntEventInvoker : MonoBehaviour
{
	protected Dictionary<EventName, UnityEvent<int>> unityIntEvents =
		new Dictionary<EventName, UnityEvent<int>>();

	/// <summary>
	/// Adds the given listener for the given event name
	/// </summary>
	/// <param name="eventName">event name</param>
	/// <param name="listener">listener</param>
	public void AddListener(EventName eventName, UnityAction<int> listener)
    {
		// only add listeners for supported events
		if (unityIntEvents.ContainsKey(eventName))
        {
			unityIntEvents[eventName].AddListener(listener);
		}
	}
}
