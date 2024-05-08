using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuturePresentSwitcher : MonoBehaviour
{
    public bool inFuture = false;
    public float switchCooldown = 2f;
    private float timer;

    //option for left and right click/just left click
    [SerializeField] bool oneClickOption = false;

    [Header("Toggling Gameobjects Method")]
    [SerializeField] GameObject present;
    [SerializeField] GameObject future;
    

    //teleporting test//
    [Header("Teleporting method")]
    public bool useTeleportMethod = false;
    [SerializeField] GameObject playerCam;

    // Update is called once per frame
    void Update()
    {
        if(timer > 0f)
        {
            timer -= Time.deltaTime;
        }

        //Teleporting player and camera method
        if (useTeleportMethod)
        {
            //Old control scheme, left click for present right click for future
            if (!oneClickOption)
            {
                //Teleport to present (left click)
                if (Input.GetMouseButtonDown(0) && (inFuture) && (timer <= 0f))
                {
                    inFuture = !inFuture;
                    transform.position = new Vector3(transform.position.x, transform.position.y - 300, transform.position.z);
                    playerCam.transform.position = new Vector3(playerCam.transform.position.x, playerCam.transform.position.y - 300, playerCam.transform.position.z);
                    timer = switchCooldown;
                }
                //Teleport to future (right click)
                if (Input.GetMouseButtonDown(1) && (!inFuture) && (timer <= 0f))
                {
                    inFuture = !inFuture;
                    transform.position = new Vector3(transform.position.x, transform.position.y + 300, transform.position.z);
                    playerCam.transform.position = new Vector3(playerCam.transform.position.x, playerCam.transform.position.y + 300, playerCam.transform.position.z);
                    timer = switchCooldown;
                }
            }
            //New control scheme, left click switches to the other time period
            else
            {
                //Teleport to present 
                if (Input.GetMouseButtonDown(0) && (inFuture) && (timer <= 0f))
                {
                    inFuture = !inFuture;
                    transform.position = new Vector3(transform.position.x, transform.position.y - 300, transform.position.z);
                    playerCam.transform.position = new Vector3(playerCam.transform.position.x, playerCam.transform.position.y - 300, playerCam.transform.position.z);
                    timer = switchCooldown;
                }
                //Teleport to future
                if (Input.GetMouseButtonDown(0) && (!inFuture) && (timer <= 0f))
                {
                    inFuture = !inFuture;
                    transform.position = new Vector3(transform.position.x, transform.position.y + 300, transform.position.z);
                    playerCam.transform.position = new Vector3(playerCam.transform.position.x, playerCam.transform.position.y + 300, playerCam.transform.position.z);
                    timer = switchCooldown;
                }
            }
            
        }
        //Old (laggy) method, uses legacy control scheme
        else
        {
            //Teleport to present (left click)
            if (Input.GetMouseButtonDown(0) && (inFuture))
            {
                future.SetActive(false);
                present.SetActive(true);
                inFuture = !inFuture;
            }
            //Teleport to future (right click)
            if (Input.GetMouseButtonDown(1) && (!inFuture))
            {
                future.SetActive(true);
                present.SetActive(false);
                inFuture = !inFuture;
            }
        }
    }


}

