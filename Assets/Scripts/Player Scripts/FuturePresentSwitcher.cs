using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuturePresentSwitcher : MonoBehaviour
{
    public bool inFuture = false;
    [Header("Toggling Gameobjects Method")]
    [SerializeField] GameObject present;
    [SerializeField] GameObject future;
    [SerializeField] GameObject presentCogs;
    [SerializeField] GameObject futureCogs;
    [SerializeField] GameObject[] health;

    public Animator animatorPresent;
    public Animator animatorPresentTwo;
    public Animator animatorFuture;
    public Animator animatorFutureTwo;

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
                transform.position = new Vector3(transform.position.x, transform.position.y - 300, transform.position.z);
                playerCam.transform.position = new Vector3(playerCam.transform.position.x, playerCam.transform.position.y - 300, playerCam.transform.position.z);
                
            }
            //Teleport to future (right click)
            if (Input.GetMouseButtonDown(1) && (!inFuture))
            {
                inFuture = !inFuture;
                transform.position = new Vector3(transform.position.x, transform.position.y + 300, transform.position.z);
                playerCam.transform.position = new Vector3(playerCam.transform.position.x, playerCam.transform.position.y + 300, playerCam.transform.position.z);
               
            }
        }
        //Old (laggy) method
        else
        {
            
            if (Input.GetMouseButtonDown(0) && (inFuture))
            {
                animatorFuture.SetBool("Change In Time Future", true);
                animatorFutureTwo.SetBool("Change In Time Future", true);
               
                inFuture = !inFuture;
            }
            
            if (Input.GetMouseButtonDown(1) && (!inFuture))
            {
                animatorPresent.SetBool("Change In Time Present", true);
                animatorPresentTwo.SetBool("Change In Time Present", true);

                inFuture = !inFuture;
            }
        }
        //Teleport/ animate to future (right click)
        if (animatorPresent.GetCurrentAnimatorStateInfo(0).IsName("Change Time to Future"))
        {
            Debug.Log("Change");
            //future.SetActive(true);
            //present.SetActive(false);
            presentCogs.SetActive(false);
            futureCogs.SetActive(true);
        }
        //Teleport/ animate to present (left click)
        if (animatorFuture.GetCurrentAnimatorStateInfo(0).IsName("Change Time To Present"))
        {
            //future.SetActive(false);
            //present.SetActive(true);
            presentCogs.SetActive(true);
            futureCogs.SetActive(false);
        }
    }


}

