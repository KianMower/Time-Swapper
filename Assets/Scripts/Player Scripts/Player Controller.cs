using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

//Movement using old input system
public class NewBehaviourScript : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wall;

    //Basic movement
    [Header("Speeds")]
    private float horizontalInput;
    public float speed = 8f;
    public float jumpingSpeed = 16f;
    private bool isFacingRight = true;

    //Double jump
    private bool doubleJump;

    //Dashing
    [Header("Dash")]
    private bool dashAllowed = true;
    private bool isDashing;
    public float dashSpeed = 24f;
    public float dashTime = 0.2f;
    public float dashCooldown = 1.5f;

    //Wall sliding
    private bool isWallSliding;
    public float wallSlidingSpeed = 4f;

    //Wall jumping
    private bool isWallJumping;
    private float wallJumpDirection;
    private float wallJumpTime = 0.2f;
    private float wallJumpTimer;
    public float wallJumpDuration = 0.4f;
    public Vector2 wallJumpingPower = new Vector2(8f, 16f);

    void Update()
    {
        //Prevents player inputting extra actions while dashing
        if(isDashing)
        {
            return;
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");
        //Jumping
        if(isGrounded() && !Input.GetButton("Jump"))
        {
            doubleJump = false;
        }

        if(Input.GetButtonDown("Jump") && (isGrounded() || doubleJump))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingSpeed);

            doubleJump = !doubleJump;
        }
        
        if (Input.GetButtonDown("Jump") && rb.velocity.y > 0.0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        //Trigger dash
        //On left shift for now, will change to double tap later :)
        if(Input.GetKeyDown(KeyCode.LeftShift) && dashAllowed)
        {
            StartCoroutine(dash());
        }

        wallSlide();
        wallJump();

        //Stops player fliping while wall jumping 
        if(!isWallJumping)
        {
            flipPlayer();
        }
        
    }

    private void FixedUpdate()
    {
        //Prevents player inputting extra actions while dashing
        if (isDashing)
        {
            return;
        }

        //Takes horizontal input from update and sets speed if we are not wall jumping
        if(!isWallJumping)
        {
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        }
        
    }

    private bool isGrounded()
    {
        //Used a overlap circle on the groundCheck game object to see if we are grounded
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground);
    }

    private bool isOnWall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wall);
    }

    private void wallSlide()
    {
        //If is wall sliding, set velocity to (velocity.x, and clamp Y between its current velocity and wallslide speed
        //This means we dont continue accelerating down the wall
        if (isOnWall() && !isGrounded() && horizontalInput != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
            
    }

    private void wallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpDirection = -transform.localScale.x; //We want to wall jump in the opposite direction we are facing
            wallJumpTimer = wallJumpTime;
            CancelInvoke(nameof(stopWallJump));
        }
        //Lets us leave the wall and be able to wall jump for a brief moment of time
        else
        {
            wallJumpTimer -= Time.deltaTime;
        }

        if (Input.GetButton("Jump") && wallJumpTimer > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpTimer = 0f;
            //Flips player if facing direction and wallJumpDirection arent equal
            if (transform.localScale.x != wallJumpDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
            }

            Invoke(nameof(stopWallJump), wallJumpDuration); //Calls function stopWallJump after duration of wallJump
        }
    }
    private void stopWallJump()
    {
        isWallJumping = false;
    }

    private void flipPlayer()
    {
        //Based on horizontal input, switch isFacingRight
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f) 
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    private IEnumerator dash()
    {
        dashAllowed = false;
        isDashing = true;
        float startingGravity = rb.gravityScale;
        rb.gravityScale = 0; //Sets gravity to 0 so the player doesnt fall during the dash
        rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0f ); //transform.localScale.x is player direction
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = startingGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        dashAllowed = true;
    }
}
