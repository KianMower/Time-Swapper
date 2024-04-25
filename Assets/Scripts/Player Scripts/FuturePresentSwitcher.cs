using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuturePresentSwitcher : MonoBehaviour
{
    bool inFuture = false;
    [SerializeField] GameObject present;
    [SerializeField] GameObject future;

    // Update is called once per frame
    void Update()
    {
        //Teleport to present (left click)
        if(Input.GetMouseButtonDown(0) && (inFuture))
        {
            future.SetActive(false);
            present.SetActive(true);
            inFuture = !inFuture;
        }
        //Teleport to future (right click)
        if(Input.GetMouseButtonDown(1) && (!inFuture))
        {
            future.SetActive(true);
            present.SetActive(false);
            inFuture = !inFuture;
        }
    }
}
