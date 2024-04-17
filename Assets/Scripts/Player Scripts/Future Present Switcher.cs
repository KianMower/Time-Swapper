using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuturePresentSwitcher : MonoBehaviour
{
    Rigidbody2D rb;
    bool inFuture = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Teleport to present (left click)
        if(Input.GetMouseButtonDown(0) && (inFuture))
        {
            Debug.Log("present");
            rb.position = new Vector2(rb.position.x, rb.position.y - 30);
            inFuture = false;
        }
        //Teleport to future (right click)
        if(Input.GetMouseButtonDown(1) && (!inFuture))
        {
            Debug.Log("future");
            rb.position = new Vector2(rb.position.x, rb.position.y + 30);
            inFuture = true;
        }
    }
}
