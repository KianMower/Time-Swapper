using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    //This script adds smooth camera movement which works by having a camera offset added to the target position
    //which is the player's positionm

    public Transform playerPos;
    public Vector3 cameraOffset;
    public float damping;
    private Vector3 velocity = Vector3.zero;

    //Used to change camera offset later
    public float movingCameraXOffset;
  
    void Update()
    {
        //Checks which way the player is facing and adds offset to camera position
        //Reuslts in player being able to see slightly more in the direction they are moving
        if (playerPos.localScale.x > 0f) //Right
        {
            cameraOffset.x = movingCameraXOffset;
        }
        else if (playerPos.localScale.x < 0f) //Left
        {
            cameraOffset.x = -movingCameraXOffset;
        }

        Vector3 moveTo = playerPos.position + cameraOffset;
        transform.position = Vector3.SmoothDamp(transform.position, moveTo, ref velocity, damping);
    }
}
