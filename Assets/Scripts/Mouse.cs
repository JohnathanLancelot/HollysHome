using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public InGameScript inGameScript;

    public HUDScript hudScript;

    public GameObject mouseTrapAnim;

    public AudioSource mouseTrapAudio;

    [SerializeField]
    public AudioSource eatingSFX;

    [SerializeField]
    public AudioSource drinkingSFX;

    [SerializeField]
    public AudioSource paperPickUpSFX;

    [SerializeField]
    public AudioSource nestingSFX;

    [SerializeField]
    public GameObject riggedMouse;

    [SerializeField]
    public GameObject paper;

    [SerializeField]
    public MeshRenderer transparentMaterial;

    [SerializeField]
    public MeshRenderer paperMaterial;

    // Accessing materials for all 5 nest pieces:
    [SerializeField]
    private MeshRenderer nestMat1;

    [SerializeField]
    private MeshRenderer nestMat2;

    [SerializeField]
    private MeshRenderer nestMat3;

    [SerializeField]
    private MeshRenderer nestMat4;

    [SerializeField]
    private MeshRenderer nestMat5;

    // Reference Material (Pillow):
    [SerializeField]
    private MeshRenderer pillowMat;

    private Animator mouse;

    CharacterController mouseController;

    public float moveSpeed = 1.3f;

    public float rotationSpeed = 720;

    public Vector3 direction;

    // Boolean determining if the mouse should be dead:
    public bool isDead = false;

    // Boolean for if the win condition has been met:
    public bool hasWon = false;

    // Boolean showing if the mouse is carrying paper:
    public bool hasPaper;

    // Boolean showing if the paper is present in the environment:
    public bool paperPresent;

    float moveX;

    float moveZ;

    // Start is called before the first frame update
    void Start()
    {
        mouse = GetComponent<Animator>();
        mouseController = GetComponent<CharacterController>();
        inGameScript = FindObjectOfType<InGameScript>();
        hudScript = FindObjectOfType<HUDScript>();
        paper = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check what colour the paper should be (clear or white),
        // and make it the correct colour:
        if (paperPresent == true)
        {
            // Make it visible:
            paperMaterial.material = pillowMat.material;
        }
        else
        {
            // Make it invisible:
            paperMaterial.material = transparentMaterial.material;
        }

        moveZ = Input.GetAxis("Horizontal") * -1;
        moveX = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(moveX, 0, moveZ);

        // Might make things less jittery:
        moveDir.Normalize();

        if ((!isDead) && (!inGameScript.isMenuUp) && (!inGameScript.isWindowUp) && (!hasWon))
        {
            // Actually moves the mouse:
            transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);
            //mouseController.Move(moveDir * moveSpeed * Time.deltaTime);

            // Send the mouse's position to the in-game script for saving:
            inGameScript.mouseX = transform.position.x;
            inGameScript.mouseY = transform.position.y;
            inGameScript.mouseZ = transform.position.z;
        }

        if ((inGameScript.isMenuUp) || (inGameScript.isWindowUp) || (hasWon))
        {
            mouse.SetBool("run", false);
            mouse.SetBool("jump", false);
            mouse.SetBool("idle", true);
        }

        if ((isDead == false) & (hasWon == false))
        {
            if ((!inGameScript.isMenuUp) && (!inGameScript.isWindowUp))
            {
                // If the mouse is moving:
                if (moveDir != Vector3.zero)
                {
                    // Points the mouse in the right direction:
                    transform.forward = moveDir;

                    // If space is not being pressed:
                    if (!Input.GetKeyDown(KeyCode.Space))
                    {
                        // Running animation:
                        mouse.SetBool("run", true);
                        mouse.SetBool("idle", false);
                        mouse.SetBool("jump", false);
                    }
                    // If space is being pressed:
                    else
                    {
                        // Jumping animation
                        mouse.SetBool("jump", true);
                        mouse.SetBool("idle", false);
                        mouse.SetBool("run", false);
                    }
                }
                // If the mouse is not moving:
                else
                {
                    if (!Input.GetKeyDown(KeyCode.Space))
                    {
                        mouse.SetBool("idle", true);
                        mouse.SetBool("run", false);
                        mouse.SetBool("jump", false);
                    }
                    else
                    {
                        mouse.SetBool("jump", true);
                        mouse.SetBool("idle", false);
                        mouse.SetBool("run", false);
                    }
                }
            }
        }
        else if (isDead == true)
        {
            // Check if 'no dead bodies mode' is enabled:
            if (inGameScript.deadBodiesShown)
            {
                mouse.SetBool("jump", false);
                mouse.SetBool("idle", false);
                mouse.SetBool("run", false);
                mouse.SetBool("death", true);
            }
            else
            {
                // Make the mouse disappear:
                riggedMouse.SetActive(false);
            }

            // Make the death screen appear:
            inGameScript.deathScreenTrigger = true;
        }

        // Depending on the stage of nest development, determine which parts of the nest
        // should be visible:
        switch (PlayerPrefs.GetInt("NestingStage"))
        {
            case 1:
                nestMat1.material = pillowMat.material;
                break;
            case 2:
                nestMat1.material = pillowMat.material;
                nestMat2.material = pillowMat.material;
                break;
            case 3:
                nestMat1.material = pillowMat.material;
                nestMat2.material = pillowMat.material;
                nestMat3.material = pillowMat.material;
                break;
            case 4:
                nestMat1.material = pillowMat.material;
                nestMat2.material = pillowMat.material;
                nestMat3.material = pillowMat.material;
                nestMat4.material = pillowMat.material;
                break;
            case 5:
                nestMat1.material = pillowMat.material;
                nestMat2.material = pillowMat.material;
                nestMat3.material = pillowMat.material;
                nestMat4.material = pillowMat.material;
                nestMat5.material = pillowMat.material;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "trap")
        {
            mouseTrapAnim.GetComponent<Animation>().Play();
            isDead = true;

            if (!inGameScript.soundEffectsMuted)
            {
                mouseTrapAudio.Play();
            }
        }
        else if (other.gameObject.tag == "water")
        {
            // Reset thirst to 1:
            hudScript.thirstAmount = 1;

            if (!inGameScript.soundEffectsMuted)
            {
                drinkingSFX.Play();
            }
        }
        else if (other.gameObject.tag == "food")
        {
            // Reset hunger to 1:
            hudScript.hungerAmount = 1;

            if (!inGameScript.soundEffectsMuted)
            {
                eatingSFX.Play();
            }
        }
        else if (other.gameObject.tag == "paper")
        {
            if (paperPresent)
            {
                // Change hasPaper to true and paperPresent to false:
                hasPaper = true;
                paperPresent = false;

                // Save these values:
                PlayerPrefs.SetInt("HasPaper", 1);
                PlayerPrefs.SetInt("PaperPresent", 0);

                // Play the paper SFX:
                if (!inGameScript.soundEffectsMuted)
                {
                    paperPickUpSFX.Play();
                }

                // Make the paper disappear:
                paperMaterial.material = transparentMaterial.material;
            }
        }
        else if (other.gameObject.tag == "nest")
        {
            if (hasPaper)
            {
                // Change hasPaper to false:
                hasPaper = false;

                // Save this value:
                PlayerPrefs.SetInt("HasPaper", 0);

                // Play the nesting SFX:
                if (!inGameScript.soundEffectsMuted)
                {
                    nestingSFX.Play();
                }

                // Increase the stage of nest development:
                PlayerPrefs.SetInt("NestingStage", (PlayerPrefs.GetInt("NestingStage") + 1));

                // Depending on the stage of nest development, determine which new part of the nest
                // to make visible:
                switch (PlayerPrefs.GetInt("NestingStage"))
                {
                    case 1:
                        nestMat1.material = pillowMat.material;
                        break;
                    case 2:
                        nestMat2.material = pillowMat.material;
                        break;
                    case 3:
                        nestMat3.material = pillowMat.material;
                        break;
                    case 4:
                        nestMat4.material = pillowMat.material;
                        break;
                    case 5:
                        nestMat5.material = pillowMat.material;
                        break;
                }
            }
        }
    }
}
