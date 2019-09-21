using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum GameState
    {
        MainMenu,
        Playing,
        WiningScreen
    }
    public static GameManager instance = null;
    
    public EventManager EventManager
    {
        get;
        private set;
    }

    public InputManager inputManager;
    public UIManager UiManager;

    public PlayerController player;
    public LevelPath levelPath;

    private GameState gameState;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
        EventManager = new EventManager();

        inputManager.eventManager = EventManager;
        
        EventManager.RegisterListener<PlayerFinishEvent>(OnPlayerFinish);
        inputManager.RegisterTouchBegan(OnTouchBegan);
    }

    void Start()
    {
        gameState = GameState.MainMenu;
        UiManager.ShowMainMenu();
        player.ResetPath(levelPath);
    }

    void OnPlayerFinish(PlayerFinishEvent playerFinishEvent)
    {
        UiManager.ShowWinningScreen();
        gameState = GameState.WiningScreen;
    }

    void OnTouchBegan(TouchBeganEvent eventData)
    {
        switch (gameState)
        {
            case GameState.MainMenu:
                UiManager.HideMainMenu();
                player.StartRace();
                gameState = GameState.Playing;
                break;
            case GameState.Playing:
                break;
            case GameState.WiningScreen:
                UiManager.ShowMainMenu();
                gameState = GameState.MainMenu;
                player.ResetPath(levelPath);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
