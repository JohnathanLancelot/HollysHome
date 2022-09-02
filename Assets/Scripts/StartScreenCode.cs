using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class StartScreenCode : MonoBehaviour
{
    // VARIABLES:
    [SerializeField]
    public GameObject loadGame;

    [SerializeField]
    public GameObject gameControls;

    // The stage of nest creation (0 - 5):
    public int nestStage;

    // Start Function:
    void Start()
    {
        loadGame.SetActive(false);
        gameControls.SetActive(false);

        nestStage = 0;
        PlayerPrefs.SetInt("NestingStage", 0);
    }

    // New Game Button:
    public void newGameButton()
    {
        SceneManager.LoadScene(1);
    }

    // Load Game Button:
    public void loadGameButton()
    {
        loadGame.SetActive(true);
    }

    // Game Controls Button:
    public void gameControlsButton()
    {
        gameControls.SetActive(true);
    }

    // Exit Button:
    public void exitButton()
    {
        Debug.Log("EXIT");
        Application.Quit();
    }

    // Yes Button In Load Screen:
    public void yesLoadButton()
    {
        loadGame.SetActive(false);
        SceneManager.LoadScene(1);
    }

    // No Button In Load Screen:
    public void noLoadButton()
    {
        loadGame.SetActive(false);
    }

    // The 'go back' Button For The Controls Pop-Up:
    public void goBackFromControls()
    {
        gameControls.SetActive(false);
    }
}
