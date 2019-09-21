using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public struct TouchBeganEvent
{
    public Vector2 position;
}

public struct TouchEndEvent
{
    public Vector2 position;
}

public class InputManager : MonoBehaviour
{
    public EventManager eventManager;

    private bool isMouseDown = false;

    void Update()
    {
        if (isMouseDown)
        {
            if (Input.GetMouseButtonUp(0))
            {
                eventManager.TriggerEvent(new TouchEndEvent{position = Input.mousePosition});
                isMouseDown = false;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                isMouseDown = true;
                eventManager.TriggerEvent(new TouchBeganEvent {position = Input.mousePosition});
            }
        }
    }

    public void RegisterTouchBegan(EventManager.EventListener<TouchBeganEvent> listener)
    {
        eventManager.RegisterListener(listener);
    }
    
    public void RegisterTouchEnd(EventManager.EventListener<TouchEndEvent> listener)
    {
        eventManager.RegisterListener(listener);
    }

    public void UnregisterTouchBegan(EventManager.EventListener<TouchBeganEvent> listener)
    {
        eventManager.UnregisterListener(listener);
    }
    
    public void UnregisterTouchEnd(EventManager.EventListener<TouchEndEvent> listener)
    {
        eventManager.UnregisterListener(listener);
    }
}
