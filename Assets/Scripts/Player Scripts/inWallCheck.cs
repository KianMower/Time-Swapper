using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inWallCheck : MonoBehaviour
{
    [SerializeField] LayerMask ground;
    private Vector3 edgeDirection;
    //Distance to teleport the player if we are stuck
    public float teleportDistance = 1f;
    private Vector2 rcPos;
    private PlayerController controller;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
       //Debug.Log(isStuck());
       if(isStuck())
       {
            //Debug.Log("is stuck");
            Vector3 teleportTo = newPos();
            transform.position = new Vector2(teleportTo.x, teleportTo.y);
       }

    }   

    public bool isStuck() //This function works :)
    {
        //Does a raycast and if it collides hits object on ground layer, we are stuck so it returns true!
        RaycastHit2D check = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, ground);
        return check.collider != null;
    }

    public Vector3 newPos()
    {
        float stepSize = 1f;
        Vector3 newPosition = transform.position;

        while (isStuck())
        {
            // Check surrounding positions for availability
            //These two for loops change x/y pos for the check so we scan around the player in a circle
            for (float xOffset = -stepSize; xOffset <= stepSize; xOffset += stepSize)
            {
                for (float yOffset = -stepSize; yOffset <= stepSize; yOffset += stepSize)
                {
                    Vector3 offset = new Vector3(xOffset, yOffset, 0);
                    Vector3 testPosition = transform.position + offset;
                    Debug.Log(testPosition);

                    // Check if the test position is available (not colliding with walls)
                    if (!Physics2D.OverlapCircle(testPosition, 0.1f))
                    {
                        // Found an available spot, teleport the player here
                        newPosition = testPosition;
                        return newPosition;
                    }
                }
            }

            // Expand the search area for the next iteration
            stepSize++;
        }

        // If no available spot is found, return the original position
        return transform.position;
    }

}