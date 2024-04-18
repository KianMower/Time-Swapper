using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuturePresentSwitcher : MonoBehaviour
{
    bool inFuture = false;
    public float switchCooldown = 1f;
    private float cooldown;
    [SerializeField] GameObject present;
    [SerializeField] GameObject future;

    // Update is called once per frame
    void Update()
    {
        if(cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
        }

        //Teleport to present (left click)
        if(Input.GetMouseButtonDown(0) && (inFuture) && cooldown <= 0f)
        {
            cooldown = switchCooldown;
            future.SetActive(false);
            present.SetActive(true);
            inFuture = !inFuture;
        }
        //Teleport to future (right click)
        if(Input.GetMouseButtonDown(1) && (!inFuture) && cooldown <= 0f)
        {
            cooldown = switchCooldown;
            future.SetActive(true);
            present.SetActive(false);
            inFuture = !inFuture;
        }
    }
}
