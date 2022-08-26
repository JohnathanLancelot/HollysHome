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
    public GameObject gameOptions;

    [SerializeField]
    public GameObject gameControls;

    [SerializeField]
    Toggle startNDBMToggle;

    public bool startDeadBodiesShown;

    // Start Function:
    void Start()
    {
        loadGame.SetActive(false);
        gameOptions.SetActive(false);
        gameControls.SetActive(false);
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

    // Game Options Button:
    public void gameOptionsButton()
    {
        gameOptions.SetActive(true);
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

    // The 'go back' Button For The Settings Pop-Up:
    public void goBackFromSettings()
    {
        gameOptions.SetActive(false);
    }

    // The 'go back' Button For The Controls Pop-Up:
    public void goBackFromControls()
    {
        gameControls.SetActive(false);
    }

    // Toggle For The 'No Dead Bodies' Option:
    public void noDeadBodiesOption()
    {
        if (startNDBMToggle.isOn)
        {
            startDeadBodiesShown = false;
        }
        else
        {
            startDeadBodiesShown = true;
        }
    }
}
