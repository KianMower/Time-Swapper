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
    private Vector3 oldPlayerPosition;
    private Vector3 deltaPlayerPosition;
    
    void Start()
    {
        oldPlayerPosition = playerPos.position;
    }

    void FixedUpdate()
    {
        Vector3 moveTo = playerPos.position + cameraOffset;
        transform.position = Vector3.SmoothDamp(transform.position, moveTo, ref velocity, damping);
    }

    void Update()
    {
        
    }
}
