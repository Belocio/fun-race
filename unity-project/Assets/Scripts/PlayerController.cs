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
        WaitingToStart,
        Stopped,
        Running,
        Flying,
        Finished
    }
    
    public float speed = 1.0f;
    public GameObject playerSkin;

    private LevelPath levelPath;

    private float currentPositionInPath;
    private Vector3 playerHeight = new Vector3(0, 1.0f, 0);
    private Animator animator;

    private State playerState;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerState = State.WaitingForPath;
    }
    
    void Start()
    {
        GameManager.instance.inputManager.RegisterTouchBegan(OnTouchBegan);
        GameManager.instance.inputManager.RegisterTouchEnd(OnTouchEnd);
    }

    public void StartRace()
    {
        playerState = State.Stopped;
        animator.enabled = false;
        SetInStartingPosition();
    }
    
    public void ResetPath(LevelPath newPath)
    {
        levelPath = newPath;

        animator.enabled = false;
        SetInStartingPosition();

        playerState = State.WaitingToStart;
    }
    
    void Update()
    {
        if (playerState == State.Running)
        {
            currentPositionInPath += Time.deltaTime * speed;
            levelPath.UpdateTransform(currentPositionInPath, playerHeight, transform);

            if (currentPositionInPath >= levelPath.TotalDistance)
            {
                Win();
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

    private void OnTriggerEnter(Collider other)
    {
        if (playerState == State.Running || playerState == State.Stopped)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemies"))
            {
                playerState = State.Flying;
                animator.enabled = true;
                animator.Play("PlayerDie");
            }
        }
    }

    private void SetInStartingPosition()
    {
        currentPositionInPath = 0;

        playerSkin.transform.localRotation = Quaternion.identity;
        playerSkin.transform.localPosition = Vector3.zero;
        levelPath.UpdateTransform(currentPositionInPath, playerHeight, transform);
    }

    public void OnPlayerDied()
    {
        GetComponent<Animator>().enabled = false;
        StartRace();
    }

    private void Win()
    {
        GameManager.instance.EventManager.TriggerEvent(new PlayerFinishEvent{ player = this});
        animator.enabled = true;
        animator.Play("PlayerWin");
        playerState = State.Finished;
    }
}
