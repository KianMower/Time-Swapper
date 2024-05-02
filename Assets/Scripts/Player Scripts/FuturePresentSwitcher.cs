using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuturePresentSwitcher : MonoBehaviour
{
    bool inFuture = false;
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
        //Teleporting player and camera method
        if (useTeleportMethod)
        {
            //Teleport to present (left click)
            if (Input.GetMouseButtonDown(0) && (inFuture))
            {
                inFuture = !inFuture;
                transform.position = new Vector3(transform.position.x, transform.position.y - 600, transform.position.z);
                playerCam.transform.position = new Vector3(playerCam.transform.position.x, playerCam.transform.position.y - 600, playerCam.transform.position.z);
            }
            //Teleport to future (right click)
            if (Input.GetMouseButtonDown(1) && (!inFuture))
            {
                inFuture = !inFuture;
                transform.position = new Vector3(playerCam.transform.position.x, playerCam.transform.position.y + 600, playerCam.transform.position.z);
                playerCam.transform.position = new Vector3(playerCam.transform.position.x, playerCam.transform.position.y + 600, playerCam.transform.position.z);
            }
        }
        //Old (laggy) method
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

