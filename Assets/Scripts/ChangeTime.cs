using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeTime : MonoBehaviour
{
    public GameObject present;
    public GameObject future;    

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            future.SetActive(true);
            present.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            future.SetActive(false);
            present.SetActive(true);
        }
    }    
}
