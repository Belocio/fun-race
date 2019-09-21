using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


struct PlayerFinishEvent
{
    public PlayerController player;
}
public class PlayerController : MonoBehaviour
{
    private enum State
    {
        WaitingForPath,
        Stopped,
        Running,
        Flying,
        Finished
    }
    
    public float speed = 1.0f;

    private LevelPath levelPath;
    private Rigidbody playerRigidbody;
    
    private float currentPositionInPath;
    private Vector3 playerHeight = new Vector3(0, 1.0f, 0);

    private State playerState;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerState = State.WaitingForPath;
    }
    
    void Start()
    {
        GameManager.instance.inputManager.RegisterTouchBegan(OnTouchBegan);
        GameManager.instance.inputManager.RegisterTouchEnd(OnTouchEnd);
    }

    public void ResetPath(LevelPath newPath)
    {
        levelPath = newPath;

        currentPositionInPath = 0;
        playerRigidbody.Sleep();
        transform.position = levelPath.GetWorldPosition(currentPositionInPath) + playerHeight;
        transform.rotation = Quaternion.identity;
        playerRigidbody.velocity = Vector3.zero;
        playerRigidbody.angularVelocity = Vector3.zero;
        playerRigidbody.WakeUp();
        playerState = State.Stopped;
    }
    
    void Update()
    {
        if (playerState == State.Running)
        {
            currentPositionInPath += Time.deltaTime * speed;
            transform.position = levelPath.GetWorldPosition(currentPositionInPath) + playerHeight;

            if (currentPositionInPath >= levelPath.TotalDistance)
            {
                GameManager.instance.EventManager.TriggerEvent(new PlayerFinishEvent{ player = this});
                playerState = State.Finished;
            }
        }
        else if (playerState == State.Stopped)
        {
            transform.position = transform.position;
        }
    }

    private void OnTouchBegan(TouchBeganEvent touchBeganData)
    {
        Run();
    }

    private void OnTouchEnd(TouchEndEvent touchBeganData)
    {
        Stop();
    }

    private void Run()
    {
        if (playerState == State.Stopped)
        {
            playerState = State.Running;
        }
    }

    private void Stop()
    {
        if (playerState == State.Running)
        {
            playerState = State.Stopped;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            playerState = State.Flying;
        }
    }
}
