using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/* Need to add:
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
    private bool doubleJump;
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
        
        //Jumping Logic
        //If grounded and not pressing jump, set double jump to false
        if(isGrounded() && !Input.GetButton("Jump"))
        {
            doubleJump = false;
        }
        if(Input.GetButtonDown("Jump"))
        {
            //If jump pressed, is grounded or double jump is true, make player jump and switch double jump boolean 
            if(isGrounded() || doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                doubleJump = !doubleJump;
            }
        }

        //If jump released early, jump shorter distance
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
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
