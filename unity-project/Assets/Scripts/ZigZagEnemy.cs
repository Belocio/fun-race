using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZagEnemy : MonoBehaviour
{
    private enum State
    {
        WaitingToMoveForward,
        MovingForward,
        WaitingToMoveBack,
        MovingBack
    }
    
    public float movementMagnitude = 10;
    public Vector3 movementDirection = Vector3.right;

    public float waitingSeconds = 5;
    public float movingSeconds = 2;

    private Vector3 startingPosition;
    private Vector3 finishingPosition;
    private State state;
    private float timeSinceLastStateChange;
    

    void Awake()
    {
        SetState(State.WaitingToMoveForward);
        startingPosition = transform.position;
        finishingPosition = startingPosition + (transform.rotation * movementDirection) * movementMagnitude;
    }
    void Start()
    {
        
    }
    
    void Update()
    {
        timeSinceLastStateChange += Time.deltaTime;

        switch (state)
        {
            case State.MovingForward:
            {
                float t = timeSinceLastStateChange / movingSeconds;
                transform.position = Vector3.Lerp(startingPosition, finishingPosition, Mathf.Clamp01(t));

                if (t >= 1.0)
                {
                    SetState(State.WaitingToMoveBack);
                }
                break;
            }
            case State.MovingBack:
            {
                float t = timeSinceLastStateChange / movingSeconds;
                transform.position = Vector3.Lerp(finishingPosition, startingPosition, Mathf.Clamp01(t));
                if (t >= 1.0)
                {
                    SetState(State.WaitingToMoveForward);
                }
                break;
            }
            case State.WaitingToMoveForward:
                if (timeSinceLastStateChange >= waitingSeconds)
                {
                    SetState(State.MovingForward);
                }
                break;
            case State.WaitingToMoveBack:
                if (timeSinceLastStateChange >= waitingSeconds)
                {
                    SetState(State.MovingBack);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void SetState(State newState)
    {
        state = newState;
        timeSinceLastStateChange = 0;
    }
}
