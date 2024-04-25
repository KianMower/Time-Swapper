using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class doorScript : MonoBehaviour
{
    /* This script works by having two game objects, their init positions, and a open height. When the player enters door collider,
     * sets open to true, and the twp halves of the door have their Y coordinate changed over time. When the player leaves the collider, 
     * the opposite happens and the Y coordinate is chased back to its original value. */


    [SerializeField] GameObject topHalf;
    [SerializeField] GameObject bottomHalf;
    Vector3 topHalfInitPos;
    Vector3 bottomHalfInitPos;
    public float openHeight;


    float timer = 0;
    public float openTime = 1f;
    private bool opening = false;
    private bool closed = true;

    private void Start()
    {
        topHalfInitPos = topHalf.transform.position;
        bottomHalfInitPos = bottomHalf.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("enter");
            opening = true;
            closed = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("exit");
            opening = false;
            closed = true;
        }
    }

    private void FixedUpdate()
    {
        if (timer <= openTime && opening)
        {
            timer += Time.deltaTime;
            float result = (timer / openTime);
            //Debug.Log(result);
            topHalf.transform.position = new Vector3(topHalfInitPos.x, topHalfInitPos.y + (openHeight * result), topHalfInitPos.z);
            bottomHalf.transform.position = new Vector3(bottomHalfInitPos.x, bottomHalfInitPos.y - (openHeight * result), bottomHalfInitPos.z);
        }

        if (timer >= 0f && closed)
        {
            timer -= Time.deltaTime;
            float result = (timer / openTime);
            //Debug.Log(result);
            topHalf.transform.position = new Vector3(topHalfInitPos.x, topHalfInitPos.y + (openHeight * result), topHalfInitPos.z);
            bottomHalf.transform.position = new Vector3(bottomHalfInitPos.x, bottomHalfInitPos.y - (openHeight * result), bottomHalfInitPos.z);
        }

    }

}
