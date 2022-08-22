using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Mouse_Trap : MonoBehaviour
{

    public GameObject mouseTrapAnim;
    public AudioSource mouseTrapAudio;

    void Start()
    {

    }


    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            Debug.Log("BANG!");
            mouseTrapAnim.GetComponent<Animation>().Play();
            mouseTrapAudio.Play();
        }
    }
}