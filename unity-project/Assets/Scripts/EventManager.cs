using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventManager  {
    public delegate void EventListener<in T>(T eventData);

    Dictionary<System.Type, List<object> > eventHandlers = new Dictionary<System.Type, List<object>>();
    
    public void RegisterListener<T>(EventListener<T> eventDelegate)
    {
        List<object> handlers = null;
        this.eventHandlers.TryGetValue(typeof(T), out handlers);
        if (handlers != null)
        {
            handlers.Add(eventDelegate);
        }
        else
        {
            handlers = new List<object>();
            handlers.Add(eventDelegate);
            this.eventHandlers.Add(typeof(T), handlers);
        }
    }

    public void UnregisterListener<T>(EventListener<T> eventDelegate)
    {
        List<object> handlers = null;
        this.eventHandlers.TryGetValue(typeof(T), out handlers);
        if (handlers != null)
        {
            handlers.Remove(eventDelegate);
        }
    }

    public void TriggerEvent<T>(T eventData)
    {
        List<object> handlers = null;
        this.eventHandlers.TryGetValue(typeof(T), out handlers);
        if (handlers != null)
        {
            for(int i = 0; i < handlers.Count; ++i)
            {
                var eventDelegate = handlers[i] as EventListener<T>;
                if (eventDelegate != null) eventDelegate(eventData);
            }
        }
    }
}