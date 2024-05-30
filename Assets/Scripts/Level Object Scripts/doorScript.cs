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
    Vector2 topHalfInitPos;
    Vector2 bottomHalfInitPos;

    private Vector2 topHalfInitStartPos;
    private Vector2 bottomHalfInitStartPos;
    private Vector2 topHalfInitendPos;
    private Vector2 bottomHalfInitendPos;

    public float openHeight;


    float timer = 0;
    public float openTime = 1f;
    private bool opening = false;
    

    private void Start()
    {
        topHalfInitPos = topHalf.transform.position;
        bottomHalfInitPos = bottomHalf.transform.position;

        topHalfInitStartPos = topHalfInitPos;
        bottomHalfInitStartPos = bottomHalfInitPos;

        topHalfInitendPos = new Vector2(topHalfInitPos.x, topHalfInitPos.y + openHeight);
        bottomHalfInitendPos = new Vector2(topHalfInitPos.x, topHalfInitPos.y - openHeight);
    }

    private void Update()
    {
        topHalfInitendPos = new Vector2(topHalfInitPos.x, topHalfInitPos.y + openHeight);
        bottomHalfInitendPos = new Vector2(topHalfInitPos.x, topHalfInitPos.y - openHeight);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("enter");
            opening = true;
            timeElapsed = 0;


        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("exit");
            opening = false;

            timeElapsed = 0;


        }
    }



    private float timeElapsed=0;
    public float lerpDuration = 3;
    private void FixedUpdate()
    {
        //if (timer <= openTime && opening)
        //{
        //    timer += Time.deltaTime;
        //    float result = (timer / openTime);
        //    Debug.Log("timer <= openTime && opening");
        //    topHalf.transform.position = new Vector2(topHalfInitPos.x, topHalfInitPos.y + (openHeight * result));
        //    bottomHalf.transform.position = new Vector2(bottomHalfInitPos.x, bottomHalfInitPos.y - (openHeight * result));
        //}

        //if (timer >= 0f && !opening)
        //{
        //    timer -= Time.deltaTime;
        //    float result = (timer / openTime);
        //    Debug.Log("result " +result);
        //    topHalf.transform.position = new Vector2(topHalfInitPos.x, topHalfInitPos.y + (openHeight * result));
        //    bottomHalf.transform.position = new Vector2(bottomHalfInitPos.x, bottomHalfInitPos.y - (openHeight * result));
        //}
        //Debug.Break();

        if(opening &&  timeElapsed < lerpDuration)
        {

            float lerpTime = timeElapsed / lerpDuration;
            timeElapsed += Time.deltaTime;
            topHalf.transform.position = Vector3.Lerp(topHalfInitStartPos, topHalfInitendPos, lerpTime);
            bottomHalf.transform.position = Vector3.Lerp(bottomHalfInitStartPos, bottomHalfInitendPos, lerpTime);
        }
        else if (!opening && timeElapsed < lerpDuration)
        {
            float lerpTime = timeElapsed / lerpDuration;
            timeElapsed += Time.deltaTime;
            topHalf.transform.position = Vector3.Lerp(topHalfInitendPos, topHalfInitStartPos, lerpTime);
            bottomHalf.transform.position = Vector3.Lerp(bottomHalfInitendPos, bottomHalfInitStartPos, lerpTime);
        }

    }

}
