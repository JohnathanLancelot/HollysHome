using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    private Animator mouse;

    CharacterController mouseController;

    public float moveSpeed = 1.3f;
    public float rotationSpeed = 720;

    public Vector3 direction;

    [SerializeField]
    Transform mouseTransform;

    float moveX;
    float moveZ;

    // Start is called before the first frame update
    void Start()
    {
        mouse = GetComponent<Animator>();
        mouseController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //moveZ = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed * -1;
        //moveX = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        moveZ = Input.GetAxis("Horizontal") * -1;
        moveX = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(moveX, 0, moveZ);

        // Might make things less jittery:
        moveDir.Normalize();

        // TOO FAST:
        //mouseController.Move(moveDir);

        // JUST RIGHT SPEED:
        transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);

        if (moveDir != Vector3.zero)
        {
            transform.forward = moveDir;

            mouse.SetBool("idle", false);
            mouse.SetBool("run", true);
        }
        else
        {
            mouse.SetBool("idle", true);
            mouse.SetBool("run", false);
        }

    //    if ((Input.GetKeyUp(KeyCode.UpArrow)) || (Input.GetKeyUp(KeyCode.W)))
    //    {
    //        mouse.SetBool("idle", true);
    //        mouse.SetBool("walk", false);
    //    }
    //    if ((Input.GetKeyUp(KeyCode.DownArrow)) || (Input.GetKeyUp(KeyCode.S)))
    //    {
    //        mouse.SetBool("idle", true);
    //        mouse.SetBool("backward", false);
    //    }
    //    if (Input.GetKeyDown(KeyCode.UpArrow))
    //    {
    //        mouse.SetBool("walk", true);
    //        mouse.SetBool("idle", false);
    //        mouse.SetBool("walkleft", false);
    //        mouse.SetBool("walkright", false);
    //        mouse.SetBool("backward", false);
    //    }
    //    if (Input.GetKeyDown(KeyCode.W))
    //    {
    //        mouse.SetBool("walk", true);
    //        mouse.SetBool("idle", false);
    //        mouse.SetBool("walkleft", false);
    //        mouse.SetBool("walkright", false);
    //        mouse.SetBool("backward", false);
    //    }
    //    if ((Input.GetKeyDown(KeyCode.DownArrow)) || (Input.GetKeyDown(KeyCode.S)))
    //    {
    //        mouse.SetBool("backward", true);
    //        mouse.SetBool("idle", false);
    //        mouse.SetBool("walk", false);
    //        mouse.SetBool("walkleft", false);
    //        mouse.SetBool("walkright", false);
    //    }
    //    if ((Input.GetKeyDown(KeyCode.LeftArrow)) || (Input.GetKeyDown(KeyCode.A)))
    //    {
    //        mouse.SetBool("walkleft", true);
    //        mouse.SetBool("idle", false);
    //        mouse.SetBool("walk", false);
    //        mouse.SetBool("walkright", false);
    //    }
    //    if ((Input.GetKeyUp(KeyCode.LeftArrow)) || (Input.GetKeyUp(KeyCode.A)))
    //    {
    //        mouse.SetBool("idle", true);
    //        mouse.SetBool("walkleft", false);
    //    }
    //    if ((Input.GetKeyDown(KeyCode.RightArrow)) || (Input.GetKeyDown(KeyCode.D)))
    //    {
    //        mouse.SetBool("walkright", true);
    //        mouse.SetBool("idle", false);
    //        mouse.SetBool("backward", false);
    //        mouse.SetBool("walk", false);
    //        mouse.SetBool("walkleft", false);
    //    }
    //    if ((Input.GetKeyUp(KeyCode.RightArrow)) || (Input.GetKeyUp(KeyCode.D)))
    //    {
    //        mouse.SetBool("idle", true);
    //        mouse.SetBool("walkright", false);
    //    }
    //    if (Input.GetKeyDown(KeyCode.LeftArrow))
    //    {
    //        mouse.SetBool("walkleft", true);
    //        mouse.SetBool("walk", false);
    //        mouse.SetBool("backward", false);
    //        mouse.SetBool("idle", false);
    //        mouse.SetBool("walkright", false);
    //    }
    //    if (Input.GetKeyUp(KeyCode.LeftArrow))
    //    {
    //        mouse.SetBool("walk", true);
    //        mouse.SetBool("walkleft", false);
    //    }
    //    if (Input.GetKeyDown(KeyCode.RightArrow))
    //    {
    //        mouse.SetBool("walkright", true);
    //        mouse.SetBool("walk", false);
    //        mouse.SetBool("backward", false);
    //        mouse.SetBool("idle", false);
    //        mouse.SetBool("walkleft", false);
    //    }
    //    if (Input.GetKeyUp(KeyCode.RightArrow))
    //    {
    //        mouse.SetBool("walk", true);
    //        mouse.SetBool("walkright", false);
    //    }
    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        mouse.SetBool("walkleft", true);
    //        mouse.SetBool("walk", false);
    //        mouse.SetBool("backward", false);
    //        mouse.SetBool("idle", false);
    //        mouse.SetBool("walkright", false);
    //    }
    //    if (Input.GetKeyUp(KeyCode.A))
    //    {
    //        mouse.SetBool("walk", true);
    //        mouse.SetBool("walkleft", false);
    //    }
    //    if (Input.GetKeyDown(KeyCode.D))
    //    {
    //        mouse.SetBool("walkright", true);
    //        mouse.SetBool("walk", false);
    //        mouse.SetBool("backward", false);
    //        mouse.SetBool("idle", false);
    //        mouse.SetBool("walkleft", false);
    //    }
    //    if (Input.GetKeyUp(KeyCode.D))
    //    {
    //        mouse.SetBool("walk", true);
    //        mouse.SetBool("walkright", false);
    //    }
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        mouse.SetBool("jump", true);
    //        mouse.SetBool("idle", false);
    //        mouse.SetBool("walk", false);
    //        mouse.SetBool("run", false);
    //    }
    //    if (Input.GetKeyUp(KeyCode.Space))
    //    {
    //        mouse.SetBool("idle", true);
    //        mouse.SetBool("jump", false);
    //    }
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
}
