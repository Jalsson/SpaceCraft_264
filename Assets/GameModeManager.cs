using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameModeManager : MonoBehaviour {

    #region instance
    public static GameModeManager instance;
        private void Awake()
    {
        instance = this;
        Time.timeScale = 0;

    }
    #endregion

    public GameObject[] tutorialObjects;
    public GameObject[] objectSpawners;
    public GameObject[] uiElements;
    public GameObject[] infoElements;
    public GameObject[] mainMenu;
    public GameObject[] gameMenu;
    public GameObject[] menuElements;
    public GameObject menu;

    public string[] uiInfo;
    public TextMeshProUGUI infoText;
    public bool gameStarted = false;
    bool continueTutorial;
    int indexOfElement;
    public int curGameModeIndex;
    public bool menuOn = false;



    public void TutorialGameMode()
    {
        foreach (var item in tutorialObjects)
        {
            item.SetActive(true);
        }
        indexOfElement = 0;

        StartCoroutine(TutorialText(uiElements[0], uiInfo[0]));
    }

    public void ContinueTutorial() {
        continueTutorial = true;
    }
    private void Update()
    {

        if (Input.GetButtonDown("Cancel"))
        {
            if (gameStarted)
            {
                if (!menuOn)
                {
                    menu.SetActive(true);
                    menuOn = true;
                    Time.timeScale = 0;
                    PlayerStats.instance.ShowMouse();
                }
                else if (menuOn)
                {
                    menu.SetActive(false);
                    foreach (var item in menuElements)
                    {
                        item.SetActive(false);
                    }
                    menuOn = false;
                    if(!PlayerStats.instance.paused)
                    {
                        Time.timeScale = 1;
                        PlayerStats.instance.HideMouse();
                    }
                }
            }
        }
        

    }

    public IEnumerator TutorialText(GameObject uiElementToShow, string stringToDisplay)
    {
        uiElementToShow.transform.SetAsLastSibling();
        infoText.text = stringToDisplay;
        while (!continueTutorial)
        {
            yield return null;
        }
        continueTutorial = false;
        indexOfElement++;
        uiElementToShow.transform.SetAsFirstSibling();
        if (uiElements.Length != indexOfElement)
        {
            StartCoroutine(TutorialText(uiElements[indexOfElement], uiInfo[indexOfElement]));
            if (indexOfElement == uiElements.Length-1)
            {
                infoElements[1].SetActive(false);
            }
            yield return null;
        }
        else
        {
            foreach (var item in infoElements)
            {
                item.SetActive(false);
                gameStarted = true;
            }
            yield return null;
        }
        
    }

    public IEnumerator UpdateInfoText(string textMessage)
    {
        infoText.text = textMessage;
        yield return new WaitForSecondsRealtime(5);
        infoText.text = "";
    }

    public void NormalGameMode()
    {
        foreach (var item in objectSpawners)
        {
            item.SetActive(true);
        }
        PlayerStats.instance.GetComponent<Rigidbody2D>().angularDrag = 0.8f;

    }

    public void StartGame()
    {
        switch (curGameModeIndex)
        {
            case 0:
                StartCoroutine(TutorialText(uiElements[0], uiInfo[0]));
                TutorialGameMode();
                foreach (var item in infoElements)
                {
                    item.SetActive(true);
                }
                foreach (var item in uiElements)
                {
                    item.SetActive(true);
                }
                Time.timeScale = 0;
                break;
            default:
                foreach (var item in objectSpawners)
                {
                    item.SetActive(true);
                    gameStarted = true;
                }
                foreach (var item in uiElements)
                {
                    item.SetActive(true);
                }
                foreach (var item in mainMenu)
                {
                    item.SetActive(false);
                }
                foreach (var item in gameMenu)
                {
                    item.SetActive(true);
                }
                PlayerStats.instance.HideMouse();
                Time.timeScale = 1;
                break;
        }
    }

}
