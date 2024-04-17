using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTime : MonoBehaviour
{
    public GameObject present;
    public GameObject future;

    static bool changeTime = false;    

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            changeTime = !changeTime;
            TimesChanging();
        }

        
    }

    void TimesChanging()
    {
        if (changeTime)
        {
            future.SetActive(true);
            present.SetActive(false);
        }
        else
        {
            future.SetActive(false);
            present.SetActive(true);
        }
    }
}
