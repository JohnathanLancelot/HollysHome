using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{
    [SerializeField]
    Image hunger;

    [SerializeField]
    Image thirst;

    public float hungerAmount;
    public float thirstAmount;

    Mouse mouseScript;
    InGameScript inGameScript;

    // Start is called before the first frame update
    void Start()
    {
        hungerAmount = 1;
        thirstAmount = 1;

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
        if ((!mouseScript.isDead) && (!inGameScript.isMenuUp) && (!inGameScript.isWindowUp))
        {
            hungerAmount -= (0.1f * Time.deltaTime) / 2;
            thirstAmount -= (0.1f * Time.deltaTime) / 2;
        }

        // Make death possible via starvation or dehydration:
        if ((hungerAmount <= 0) || (thirstAmount <= 0))
        {
            mouseScript.isDead = true;
        }
    }
}
