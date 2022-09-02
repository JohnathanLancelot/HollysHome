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

        hungerAmount = 1;
        thirstAmount = 1;
        dayAmount = 1;
        currentDay = dayNumber.text;

        // Convert the day number text into an integer so we can use it
        // to call the next scene:
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

        mouseScript = FindObjectOfType<Mouse>();
        inGameScript = FindObjectOfType<InGameScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // Making the hunger and thirst dials respond to hunger and thirst:
        hunger.fillAmount = hungerAmount;
        thirst.fillAmount = thirstAmount;

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
