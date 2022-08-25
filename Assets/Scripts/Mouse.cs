using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public InGameScript inGameScript;

    public GameObject mouseTrapAnim;
    public AudioSource mouseTrapAudio;

    private Animator mouse;

    CharacterController mouseController;

    public float moveSpeed = 1.3f;
    public float rotationSpeed = 720;

    public Vector3 direction;

    // Boolean determining if the mouse should be dead:
    public bool isDead = false;

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
    }

    // Update is called once per frame
    void Update()
    {
        moveZ = Input.GetAxis("Horizontal") * -1;
        moveX = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(moveX, 0, moveZ);

        // Might make things less jittery:
        moveDir.Normalize();

        if (!isDead)
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

        if (isDead == false)
        {
            if (moveDir != Vector3.zero)
            {
                // Points the mouse in the right direction:
                transform.forward = moveDir;

                if (!Input.GetKeyDown(KeyCode.Space))
                {
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
        else
        {
            mouse.SetBool("jump", false);
            mouse.SetBool("idle", false);
            mouse.SetBool("run", false);
            mouse.SetBool("death", true);

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
            mouseTrapAudio.Play();
            isDead = true;
        }
    }
}
