using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMenu : MonoBehaviour {

    public int numberOfGameModes;
    public TextMeshProUGUI gameModeText;
    GameModeManager gameModeManager;
    int curGameMode;

    private void Start()
    {
        gameModeManager = GameModeManager.instance;
    }
    public void Quit()
    {
        Application.Quit();
        
    }

    public void ChangeGameModeRight()
    {
        curGameMode = curGameMode == numberOfGameModes ? curGameMode = 0 : curGameMode += 1;
        gameModeManager.curGameModeIndex = curGameMode;
    }
    public void ChangeGameModeLeft()
    {
        curGameMode = curGameMode == 0 ? curGameMode = numberOfGameModes : curGameMode -= 1;
        gameModeManager.curGameModeIndex = curGameMode;
    }

    public void ChangeText()
    {
        switch (curGameMode)
        {
            case 0:
                gameModeText.text = "mode: tutorial";
                break;
            case 1:
                gameModeText.text = "mode: normal";
                break;
        }
    }
  
}
