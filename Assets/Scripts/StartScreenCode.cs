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

    Mouse mouseScript;

    // Start Function:
    void Start()
    {
        loadGame.SetActive(false);
        gameControls.SetActive(false);

        mouseScript = FindObjectOfType<Mouse>();
    }

    // New Game Button:
    public void newGameButton()
    {
        // If a new game is started, reset the hunger, thirst
        // and day levels:
        PlayerPrefs.SetFloat("SavedHunger", 1);
        PlayerPrefs.SetFloat("SavedThirst", 1);
        PlayerPrefs.SetFloat("SavedDayAmount", 1);

        // Also reset the nesting stage back to 0:
        PlayerPrefs.SetInt("NestingStage", 0);

        // Tell the mouse script that a previously-saved game
        // is not being used:
        PlayerPrefs.SetInt("LoadedGame", 0);

        // Reset the paper booleans so the paper hasn't been
        // picked up yet:
        PlayerPrefs.SetInt("HasPaper", 0);
        PlayerPrefs.SetInt("PaperPresent", 1);

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

            // Set the saved "boolean" for whether we are using a loaded game to true:
            PlayerPrefs.SetInt("LoadedGame", 1);

            // Based on what day the player was in when they last saved,
            // load the appropriate day and save it as the "starting day":
            switch (PlayerPrefs.GetInt("DaySaved"))
            {
                case 1:
                    SceneManager.LoadScene(1);
                    PlayerPrefs.SetInt("StartingDay", 1);
                    break;
                case 2:
                    SceneManager.LoadScene(2);
                    PlayerPrefs.SetInt("StartingDay", 2);
                    break;
                case 3:
                    SceneManager.LoadScene(3);
                    PlayerPrefs.SetInt("StartingDay", 3);
                    break;
                case 4:
                    SceneManager.LoadScene(4);
                    PlayerPrefs.SetInt("StartingDay", 4);
                    break;
                case 5:
                    SceneManager.LoadScene(5);
                    PlayerPrefs.SetInt("StartingDay", 5);
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
