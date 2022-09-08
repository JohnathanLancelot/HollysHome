using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDScript : MonoBehaviour
{
    [SerializeField]
    Image hunger;

    [SerializeField]
    Image thirst;

    [SerializeField]
    Image day;

    [SerializeField]
    TextMeshProUGUI dayNumber;

    public float hungerAmount;
    public float thirstAmount;
    public float dayAmount;
    public string currentDay;
    public int currentDayInt;

    Mouse mouseScript;
    InGameScript inGameScript;

    [SerializeField]
    GameObject winningScreen;

    // Access the text on the winning screen to change the nesting percentage:
    [SerializeField]
    TextMeshProUGUI nestPercent;

    public bool winScreenTrigger;

    // Start is called before the first frame update
    void Start()
    {
        winningScreen.SetActive(false);
        winScreenTrigger = false;

        currentDay = dayNumber.text;

        mouseScript = FindObjectOfType<Mouse>();
        inGameScript = FindObjectOfType<InGameScript>();

        // Convert the day number text into an integer so we can use it
        // to call the next scene. Also send the day number to the in-game script
        // so it can be used to save the user's progress:
        if (currentDay == "Day 1")
        {
            currentDayInt = 1;
        }
        else if (currentDay == "Day 2")
        {
            currentDayInt = 2;
        }
        else if (currentDay == "Day 3")
        {
            currentDayInt = 3;
        }
        else if (currentDay == "Day 4")
        {
            currentDayInt = 4;
        }
        else if (currentDay == "Day 5")
        {
            currentDayInt = 5;
        }

        inGameScript.currentDayInt = currentDayInt;

        // Keep note of the current day, so it can be checked against
        // the 'first day' of a saved game:
        PlayerPrefs.SetInt("CurrentDay", currentDayInt);

        // Find out if there is a save file available:
        if ((PlayerPrefs.GetInt("SlotFilled")) == 1)
        {
            // Find out if a saved game is currently being loaded:
            if ((PlayerPrefs.GetInt("LoadedGame")) == 1)
            {
                // Find out if this is NOT the starting "day" for the loaded game:
                if ((PlayerPrefs.GetInt("CurrentDay")) != (PlayerPrefs.GetInt("DaySaved")))
                {
                    // If it isn't the starting day, reset the HUD values to 1:
                    PlayerPrefs.SetFloat("SavedHunger", 1);
                    PlayerPrefs.SetFloat("SavedThirst", 1);
                    PlayerPrefs.SetFloat("SavedDayAmount", 1);

                    // If it isn't the starting day, reset the paper booleans so the paper hasn't been
                    // picked up yet:
                    PlayerPrefs.SetInt("HasPaper", 0);
                    PlayerPrefs.SetInt("PaperPresent", 1);

                    // Send these values to the mouse script:
                    mouseScript.hasPaper = false;
                    mouseScript.paperPresent = true;
                }
                else
                {
                    // If this is the starting day, base the mouse's starting position
                    // on the saved floats:
                    mouseScript.transform.position = new Vector3((PlayerPrefs.GetFloat("XPosition")), (PlayerPrefs.GetFloat("YPosition")),
                        (PlayerPrefs.GetFloat("ZPosition")));

                    // Base the paper booleans on the saved data:
                    if (PlayerPrefs.GetInt("HasPaper") == 0)
                    {
                        mouseScript.hasPaper = false;
                    }
                    else if (PlayerPrefs.GetInt("HasPaper") == 1)
                    {
                        mouseScript.hasPaper = true;
                    }

                    if (PlayerPrefs.GetInt("paperPresent") == 0)
                    {
                        mouseScript.paperPresent = false;
                    }
                    else if (PlayerPrefs.GetInt("paperPresent") == 1)
                    {
                        mouseScript.paperPresent = true;
                    }
                }
            }
            else
            {
                // If this is a new game, send the reset paper booleans to the mouse script:
                mouseScript.hasPaper = false;
                mouseScript.paperPresent = true;
            }
        }
        else
        {
            // If there is no save slot, also send the reset paper booleans to the mouse script:
            mouseScript.hasPaper = false;
            mouseScript.paperPresent = true;
        }

        hungerAmount = PlayerPrefs.GetFloat("SavedHunger");
        thirstAmount = PlayerPrefs.GetFloat("SavedThirst");
        dayAmount = PlayerPrefs.GetFloat("SavedDayAmount");
    }

    // Update is called once per frame
    void Update()
    {
        // Making the hunger and thirst dials respond to hunger and thirst:
        hunger.fillAmount = hungerAmount;
        thirst.fillAmount = thirstAmount;

        // Pass these values to the in-game script for saving purposes:
        inGameScript.hungerLevel = hungerAmount;
        inGameScript.thirstLevel = thirstAmount;

        // Making hunger and thirst increase over time (shown as a dial decrease)
        // (Only if the menu isn't up, and only if the mouse isn't dead):
        if ((!mouseScript.isDead) && (!inGameScript.isMenuUp) && (!inGameScript.isWindowUp) && (!mouseScript.hasWon))
        {
            hungerAmount -= (0.1f * Time.deltaTime) / 2;
            thirstAmount -= (0.1f * Time.deltaTime) / 2;
        }

        // Make death possible via starvation or dehydration:
        if ((hungerAmount <= 0) || (thirstAmount <= 0))
        {
            mouseScript.isDead = true;
        }

        // Making the day amount go down over time, leading to a new day:
        day.fillAmount = dayAmount;

        // Pass this value to the in-game script for saving purposes:
        inGameScript.dayLevel = dayAmount;

        if ((!mouseScript.isDead) && (!inGameScript.isMenuUp) && (!inGameScript.isWindowUp) && (!mouseScript.hasWon))
        {
            dayAmount -= (0.1f * Time.deltaTime) / 3;
        }

        // Move to the next day if dayAmount goes down to 0:
        if (dayAmount <= 0)
        {
            if (currentDayInt == 1)
            {
                SceneManager.LoadScene(2);
            }
            else if (currentDayInt == 2)
            {
                SceneManager.LoadScene(3);
            }
            else if (currentDayInt == 3)
            {
                SceneManager.LoadScene(4);
            }
            else if (currentDayInt == 4)
            {
                SceneManager.LoadScene(5);
            }
            else if (currentDayInt == 5)
            {
                mouseScript.hasWon = true;

                winningScreen.SetActive(true);

                // Show how far the user got in building the nest:
                switch (PlayerPrefs.GetInt("NestingStage"))
                {
                    case 0:
                        nestPercent.text = "Nest 0% Complete.";
                        break;
                    case 1:
                        nestPercent.text = "Nest 20% Complete.";
                        break;
                    case 2:
                        nestPercent.text = "Nest 40% Complete.";
                        break;
                    case 3:
                        nestPercent.text = "Nest 60% Complete.";
                        break;
                    case 4:
                        nestPercent.text = "Nest 80% Complete.";
                        break;
                    case 5:
                        nestPercent.text = "Nest 100% Complete. Well done!";
                        break;
                }

                // Allow the user to exit:
                if (Input.GetKeyDown(KeyCode.X))
                {
                    Debug.Log("EXIT");
                    Application.Quit();
                }
            }
        }
    }
}
