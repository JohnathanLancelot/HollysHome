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

    [SerializeField]
    public TextMeshProUGUI lastSaved;

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
        // If a new game is started, reset the hunger, thirst
        // and day levels:
        PlayerPrefs.SetFloat("SavedHunger", 1);
        PlayerPrefs.SetFloat("SavedThirst", 1);
        PlayerPrefs.SetFloat("SavedDayAmount", 1);

        SceneManager.LoadScene(1);
    }

    // Load Game Button:
    public void loadGameButton()
    {
        loadGame.SetActive(true);

        // If there is a save file ready:
        if (PlayerPrefs.GetInt("SlotFilled") == 1)
        {
            // Tell the user when the game was last saved:
            lastSaved.text = PlayerPrefs.GetString("LastSaved");
        }
        else
        {
            lastSaved.text = "No saved game on file.";
        }
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
        // If there is a save file ready:
        if (PlayerPrefs.GetInt("SlotFilled") == 1)
        {
            loadGame.SetActive(false);

            // Based on what day the player was in when they last saved,
            // load the appropriate day:
            switch (PlayerPrefs.GetInt("DaySaved"))
            {
                case 1:
                    SceneManager.LoadScene(1);
                    break;
                case 2:
                    SceneManager.LoadScene(2);
                    break;
                case 3:
                    SceneManager.LoadScene(3);
                    break;
                case 4:
                    SceneManager.LoadScene(4);
                    break;
                case 5:
                    SceneManager.LoadScene(5);
                    break;
            }
        }
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
