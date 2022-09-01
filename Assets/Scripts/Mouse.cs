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

    // Boolean showing if the mouse is carrying paper:
    public bool hasPaper = false;

    // Boolean showing if the paper is present in the environment:
    public bool paperPresent = true;

    // The stage of nest creation (0 - 5):
    public int nestStage;

    //[SerializeField]
    //Transform mouseTransform;

    float moveX;
    float moveZ;

    // Jump Variables:
    //[SerializeField]
    //bool isGrounded;

    //[SerializeField]
    //float groundCheckDistance;

    //[SerializeField]
    //LayerMask groundLayerMask;

    //[SerializeField]
    //float gravity = -0.2f;

    //[SerializeField]
    //float jumpHeight = 0.03f;

    //Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        mouse = GetComponent<Animator>();
        mouseController = GetComponent<CharacterController>();
        inGameScript = FindObjectOfType<InGameScript>();
        hudScript = FindObjectOfType<HUDScript>();
        paper = GetComponent<GameObject>();
        nestStage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        moveZ = Input.GetAxis("Horizontal") * -1;
        moveX = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(moveX, 0, moveZ);

        // Might make things less jittery:
        moveDir.Normalize();

        if ((!isDead) && (!inGameScript.isMenuUp) && (!inGameScript.isWindowUp))
        {
            // Actually moves the mouse:
            transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);
        }

        // Jumping:
        //isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundLayerMask);

        //if (!isGrounded)
        //{
        //    velocity.y = -2.0f;
        //}

        if ((inGameScript.isMenuUp) || (inGameScript.isWindowUp))
        {
            mouse.SetBool("run", false);
            mouse.SetBool("jump", false);
            mouse.SetBool("idle", true);
        }

        if (isDead == false)
        {
            if ((!inGameScript.isMenuUp) && (!inGameScript.isWindowUp))
            {
                if (moveDir != Vector3.zero)
                {
                    // Points the mouse in the right direction:
                    transform.forward = moveDir;

                    if (!Input.GetKeyDown(KeyCode.Space))
                    {
                        // Running animation:
                        mouse.SetBool("run", true);
                        mouse.SetBool("idle", false);
                        mouse.SetBool("jump", false);
                    }
                    else
                    {
                        //if (isGrounded)
                        //{
                        //    velocity.y = jumpHeight;
                        //}

                        // Jumping animation
                        mouse.SetBool("jump", true);
                        mouse.SetBool("idle", false);
                        mouse.SetBool("run", false);
                    }
                }
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
                        //if (isGrounded)
                        //{
                        //    velocity.y = jumpHeight;
                        //}
                        mouse.SetBool("jump", true);
                        mouse.SetBool("idle", false);
                        mouse.SetBool("run", false);
                    }
                }
            }
        }
        else
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

        // More for jumping:
        //velocity.y += gravity * Time.deltaTime;

 
        //    if (Input.GetKey(KeyCode.E))
        //    {
        //        mouse.SetBool("eat", true);
        //        mouse.SetBool("idle", false);
        //        mouse.SetBool("walk", false);
        //        mouse.SetBool("run", false);
        //    }
        //    if (Input.GetKeyUp(KeyCode.E))
        //    {
        //        mouse.SetBool("idle", true);
        //        mouse.SetBool("eat", false);
        //    }
        //    if (Input.GetKey(KeyCode.K))
        //    {
        //        mouse.SetBool("death", true);
        //        mouse.SetBool("idle", false);
        //        mouse.SetBool("walk", false);
        //        mouse.SetBool("run", false);
        //    }
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

                // Play the nesting SFX:
                if (!inGameScript.soundEffectsMuted)
                {
                    nestingSFX.Play();
                }

                // Increase the stage of nest development:
                nestStage += 1;

                // Depending on the stage of nest development, determine which new part of the nest
                // to make visible:
                switch (nestStage)
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
