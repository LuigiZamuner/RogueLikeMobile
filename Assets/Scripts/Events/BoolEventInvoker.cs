using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoolEventInvoker : IntEventInvoker
{
    protected Dictionary<EventName, UnityEvent<bool>> unityBoolEvents =
        new Dictionary<EventName, UnityEvent<bool>>();


    public void AddListener(EventName eventName, UnityAction<bool> listener)
    {
        // only add listeners for supported events
        if (unityBoolEvents.ContainsKey(eventName))
        {
            unityBoolEvents[eventName].AddListener(listener);
        }
    }
}
