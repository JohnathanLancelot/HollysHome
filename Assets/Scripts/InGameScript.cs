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

    [SerializeField]
    GameObject deathScreen;

    [SerializeField]
    Toggle deadBodiesToggle;

    [SerializeField]
    AudioSource backgroundMusic;

    [SerializeField]
    Toggle muteMusic;

    [SerializeField]
    Slider volumeSlider;

    public bool deadBodiesShown = true;

    public bool deathScreenTrigger = false;

    public bool isMenuUp;

    public bool isWindowUp;

    // Start is called before the first frame update
    void Start()
    {
        menuScreen.SetActive(false);
        controlsScreen.SetActive(false);
        optionsScreen.SetActive(false);
        deathScreen.SetActive(false);
        isMenuUp = false;
        isWindowUp = false;

        volumeSlider.value = backgroundMusic.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (deathScreenTrigger == true)
        {
            deathScreen.SetActive(true);

            // Allow the user to exit:
            if (Input.GetKeyDown(KeyCode.X))
            {
                Debug.Log("EXIT");
                Application.Quit();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                if ((isMenuUp == false) && (isWindowUp == false))
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
    }

    // In-Game Menu's Game Options Button:
    public void visitGameOptions()
    {
        menuScreen.SetActive(false);
        isMenuUp = false;
        optionsScreen.SetActive(true);
        isWindowUp = true;
    }

    // In-Game Menu's Game Controls Button:
    public void visitGameControls()
    {
        menuScreen.SetActive(false);
        isMenuUp = false;
        controlsScreen.SetActive(true);
        isWindowUp = true;
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
        isWindowUp = false;
    }

    // In-Game Menu's Controls Screen's Back Button:
    public void backFromControls()
    {
        controlsScreen.SetActive(false);
        menuScreen.SetActive(true);
        isMenuUp = true;
        isWindowUp = false;
    }

    // In-Game Menu's Options Screen's Back Button:
    public void backFromOptions()
    {
        optionsScreen.SetActive(false);
        menuScreen.SetActive(true);
        isMenuUp = true;
        isWindowUp = false;
    }

    // Toggle For The 'No Dead Bodies' Option:
    public void noDeadBodiesOption()
    {
        if (deadBodiesToggle.isOn)
        {
            deadBodiesShown = false;
        }
        else
        {
            deadBodiesShown = true;
        }
    }

    // Toggle For Background Music:
    public void MuteBGM()
    {
        if (backgroundMusic.isPlaying)
        {
            backgroundMusic.Stop();
        }
        else
        {
            backgroundMusic.Play();
        }
    }

    // Volume Slider:
    public void VolumeChange()
    {
        backgroundMusic.volume = volumeSlider.value;
    }
}
