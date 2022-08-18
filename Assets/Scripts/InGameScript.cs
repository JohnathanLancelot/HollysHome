using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class InGameScript : MonoBehaviour
{
    // MORE VARIABLES:
    [SerializeField]
    GameObject menuScreen;

    [SerializeField]
    GameObject controlsScreen;

    [SerializeField]
    GameObject optionsScreen;

    bool isMenuUp;

    // Start is called before the first frame update
    void Start()
    {
        menuScreen.SetActive(false);
        controlsScreen.SetActive(false);
        optionsScreen.SetActive(false);
        isMenuUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (isMenuUp == false)
            {
                menuScreen.SetActive(true);
                isMenuUp = true;
            }
            else
            {
                menuScreen.SetActive(false);
                isMenuUp = false;
            }
        }
    }

    // In-Game Menu's Game Options Button:
    public void visitGameOptions()
    {
        menuScreen.SetActive(false);
        isMenuUp = false;
        optionsScreen.SetActive(true);
    }

    // In-Game Menu's Game Controls Button:
    public void visitGameControls()
    {
        menuScreen.SetActive(false);
        isMenuUp = false;
        controlsScreen.SetActive(true);
    }

    // In-Game Menu's Exit Button:
    public void exitGame()
    {
        Debug.Log("EXIT");
        Application.Quit();
    }

    // In-Game Menu's Back Button:
    public void backFromMenu()
    {
        menuScreen.SetActive(false);
        isMenuUp = false;
    }

    // In-Game Menu's Controls Screen's Back Button:
    public void backFromControls()
    {
        controlsScreen.SetActive(false);
        menuScreen.SetActive(true);
        isMenuUp = true;
    }

    // In-Game Menu's Options Screen's Back Button:
    public void backFromOptions()
    {
        optionsScreen.SetActive(false);
        menuScreen.SetActive(true);
        isMenuUp = true;
    }
}
