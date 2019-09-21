using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenuScreen;
    public GameObject winningScreen;

    public void ShowMainMenu()
    {
        mainMenuScreen.SetActive(true);
        winningScreen.SetActive(false);
    }
    
    public void HideMainMenu()
    {
        mainMenuScreen.SetActive(false);
    }
    
    public void ShowWinningScreen()
    {
        winningScreen.SetActive(true);
    }
}
