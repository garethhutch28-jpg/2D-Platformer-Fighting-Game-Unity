    using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject cam; //Reference to the camera game object
    bool cameraoff = false; //Boolean to turn the camera off

    //Trigger when a collider enters the cameras collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if the camera is not off and a player or enemy walks past the collider
        if (!cameraoff && other.CompareTag("Player") && !other.isTrigger || other.CompareTag("Enemy") && !other.isTrigger && !cameraoff)
        {
            //activate the camera (this activates the zoom out camera)
            cam.SetActive(true);
            
        }
    }
    //When another collider exists the collider
    private void OnTriggerExit2D(Collider2D other)
    {
        //if either a player or enemy walk past the trigger
        if (other.CompareTag("Player") && !other.isTrigger || other.CompareTag("Enemy") && !other.isTrigger)
        {
            //deactive the camera (the zoomed in camera)
            cam.SetActive(false);
            //turn the zoom in camera off (so the only active one is the zoom out camera)
            cameraoff = true;
        }
    }
}
