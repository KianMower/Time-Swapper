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
    float horizontalInput;
    //Initalise speed values and rigid body component 
    //Serialize field just makes values appear to editor
    [Header("Moving Speeds")]
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpSpeed = 5;
    [SerializeField] private float wallJumpSpeed = 5;
    [Header("Jumping Values")]
    [SerializeField] private float wallJumpCD = 0.2f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask wallMask;
    [SerializeField] private float coyoteTime;

    private float currentWallJumpCD;
    private float coyoteTimeTimer;
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
        //Flip the player by inverting X scale
        horizontalInput = Input.GetAxis("Horizontal");
        if(horizontalInput > 0.01f )
        {
            transform.localScale = Vector3.one;
        }
        else if(horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //Grabs horizontal input and moves accordingly, does not change Y axis movement
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
    
        //If is grounded, reset coyote time timer
        if(isGrounded())
        {
            coyoteTimeTimer = coyoteTime;
            //doubleJump = true;
        }
        //If not grounded, tick down coyote time timer
        else
        {
            coyoteTimeTimer -= Time.deltaTime;
        }

        //Jumping Logic
        //If grounded and not pressing jump, set double jump to false
        if (isOnWall() == false)
        {
            if (coyoteTimeTimer > 0 && !Input.GetButton("Jump"))
            {
                doubleJump = false;
            }
            if (Input.GetButtonDown("Jump"))
            {
                //If jump pressed, is grounded or double jump is true, make player jump and switch double jump boolean 
                if (coyoteTimeTimer > 0 || doubleJump)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                    doubleJump = !doubleJump;
                }
            }

        }
        //Wall Jump Logic
        else
        {
            if(currentWallJumpCD == 0)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    rb.velocity = new Vector2(rb.velocity.x, wallJumpSpeed);
                    currentWallJumpCD = wallJumpCD;
                }
            }
            else
            {
                currentWallJumpCD -= Time.deltaTime;
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

    //Checks the player is on a wall by doing a box cast in front of the player. If it hits an object with the tag 'wall', we must be walled so return true
    private bool isOnWall()
    {
        RaycastHit2D boxCheckWall = Physics2D.BoxCast(boxColl2D.bounds.center, boxColl2D.bounds.size, 0f, new Vector2(transform.localScale.x, 0), 0.2f, wallMask);
        return boxCheckWall.collider != null;
    }

}
