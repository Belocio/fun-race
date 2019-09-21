using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    
    public EventManager EventManager
    {
        get;
        private set;
    }

    public InputManager inputManager;

    public PlayerController player;
    public LevelPath levelPath;
    
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
    }

    void Start()
    {
        player.ResetPath(levelPath);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
