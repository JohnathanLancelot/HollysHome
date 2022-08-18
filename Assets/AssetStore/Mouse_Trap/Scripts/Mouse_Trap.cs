using UnityEngine;
using System.Collections;

public class Mouse_Trap : MonoBehaviour {

public GameObject mouseTrapAnim;
public AudioSource mouseTrapAudio;

void Start (){

    

}  
  
  
void Update (){
 
    if (Input.GetButtonDown("Fire1")) //check to see if the left mouse was pressed - trigger trap
    {

        TriggerTrap();
         
    }
            
}


void TriggerTrap (){

    mouseTrapAnim.GetComponent<Animation>().Play();
    mouseTrapAudio.Play();

}


}