using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/* Need to add:
 * - Ground Check to prevent infinte jumping
 * - Double jump counter
 * - Wall climbing
 */

public class NewBehaviourScript : MonoBehaviour
{
    //Initalise speed values and rigid body component 
    //Serialize field just makes values appear to editor
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpSpeed = 5;
    [SerializeField] private LayerMask groundMask;
    private int jumps = 0;
    Rigidbody2D rb;
    BoxCollider2D boxColl2D;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxColl2D = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        //Grabs horizontal input and moves accordingly, does not change Y axis movement
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
        //If on the ground, reset number of jumps
        if(isGrounded() == true)
        {
            jumps = 2;
            
        }
        //if space is pressed, and the player has atleast one jump, go up
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(jumps);
            if (jumps > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                jumps = jumps - 1;
               
            }
        }
    }
    private bool isGrounded()
    {
        float extraSize = 0.2f;
        RaycastHit2D boxCheck = Physics2D.BoxCast(boxColl2D.bounds.center, boxColl2D.bounds.size, 0f, Vector2.down, extraSize, groundMask);
        //Debug.Log(boxCheck.collider);
        return boxCheck.collider != null;
        
    }

}
