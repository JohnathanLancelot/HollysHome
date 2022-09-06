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
    AudioSource eatingSFX;

    [SerializeField]
    AudioSource drinkingSFX;

    [SerializeField]
    AudioSource mouseTrapSFX;

    [SerializeField]
    AudioSource paperSFX;

    [SerializeField]
    AudioSource nestingSFX;

    [SerializeField]
    Toggle muteMusic;

    [SerializeField]
    Toggle muteSFX;

    [SerializeField]
    Slider volumeSlider;

    public bool deadBodiesShown;

    public bool deathScreenTrigger = false;

    public bool isMenuUp;

    public bool isWindowUp;

    public bool soundEffectsMuted;

    public int currentDayInt;

    // A boolean for when toggles are changed by the code, not by the user
    // (e.g. to make settings consistent across days):
    bool backgroundToggleChange;

    HUDScript hud;

    // Start is called before the first frame update
    void Start()
    {
        hud = FindObjectOfType<HUDScript>();

        menuScreen.SetActive(false);
        controlsScreen.SetActive(false);
        optionsScreen.SetActive(false);
        deathScreen.SetActive(false);
        isMenuUp = false;
        isWindowUp = false;

        eatingSFX.Stop();
        drinkingSFX.Stop();
        mouseTrapSFX.Stop();
        paperSFX.Stop();
        nestingSFX.Stop();

        // If it is day 1, the player won't have touched the slider or toggles yet,
        // but if it is day 2 or onwards, we want to get these values from PlayerPrefs:
        if (hud.currentDayInt == 1)
        {
            volumeSlider.value = backgroundMusic.volume;
            eatingSFX.volume = volumeSlider.value;
            drinkingSFX.volume = volumeSlider.value;
            mouseTrapSFX.volume = volumeSlider.value;
            paperSFX.volume = volumeSlider.value;
            nestingSFX.volume = volumeSlider.value;
            soundEffectsMuted = false;
            deadBodiesShown = true;
            backgroundMusic.Play();
            backgroundToggleChange = false;

            // Save these to Player Prefs (1 = true, 0 = false for booleans) so that these default settings carry
            // across different days, if not changed in-game:
            PlayerPrefs.SetFloat("Volume", volumeSlider.value);
            PlayerPrefs.SetInt("SFX", 1);
            PlayerPrefs.SetInt("BackgroundMusic", 1);
            PlayerPrefs.SetInt("DeadBodies", 1);
        }
        else
        {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
            backgroundMusic.volume = volumeSlider.value;
            eatingSFX.volume = volumeSlider.value;
            drinkingSFX.volume = volumeSlider.value;
            mouseTrapSFX.volume = volumeSlider.value;
            paperSFX.volume = volumeSlider.value;
            nestingSFX.volume = volumeSlider.value;
            backgroundToggleChange = true;

            // See if the background music should be muted:
            if (PlayerPrefs.GetInt("BackgroundMusic") == 1)
            {
                backgroundMusic.Play();
                muteMusic.isOn = false;
            }
            else
            {
                backgroundMusic.Stop();
                muteMusic.isOn = true;
            }

            // See if the sound effects should be muted:
            if (PlayerPrefs.GetInt("SFX") == 1)
            {
                soundEffectsMuted = false;
                muteSFX.isOn = false;
            }
            else
            {
                soundEffectsMuted = true;
                muteSFX.isOn = true;
            }

            // See if no dead bodies mode should be on:
            if (PlayerPrefs.GetInt("DeadBodies") == 1)
            {
                deadBodiesShown = true;
                deadBodiesToggle.isOn = false;
            }
            else
            {
                deadBodiesShown = false;
                deadBodiesToggle.isOn = true;
            }
        }
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
        // Use the save slot to save the game:
        PlayerPrefs.SetInt("SlotFilled", 1);

        // Get the current date for use on the home screen's load menu:
        System.DateTime date = System.DateTime.Now;
        date = date.Add(System.TimeSpan.FromDays(0.00000000000001));
        string dateString = date.ToString("yyyy-MM-dd");

        // Save the date for later use:
        PlayerPrefs.SetString("LastSaved", dateString);

        // Save the day the user was in when they quit:
        PlayerPrefs.SetInt("DaySaved", currentDayInt);

        // Exit the program:
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
        // First check if it was a background change, and if so
        // change the boolean back to false and do nothing else:
        if (backgroundToggleChange)
        {
            backgroundToggleChange = false;
        }
        else
        {
            if (deadBodiesToggle.isOn)
            {
                deadBodiesShown = false;
                PlayerPrefs.SetInt("DeadBodies", 0);
            }
            else
            {
                deadBodiesShown = true;
                PlayerPrefs.SetInt("DeadBodies", 1);
            }
        }
    }

    // Toggle For Background Music:
    public void MuteBGM()
    {
        if (backgroundToggleChange)
        {
            backgroundToggleChange = false;
        }
        else
        {
            if (backgroundMusic.isPlaying)
            {
                backgroundMusic.Stop();
                PlayerPrefs.SetInt("BackgroundMusic", 0);
            }
            else
            {
                backgroundMusic.Play();
                PlayerPrefs.SetInt("BackgroundMusic", 1);
            }
        }
    }

    // Toggle For SFX:
    public void MuteSFX()
    {
        if (backgroundToggleChange)
        {
            backgroundToggleChange = false;
        }
        else
        {
            if (soundEffectsMuted)
            {
                soundEffectsMuted = false;
                eatingSFX.volume = volumeSlider.value;
                drinkingSFX.volume = volumeSlider.value;
                mouseTrapSFX.volume = volumeSlider.value;
                paperSFX.volume = volumeSlider.value;
                nestingSFX.volume = volumeSlider.value;
                PlayerPrefs.SetInt("SFX", 1);
            }
            else
            {
                soundEffectsMuted = true;
                eatingSFX.volume = 0;
                drinkingSFX.volume = 0;
                mouseTrapSFX.volume = 0;
                paperSFX.volume = 0;
                nestingSFX.volume = 0;
                PlayerPrefs.SetInt("SFX", 0);
            }
        }
    }

    // Volume Slider:
    public void VolumeChange()
    {
        backgroundMusic.volume = volumeSlider.value;

        // Save this value so that it stays the same across days:
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);

        if (!soundEffectsMuted)
        {
            eatingSFX.volume = volumeSlider.value;
            drinkingSFX.volume = volumeSlider.value;
            mouseTrapSFX.volume = volumeSlider.value;
            paperSFX.volume = volumeSlider.value;
            nestingSFX.volume = volumeSlider.value;
        }
    }
}
